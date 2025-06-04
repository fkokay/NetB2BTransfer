using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreShippingAddress
    {
        public int Id { get; set; }
        public string Salutation { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public int? CountryId { get; set; }
        public int? StateProvinceId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public int? DistrictId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public SmartstoreCity? City { get; set; }
        public SmartstoreTown? Town { get; set; }
        public SmartstoreDistrict? District { get; set; }
    }
}
