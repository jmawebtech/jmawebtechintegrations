using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMAAddress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string RegionName { get; set; }
        public string CountryName { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string PostalCode { get; set; }
        public bool IsBilling { get; set; }
        public string PhoneNumber { get; set; }
        public bool SubjectToVat { get; set; }
        public string FaxNumber { get; set; }
    }
}
