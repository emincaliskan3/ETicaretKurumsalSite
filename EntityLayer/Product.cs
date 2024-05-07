using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı"), StringLength(150), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Ürün Kodu"), StringLength(50)]
        public string Code { get; set; }
        [Display(Name = "Ürün Açıklaması"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        [Display(Name = "Fırsat Ürün")]
        public bool IsHome { get; set; }
        [Display(Name = "Popüler Ürün")]
        public bool IsPopular { get; set; }
        [Display(Name = "Stok"), Required(ErrorMessage = "Stok Alanı Boş Geçilemez!")]
        public int Stock { get; set; }
        [Display(Name = "Fiyat"), Required(ErrorMessage = "Fiyat Alanı Boş Geçilemez!")]
        public decimal Price { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public virtual Category? Category { get; set; }

    }
}
