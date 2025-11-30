using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.FornecedorPedido.Register;
using Celebrai.Communication.Requests.FornecedorPedido;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.FornecedorPedido;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class FornecedorPedidoController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredFornecedorPedidoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente, RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFornecedorPedidoUseCase useCase,
        [FromBody] RequestRegisterFornecedorPedidoJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}