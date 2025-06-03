using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreCustomer
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CustomerGuid")]
        public Guid CustomerGuid { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("ClientIdent")]
        public string ClientIdent { get; set; } // Can be null

        [JsonPropertyName("AdminComment")]
        public string AdminComment { get; set; } // Can be null

        [JsonPropertyName("IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [JsonPropertyName("AffiliateId")]
        public int AffiliateId { get; set; }

        [JsonPropertyName("Active")]
        public bool Active { get; set; }

        [JsonPropertyName("Deleted")]
        public bool Deleted { get; set; }

        [JsonPropertyName("IsSystemAccount")]
        public bool IsSystemAccount { get; set; }

        [JsonPropertyName("SystemName")]
        public object SystemName { get; set; } // Can be null

        [JsonPropertyName("LastIpAddress")]
        public string LastIpAddress { get; set; }

        [JsonPropertyName("CreatedOnUtc")]
        public DateTime CreatedOnUtc { get; set; }

        [JsonPropertyName("LastLoginDateUtc")]
        public DateTime? LastLoginDateUtc { get; set; }

        [JsonPropertyName("LastActivityDateUtc")]
        public DateTime LastActivityDateUtc { get; set; }

        [JsonPropertyName("LastVisitedPage")]
        public string LastVisitedPage { get; set; }

        [JsonPropertyName("Salutation")]
        public string Salutation { get; set; } // Can be null

        [JsonPropertyName("Title")]
        public string Title { get; set; } // Can be null

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("FullName")]
        public string FullName { get; set; }

        [JsonPropertyName("Company")]
        public string Company { get; set; }

        [JsonPropertyName("CustomerNumber")]
        public string CustomerNumber { get; set; } // Can be null

        [JsonPropertyName("BirthDate")]
        public DateTime? BirthDate { get; set; } // Can be null

        [JsonPropertyName("Gender")]
        public string Gender { get; set; } // Can be null

        [JsonPropertyName("VatNumberStatusId")]
        public int VatNumberStatusId { get; set; }

        [JsonPropertyName("TimeZoneId")]
        public int? TimeZoneId { get; set; } // Can be null

        [JsonPropertyName("TaxDisplayTypeId")]
        public int TaxDisplayTypeId { get; set; }

        [JsonPropertyName("LastForumVisit")]
        public DateTime? LastForumVisit { get; set; } // Can be null

        [JsonPropertyName("LastUserAgent")]
        public string LastUserAgent { get; set; }

        [JsonPropertyName("LastUserDeviceType")]
        public string LastUserDeviceType { get; set; }

        [JsonPropertyName("BillingAddressId")]
        public int BillingAddressId { get; set; }

        [JsonPropertyName("ShippingAddressId")]
        public int ShippingAddressId { get; set; }

        [JsonPropertyName("LimitedToStores")]
        public bool LimitedToStores { get; set; }

    }
}
