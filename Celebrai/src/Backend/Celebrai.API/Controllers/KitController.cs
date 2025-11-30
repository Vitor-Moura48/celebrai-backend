using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Kit.GetById;
using Celebrai.Application.UseCases.Kit.Register;
using Celebrai.Application.UseCases.Kit.GetList;
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

        [HttpGet]
        [ProducesResponseType(typeof(IList<ResponseKitJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKitsList(
            [FromServices] IGetListKitUseCase useCase,
            [FromQuery] int? page)
        {
            var result = await useCase.Execute(page);
            
            return Ok(result);   
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseKitJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetKitById(
            [FromServices] IGetKitByIdUseCase useCase,
            [FromRoute] int id)
        {
            var result = await useCase.Execute(id);

            return Ok(result);   
        }
    }
}