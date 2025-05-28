using System.ComponentModel.DataAnnotations;

namespace tag_news.Models
{
    public class Noticia
    {
        public int Id { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = "O campo deve ter entre 3 e 250 caracteres.")]
        [Required]
        public string Titulo { get; set; }

        [StringLength(800, MinimumLength = 3, ErrorMessage = "O campo deve ter entre 3 e 800 caracteres.")]
        [Required]
        public string Texto { get; set; }
        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
        public ICollection<NoticiaTag>? NoticiaTags { get; set; }
    }
}
