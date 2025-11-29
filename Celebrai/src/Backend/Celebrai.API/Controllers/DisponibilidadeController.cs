using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Disponibilidade;
using Celebrai.Communication.Requests.Disponibilidade;
using Celebrai.Communication.Responses;
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
}
