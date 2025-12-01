using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.PedidoKit.Register;
using Celebrai.Communication.Requests.PedidoKit;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.PedidoKit;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class PedidoKitController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPedidoKitJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente, RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPedidoKitUseCase useCase,
        [FromBody] RequestRegisterPedidoKitJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}