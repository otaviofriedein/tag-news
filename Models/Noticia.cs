namespace tag_news.Models
{
    public class Noticia
    {
        public int Id { get; set; }       
        public string Titulo { get; set; }       
        public string Texto { get; set; }
        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
        public ICollection<NoticiaTag> NoticiaTags { get; set; } = new List<NoticiaTag>();
    }
}
