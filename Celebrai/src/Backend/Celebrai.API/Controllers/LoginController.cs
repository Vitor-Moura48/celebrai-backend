using Celebrai.Application.UseCases.Login.DoLogin;
using Celebrai.Communication.Requests.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

public class LoginController : CelebraiBaseController
{
    [HttpPost]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
