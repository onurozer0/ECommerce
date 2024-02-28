using FFF.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFF.Core.Entities
{
	public class Order
	{
        public int ID { get; set; }

        [Display(Name = "Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [Display(Name = "Ödeme Seçeneği")]
        public PaymentOptions PaymentOption { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public OrderStatus OrderStatus { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }


        [Display(Name = "Sipariş Tarihi")]
        public DateTime CreatedDate { get; set; }
        public int AddressId { get; set; }
        public UserAddresses Address { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped, Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [NotMapped]
        public string CardMonth { get; set; }
        [NotMapped]
        public string CarddYear { get; set; }
        [NotMapped, Display(Name = "CVV")]
        public string CardCv2 { get; set; }
        [NotMapped]
        public int TotalQuantity { get; set; }
        [NotMapped]
        public decimal TotalFee { get; set; }
        public decimal ShippingFee { get; set; }
    }
}
