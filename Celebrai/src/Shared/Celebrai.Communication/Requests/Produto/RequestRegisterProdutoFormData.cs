using Microsoft.AspNetCore.Http;

namespace Celebrai.Communication.Requests.Produto;
public class RequestRegisterProdutoFormData : RequestProdutoJson
{
    public IFormFile Imagem { get; set; } = default!; 
}
