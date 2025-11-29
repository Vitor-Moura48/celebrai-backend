using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Disponibilidade.GetHoursFornecedor;
using Celebrai.Application.UseCases.Disponibilidade.Register;
using Celebrai.Communication.Requests.Disponibilidade;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Disponibilidade;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

public class DisponibilidadeController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(Domain.Enums.RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterDisponibilidadeUseCase useCase, 
        [FromBody] RequestRegistedDisponibilidadeJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpGet]
    [Route("fornecedor/{idFornecedor:guid}")]
    [ProducesResponseType(typeof(IList<ResponseDisponibilidadeJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [AuthenticatedUser(Domain.Enums.RoleUsuario.Cliente, Domain.Enums.RoleUsuario.Fornecedor)]
    public async Task<IActionResult> GetByFornecedorId(
        [FromServices] IGetHoursFornecedorUseCase useCase,
        [FromRoute] Guid idFornecedor)
    {
        var result = await useCase.Execute(idFornecedor);

        return Ok(result);
    }
}
