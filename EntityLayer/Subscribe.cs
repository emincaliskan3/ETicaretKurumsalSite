using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Subscribe : IEntity
    {
        public int Id { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
