using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Fornecedor.Register;
using Celebrai.Application.UseCases.Fornecedor.Delete;
using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class FornecedorController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredFornecedorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFornecedorUseCase useCase,
        [FromBody] RequestRegisterFornecedorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteFornecedorUseCase useCase)
    {
        await useCase.Execute();

        return NoContent();
    }
}