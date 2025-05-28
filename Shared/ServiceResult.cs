namespace tag_news.Shared
{
    public class ServiceResult<T>
    {
        public bool Sucesso { get; set; } = false;
        public string[] Mensagens { get; set; }
        public T? Dados { get; set; }
        public int StatusCode { get; set; }

        // Factory methods
        public static ServiceResult<T> Ok(T dados) =>
            new() { Sucesso = true, Dados = dados };

        public static ServiceResult<T> FalhaNegocial(string[] mensagem, int statusCode = StatusCodes.Status422UnprocessableEntity) =>
            new() { Sucesso = false, Mensagens = mensagem, StatusCode = statusCode };
        
        public static ServiceResult<T> Erro(string[] mensagem, int statusCode = StatusCodes.Status500InternalServerError) =>
            new() { Sucesso = false, Mensagens = mensagem, StatusCode = statusCode };

        public static ServiceResult<T> NaoEncontrado() =>
            new() { Sucesso = false, Mensagens = ["Recurso não encontrado"], StatusCode = StatusCodes.Status404NotFound };
    }
}
