using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "'{0}' alanı zorunludur."), Display(Name = "Id")]
        public int Id { get; set; }
    }
}
