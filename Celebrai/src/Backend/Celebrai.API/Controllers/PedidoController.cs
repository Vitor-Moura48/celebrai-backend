using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Pedido.Register;
using Celebrai.Communication.Requests.Pedido;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Pedido;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class PedidoController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPedidoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente, RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPedidoUseCase useCase,
        [FromBody] RequestRegisterPedidoJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}