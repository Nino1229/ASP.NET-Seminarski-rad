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
        public DateTime DateCreated { get; set; }
        public decimal Total { get; set; }
        public int? Discount { get; set; }
        public string? Message { get; set; }
        public string UserId { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderItem>? OrderItems { get; set; }

        [ForeignKey("UserDataId")]
        public UserData? UserData { get; set; }

    }
}
