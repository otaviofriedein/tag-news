using System.Net;

namespace tag_news.Shared
{
    public class ServiceResult<T>
    {
        public bool Sucesso { get; set; } = false;
        public string Mensagem { get; set; }
        public T? Dados { get; set; }
        public int StatusCode { get; set; }

        // Factory methods
        public static ServiceResult<T> Ok(T dados, string mensagem = "") =>
            new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

        public static ServiceResult<T> FalhaNegocial(string mensagem, int statusCode = StatusCodes.Status422UnprocessableEntity) =>
            new() { Sucesso = false, Mensagem = mensagem, StatusCode = statusCode };
        
        public static ServiceResult<T> Erro(string mensagem, int statusCode = StatusCodes.Status500InternalServerError) =>
            new() { Sucesso = false, Mensagem = mensagem, StatusCode = statusCode };

        public static ServiceResult<T> NaoEncontrado(string mensagem = "Recurso não encontrado") =>
            new() { Sucesso = false, Mensagem = mensagem, StatusCode = StatusCodes.Status404NotFound };
    }
}
