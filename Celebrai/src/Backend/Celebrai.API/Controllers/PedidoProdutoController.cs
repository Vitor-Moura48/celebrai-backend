using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.PedidoProduto.Register;
using Celebrai.Communication.Requests.PedidoProduto;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.PedidoProduto;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class PedidoProdutoController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPedidoProdutoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente, RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPedidoProdutoUseCase useCase,
        [FromBody] RequestRegisterPedidoProdutoJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}