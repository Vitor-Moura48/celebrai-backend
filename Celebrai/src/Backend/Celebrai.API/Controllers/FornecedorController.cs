using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Fornecedor.Register;
using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Fornecedor;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class FornecedorController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredFornecedorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(Domain.Enums.RoleUsuario.Cliente)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFornecedorUseCase useCase,
        [FromBody] RequestRegisterFornecedorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}