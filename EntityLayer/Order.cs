using EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        // müşteri bilgileri
        [Display(Name = "Ad"), StringLength(50), Required]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50), Required]
        public string Surname { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [Display(Name = "Telefon"), StringLength(15)]
        public string? Phone { get; set; }
        [Display(Name = "Adres"), StringLength(500), Required]
        public string Address { get; set; }
        [Display(Name = "Sipariş Bilgileri")]
        public string OrderDetail { get; set; }
        [Display(Name = "Sipariş Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "CVV"), StringLength(3), Required]
        public string CVV { get; set; }
        [Display(Name = "Kart Numarası"), StringLength(16), Required]
        public string CardNo { get; set; }
    }
}
