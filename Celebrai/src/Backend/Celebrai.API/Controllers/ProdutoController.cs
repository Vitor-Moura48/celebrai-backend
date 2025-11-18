using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Produto.Register;
using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Produto;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

[AuthenticatedUser(Domain.Enums.RoleUsuario.Fornecedor)]
public class ProdutoController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredProdutoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterProduto(
        [FromServices] IRegisterProdutoUseCase useCase,
        [FromForm] RequestRegisterProdutoFormData request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}
