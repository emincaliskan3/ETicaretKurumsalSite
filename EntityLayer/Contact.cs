using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad"), StringLength(50), Required]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50), Required]
        public string Surname { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [Display(Name = "Telefon"), StringLength(15)]
        public string? Phone { get; set; }
        [Display(Name = "Mesaj"), StringLength(500), Required]
        public string Message { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
