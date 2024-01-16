using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Seminarski_rad.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum kreiranja narudžbe")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Iznos narudžbe")]
        public decimal Total { get; set; }

        [Display(Name = "Popust")]
        public int? Discount { get; set; }

        [Display(Name = "Napomena")]
        public string? Message { get; set; }

        [Required(ErrorMessage = "Ime kupca je obavezno!")]
        [StringLength(50)]
        [DisplayName("Ime")]
        public string? BillingFirstName { get; set; }

        [Required(ErrorMessage = "Prezime kupca je obavezno!")]
        [StringLength(50)]
        [DisplayName("Prezime")]
        public string? BillingLastName { get; set; }

        [Required(ErrorMessage = "Broj telefona kupca je obavezan.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Broj telefona je neispravan.")]
        [DisplayName("Broj telefona")]
        public string? BillingPhoneNumber { get; set; }

        [Required(ErrorMessage = "Adresa kupca je obavezna!")]
        [StringLength(150)]
        [DisplayName("Adresa")]
        public string? BillingAddress { get; set; }

        [Required(ErrorMessage = "Grad kupca je obavezan!")]
        [StringLength(50)]
        [DisplayName("Grad")]
        public string? BillingCity { get; set; }

        [Required(ErrorMessage = "Država kupca je obavezna!")]
        [StringLength(50)]
        [DisplayName("Država")]
        public string? BillingCountry { get; set; }

        [Required(ErrorMessage = "Poštanski broj kupca je obavezan!")]
        [StringLength(20)]
        [DisplayName("Poštanski broj")]
        public string? BillingZipCode { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderItem>? OrderItems { get; set; }

    }
}
