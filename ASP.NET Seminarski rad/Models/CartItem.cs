using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Seminarski_rad.Models
{
    public class CartItem
    {
        [Display(Name = "Proizvod")]
        public Product Product { get; set; }


        [Display(Name = "Količina")]
        public decimal Quantity { get; set; }

        [Display(Name = "Ukupno")]
        public decimal GetTotal()
        { 
            return Product.ProductPrice * Quantity; 
        }
    }
}
