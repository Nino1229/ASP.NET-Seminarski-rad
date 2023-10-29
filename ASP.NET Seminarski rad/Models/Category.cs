using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ASP.NET_Seminarski_rad.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv kategorije je obavezan!")]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Naziv kategorije")]
        public string CategoryTitle { get; set; }

        [ForeignKey("CategoryId")]
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
