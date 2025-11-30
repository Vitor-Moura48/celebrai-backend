using Celebrai.Communication.Requests.FornecedorPedido;
using FluentValidation;

namespace Celebrai.Application.UseCases.FornecedorPedido.Register;
public class RegisterFornecedorPedidoValidator : AbstractValidator<RequestRegisterFornecedorPedidoJson>
{
    public RegisterFornecedorPedidoValidator()
    {
        RuleFor(fornecedor => fornecedor.IdFornecedor).NotEmpty().WithMessage("O ID do fornecedor está vazio.");
        RuleFor(pedido => pedido.IdPedido).GreaterThanOrEqualTo(0).WithMessage("O ID do pedido é inválido.");
    }
}