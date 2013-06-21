using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using BigCommerce.RestApi.Core;
using CloudCartConnector.QuickBooks.Core.Interfaces;
using JMA.Plugin.Accounting.QuickBooks;
using JMA.Plugin.Accounting.QuickBooks.DTO;
using JMA.Plugin.Accounting.QuickBooks.LogProvider;

namespace CloudCartConnector.QuickBooks.Core.Provider
{
    public class JMABigCommerceProvider : JMAWebServiceCommonBase
    {
        static HttpClient client;
        static BigCommerceRestApi bigCommerceApi;

        public JMABigCommerceProvider(Controller page, string userName, string password, string qbUrl, string eCommerceSystem, JMASettings qboSettings)
            : base(page, userName, password, qbUrl, eCommerceSystem, qboSettings)
        {
            bigCommerceApi = new BigCommerceRestApi(password, userName, qbUrl);
        }

        /// <summary>
        /// called to authenticate the user to the service. In this case, we tried a sample order. If there is no error, it is OK.
        /// </summary>
        /// <returns></returns>
        public override bool AuthenticateToWebService()
        {
            try
            {
                order od = bigCommerceApi.GetOrderById("1000");
                return base.AuthenticateToWebService();
            }
            catch (Exception ex)
            {
                LogProvider.WriteToLog(new ErrorMessage(MessageSeverity.Error, "CCC", "BigCommerce Web Store login error: " + ex.ToString()));
                if (Page.Session != null)
                {
                    Page.Session["QBUrl"] = null;
                    Page.Session["ECommerceSystem"] = ECommerceSystem;
                    Page.Session["UserName"] = null;
                    Page.Session["Password"] = null;
                    Page.Session["LoggedIn"] = null;
                }

                return false;

            }
        }

