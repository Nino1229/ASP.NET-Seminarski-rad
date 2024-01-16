using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ASP.NET_Seminarski_rad.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv proizvoda je obavezan!")]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Naziv")]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "Opis proizvoda je obavezan!")]
        [Display(Name = "Opis")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Količina je obavezna!")]
        [Display(Name = "Količina na stanju")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Cijena je obavezna!")]
        [Display(Name = "Cijena")]
        public decimal ProductPrice { get; set; }

        public string? ProductImage { get; set; }

        [NotMapped]
        public IFormFile? ProductImageFile { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
