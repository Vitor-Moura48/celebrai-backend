using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        var teste = "teste";
        return teste;
    }
}
