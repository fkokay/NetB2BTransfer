using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Smartstore.Library.Models
{
    public class SmartstoreProductVariantAttribute
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public string? TextPrompt { get; set; }
        public string? CustomData { get; set; }
        public bool IsRequired { get; set; }
        public int AttributeControlTypeId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
