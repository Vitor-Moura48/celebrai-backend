using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Usuario.ChangeAddress;
using Celebrai.Application.UseCases.Usuario.ChangePassword;
using Celebrai.Application.UseCases.Usuario.ConfirmEmail;
using Celebrai.Application.UseCases.Usuario.Profile;
using Celebrai.Application.UseCases.Usuario.Register;
using Celebrai.Application.UseCases.Usuario.Update;
using Celebrai.Application.UseCases.Usuario.UpdateEmail;
using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

public class UsuarioController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUsuarioJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUsuarioUseCase useCase,
        [FromBody] RequestRegisterUsuarioJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(typeof(ResponseConfirmEmailUsuariojson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromServices] IConfirmEmailUsuarioUseCase useCase)
    {
        var result = await useCase.Execute(token);

        return Ok(result);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> Update([FromServices] IUpdateUsuarioUseCase usecase, [FromBody] RequestUpdateUsuarioJson request)
    {
        await usecase.Execute(request);

        return NoContent();
    }

    [HttpPut("update/email")]
    [ProducesResponseType(typeof(ResponseRegisteredUsuarioJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> UpdateEmail([FromServices] IUpdateEmailUsuarioUseCase usecase,[FromBody] RequestEmailUsuarioJson request)
    {
        var result = await usecase.Execute(request);

        return Ok(result);
    }

    [HttpPut("update/change-address")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> ChangeAddress(
        [FromServices] IChangeAddressUsuarioUseCase usecase, 
        [FromBody] RequestChangeAddressUsuarioJson request)
    {
        await usecase.Execute(request); 
        
        return NoContent();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUsuarioProfileJson), StatusCodes.Status200OK)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> GetProfile([FromServices] IGetUsuarioProfileUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }
}
