using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Seminarski_rad.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno!")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno!")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email adresa je obavezna!")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail adresa je neispravna!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Broj telefona je neispravan.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna!")]
        [StringLength(150)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Grad je obavezan!")]
        [StringLength(50)]
        public string? City { get; set; }
        [Required(ErrorMessage = "Država je obavezna!")]
        [StringLength(50)]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Poštanski broj je obavezan!")]
        [StringLength(20)]
        public string? ZipCode { get; set; }
    }
}
