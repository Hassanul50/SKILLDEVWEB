using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKILLDEVWEB.Model.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Display Name")]
        public string? CategoryName { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 1000, ErrorMessage = "Must be 1-1000 Chareter")]
        public int DisplayOrder { get; set; }
    }
}
