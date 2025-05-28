using System.ComponentModel.DataAnnotations;

public class NoticiaViewModel
{
    public int Id { get; set; }


    [StringLength(250, MinimumLength = 3, ErrorMessage = "O campo Titulo deve ter entre 3 e 250 caracteres.")]
    [Required(ErrorMessage = "Titulo é obrigatório")]
    public string Titulo { get; set; }

    [StringLength(800, MinimumLength = 3, ErrorMessage = "O campo Texto deve ter entre 3 e 800 caracteres.")]
    [Required(ErrorMessage = "Texto é obrigatório")]
    public string Texto { get; set; }
    public int? UsuarioId { get; set; }
    public List<int>? TagIds { get; set; } = [];
}
