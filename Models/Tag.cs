using System.ComponentModel.DataAnnotations;

namespace tag_news.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3,ErrorMessage = "O campo deve ter entre 3 e 100 caracteres.")]
        [Required]
        public string Descricao { get; set; }

        public ICollection<NoticiaTag>? NoticiaTags { get; set; }
    }
}