        public override JMAUser GetOrders()
        {

            //pull a list of settings, such as highest order number, for user.
            JMASettings jma = Settings;

            if (jma == null)
            {
                jma = GetSettings();
            }

            List<order> soList = new List<order>();

            JMAUser theUser = new JMAUser();
            List<JMAOrder> orderList = new List<JMAOrder>();
            if (jma.LowestOrder > 0 && jma.HighestOrder > 0)
            {
                soList = bigCommerceApi.GetOrdersByIdRange(jma.LowestOrder, jma.HighestOrder);
            }
            else
            {
                soList = bigCommerceApi.GetOrdersByDateRange(jma.LastDownloadUtc, DateTime.Now);

            }

            if (soList == null)
            {
                return new JMAUser();
            }


            foreach (order st in soList)
            {
                if (!RightOrderStatus(jma, st))
                {
                    LogProvider.WriteToLog(new ErrorMessage(MessageSeverity.Info, "CCC", String.Format("Order {0} has an order status of {1}. It will not sync.", st.id, st.status)));
                    continue;
                }
                JMAOrder jo = new JMAOrder();
                jo.Id = int.Parse(st.id);
                jo.Note = string.Empty;
                jo.BillingAddress = new JMAAddress();

                jo.BillingAddress.Address1 = st.billing_address.street_1;
                jo.BillingAddress.Address2 = st.billing_address.street_2;
                jo.BillingAddress.City = st.billing_address.city;
                jo.BillingAddress.Company = st.billing_address.company;
                jo.BillingAddress.Email = st.billing_address.email;
                jo.BillingAddress.FirstName = st.billing_address.first_name;
                jo.BillingAddress.LastName = st.billing_address.last_name;
                jo.BillingAddress.PhoneNumber = st.billing_address.phone;
                jo.BillingAddress.PostalCode = st.billing_address.zip;
                jo.BillingAddress.CountryName = st.billing_address.country;
                jo.BillingAddress.RegionName = st.billing_address.state;
                jo.BillingAddress.TwoLetterIsoCode = st.billing_address.country_iso2;
                jo.BillingAddress.IsBilling = true;

                Address shippingAddress = bigCommerceApi.GetAddresses(int.Parse(st.id)).FirstOrDefault();

                if (shippingAddress != null)
                {
                    jo.ShippingAddress = new JMAAddress();


                    jo.ShippingAddress.Address1 = shippingAddress.street_1;
                    jo.ShippingAddress.Address2 = shippingAddress.street_2;
                    jo.ShippingAddress.City = shippingAddress.city;
                    jo.ShippingAddress.Company = shippingAddress.company;
                    jo.ShippingAddress.Email = shippingAddress.email;
                    jo.ShippingAddress.FirstName = shippingAddress.first_name;
                    jo.ShippingAddress.LastName = shippingAddress.last_name;
                    jo.ShippingAddress.PhoneNumber = shippingAddress.phone;
                    jo.ShippingAddress.PostalCode = shippingAddress.zip;
                    jo.ShippingAddress.CountryName = shippingAddress.country;
                    jo.ShippingAddress.RegionName = shippingAddress.state;
                    jo.ShippingAddress.TwoLetterIsoCode = shippingAddress.country_iso2;
                    if (!String.IsNullOrEmpty(st.date_created))
                    {
                        jo.CreationDate = DateTime.Parse(st.date_created);
                    }
                    else
                    {
                        jo.CreationDate = DateTime.Now;
                    }
                }


                if (String.IsNullOrEmpty(st.payment_method))
                {
                    jo.CardType = "Check";
                    jo.CreditCardName = "Check";
                }
                else
                {
                    jo.CardType = st.payment_method;
                    jo.CreditCardName = st.payment_method;
                }

                jo.OrderDetails = new List<JMAOrderDetail>();
                jo.Discounts = new List<JMADiscount>();

                List<Product> prList = bigCommerceApi.GetProducts(int.Parse(st.id));

                foreach (Product pr in prList)
                {
                    JMAOrderDetail jod = new JMAOrderDetail();
                    jod.OrderId = int.Parse(pr.id);
                    jod.PriceExclTax = Convert.ToDecimal(pr.price_ex_tax);

                    if (jod.PriceExclTax == 0)
                    {
                        continue;
                    }

                    jod.PriceInclTax = Convert.ToDecimal(pr.price_inc_tax);

                    jod.Quantity = int.Parse(pr.quantity);

                    jod.Sku = pr.sku;
                    jod.Name = pr.name;


                    jod.Product = new JMAProduct();
                    jod.Product.Amount = jod.PriceExclTax;
                    jod.Product.PurchaseCost = jod.Product.Amount;
                    jod.Product.IsDownload = false;
                    jod.Product.Name = jod.Name;
                    jod.Product.Sku = pr.sku;
                    jod.Product.Description = jod.Name;


                    //if (sod.catalogid.HasValue)
                    //{
                    //    List<JMAManufacturer> manList = new List<JMAManufacturer>();
                    //    manufacturer man = pc.GetManufacturer(sod.catalogid.Value);
                    //    if (man != null)
                    //    {
                    //        JMAManufacturer m = new JMAManufacturer();
                    //        m.Name = man.manufacturer1;
                    //        manList.Add(m);
                    //        jod.Product.Vendor = man.title;
                    //        jod.Product.Manufacturers = manList;
                    //    }
                    //}

                    //if (!String.IsNullOrEmpty(pr.total_tax) && Convert.ToDecimal(pr.total_tax) > 0)
                    //{
                    //    decimal tr = Convert.ToDecimal(Convert.ToDecimal(pr.total_tax) / Convert.ToDecimal(pr.total_ex_tax));
                    //    if (tr > 0)
                    //    {
                    //        jo.TaxRate = tr * 100;
                    //    }
                    //}


                    jo.OrderDetails.Add(jod);

                }



                if (!String.IsNullOrEmpty(st.discount_amount) && Convert.ToDecimal(st.discount_amount) != 0)
                {
                    JMADiscount dis = new JMADiscount();
                    dis.Amount = Convert.ToDecimal(st.discount_amount) * -1;
                    dis.Code = "Discount";
                    dis.Name = "Discount";
                    dis.Percent = 0;
                    jo.DiscountExclTax = dis.Amount;
                    jo.DiscountInclTax = jo.DiscountExclTax;
                    jo.Discounts.Add(dis);
                }

                if (!String.IsNullOrEmpty(st.coupon_discount) && Convert.ToDecimal(st.coupon_discount) != 0)
                {
                    JMADiscount dis = new JMADiscount();
                    dis.Amount = Convert.ToDecimal(st.coupon_discount) * -1;
                    dis.Code = "Coupon";
                    dis.Name = "Coupon";
                    dis.Percent = 0;
                    jo.DiscountExclTax = dis.Amount;
                    jo.DiscountInclTax = jo.DiscountExclTax;
                    jo.Discounts.Add(dis);
                }




                if (!String.IsNullOrEmpty(st.date_created))
                {
                    jo.PaidDate = DateTime.Parse(st.date_created);
                }

                jo.PaymentMethodAdditionalFeeExclTax = 0;
                jo.ShippingExclTax = Convert.ToDecimal(st.shipping_cost_ex_tax);
                jo.ShippingInclTax = Convert.ToDecimal(st.shipping_cost_inc_tax);

                jo.ShippingMethod = "Shipping";

                jo.TotalExclTax = Convert.ToDecimal(st.subtotal_ex_tax);
                jo.TotalInclTax = Convert.ToDecimal(st.total_inc_tax);
                jo.Total = Convert.ToDecimal(st.total_inc_tax);
                jo.TotalTax = Convert.ToDecimal(st.total_tax);
                orderList.Add(jo);

            }


            JMAUser ju = new JMAUser();
            ju.Orders = orderList;
            ju.Settings = jma;
            return ju;

        }

        private bool RightOrderStatus(JMASettings jma, order st)
        {
            //get rid of canceled orders
            string status = st.status.Trim().ToLower();
            if (!jma.DeleteQBDuplicates && status == "canceled" || status == "declined" || status == "refunded")
            {
                return false;
            }
            if (!String.IsNullOrEmpty(jma.OrderStatus))
            {
                foreach (string s in jma.OrderStatus.Split(','))
                {
                    if (st.status.Trim().ToLower() == s.Trim().ToLower())
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

    }
}
