using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Produto.GetById;
using Celebrai.Application.UseCases.Produto.GetList;
using Celebrai.Application.UseCases.Produto.GetListWithFilter;
using Celebrai.Application.UseCases.Produto.Register;
using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Produto;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;

public class ProdutoController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredProdutoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(Domain.Enums.RoleUsuario.Fornecedor)]
    public async Task<IActionResult> RegisterProduto(
        [FromServices] IRegisterProdutoUseCase useCase,
        [FromForm] RequestRegisterProdutoFormData request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseProdutoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProdutoById(
        [FromServices] IGetProdutoByIdUseCase useCase,
        [FromRoute] int id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<ResponseProdutoJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProdutosList(
        [FromServices] IGetListProdutoUseCase useCase,
        [FromQuery] int? page)
    {
        var result = await useCase.Execute(page);

        return Ok(result);
    }

    [HttpGet("filters")]
    [ProducesResponseType(typeof(IList<ResponseShortProdutoJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProdutosListWithFilter(
        [FromServices] IGetListWithFilterProdutoUseCase useCase,
        [FromQuery] int? page,
        [FromQuery] RequestFilterProdutoJson request)
    {
        var result = await useCase.Execute(page, request);

        return Ok(result);
    }
}
