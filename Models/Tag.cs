public class Tag
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public ICollection<NoticiaTag> NoticiaTags { get; set; }
}