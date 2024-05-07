using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Soyad"), StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre"), StringLength(50)]
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
