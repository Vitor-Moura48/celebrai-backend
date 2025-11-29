using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Kit.Register;
using Celebrai.Communication.Requests.Kit;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Kit;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers
{
    public class KitController : CelebraiBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredKitJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser(Domain.Enums.RoleUsuario.Fornecedor)]
        public async Task<IActionResult> RegisterKit(
            [FromServices] IRegisterKitUseCase useCase,
            [FromBody] RequestKitJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}