using Celebrai.Application.UseCases.Usuario.ConfirmEmail;
using Celebrai.Application.UseCases.Usuario.Register;
using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

public class UsuarioController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsuarioJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUsuarioUseCase useCase,
        [FromBody] RequestRegisterUsuarioJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(typeof(ResponseConfirmEmailUsuariojson), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromServices] IConfirmEmailUsuarioUseCase useCase)
    {
        var result = await useCase.Execute(token);

        return Ok(result);
    }
}
