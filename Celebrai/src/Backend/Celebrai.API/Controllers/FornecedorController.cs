using Celebrai.API.Attributes;
using Celebrai.Application.UseCases.Fornecedor.Register;
using Celebrai.Application.UseCases.Fornecedor.Delete;
using Celebrai.Application.UseCases.Fornecedor.Update;
using Celebrai.Application.UseCases.Fornecedor.Profile;
using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses;
using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Controllers;
public class FornecedorController : CelebraiBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredFornecedorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterFornecedorUseCase useCase,
        [FromBody] RequestRegisterFornecedorJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AuthenticatedUser(RoleUsuario.Cliente)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteFornecedorUseCase useCase)
    {
        await useCase.Execute();

        return Ok(new ResponseDeletedFornecedorJson { Message = "Fornecedor Desativado com sucesso." });
    }

    [HttpPut]
    [ProducesResponseType(typeof(ResponseUpdateFornecedorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateFornecedorUseCase useCase,
        [FromBody] RequestUpdateFornecedorJson request)
    {
        await useCase.Execute(request);

        return Ok(new ResponseUpdateFornecedorJson { Message = "Fornecedor atualizado com sucesso." });
    }

    [HttpGet("get-fornecedor")]
    [ProducesResponseType(typeof(ResponseFornecedorProfileJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser(RoleUsuario.Fornecedor)]
    public async Task<IActionResult> Profile(
        [FromServices] IGetFornecedorProfileUseCase useCase
    )
    {
        var result = await useCase.Execute();
        return Ok(result);
    }
}