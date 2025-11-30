using System.Data;
using Celebrai.Communication.Requests.Pedido;
using FluentValidation;

namespace Celebrai.Application.UseCases.Pedido.Register;
public class RegisterPedidoValidator : AbstractValidator<RequestRegisterPedidoJson>
{
    public RegisterPedidoValidator()
    {
        RuleFor(pedido => pedido.ValorTotal).GreaterThan(0).WithMessage("O valor total deve ser maior que zero.");
        RuleFor(fornecedor => fornecedor.ValorFrete).GreaterThanOrEqualTo(0).WithMessage("O valor do frete não pode ser inferior a zero.");
    }
}