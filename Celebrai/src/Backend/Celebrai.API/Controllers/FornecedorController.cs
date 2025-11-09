using Celebrai.Application.UseCases.Fornecedor.Register;
using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class FornecedorController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredFornecedorJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFornecedorUseCase useCase,
        [FromBody] RequestRegisterFornecedorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}