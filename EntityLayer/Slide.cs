using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Slide : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), StringLength(250)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Url { get; set; }
        [Display(Name = "Açıklama"), StringLength(500), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100), DataType(DataType.ImageUrl)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
