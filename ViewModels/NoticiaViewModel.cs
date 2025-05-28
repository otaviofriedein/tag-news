public class NoticiaViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public int? UsuarioId { get; set; }
    public List<int>? TagIds { get; set; }
}
