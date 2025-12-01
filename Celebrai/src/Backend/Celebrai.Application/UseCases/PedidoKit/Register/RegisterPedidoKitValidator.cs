using Celebrai.Communication.Requests.PedidoKit;
using FluentValidation;

namespace Celebrai.Application.UseCases.PedidoKit.Register;
public class RegisterPedidoKitValidator : AbstractValidator<RequestRegisterPedidoKitJson>
{
    public RegisterPedidoKitValidator()
    {
        RuleFor(pedidoKit => pedidoKit.IdPedido)
            .NotEmpty().WithMessage("O ID do pedido está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O ID do pedido é inválido.");

        RuleFor(pedidoKit => pedidoKit.IdKit)
            .NotEmpty().WithMessage("O ID do kit está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O ID do kit é inválido.");

        RuleFor(pedidoKit => pedidoKit.Quantidade)
            .NotEmpty().WithMessage("A quantidade está vazia.")
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade é menor que zero.");

        RuleFor(pedidoKit => pedidoKit.Preco)
            .NotEmpty().WithMessage("O preço está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O preço é menor que zero.");
        
        RuleFor(pedidoKit => pedidoKit.Nota)
            .GreaterThanOrEqualTo(1).WithMessage("A avaliação é menor que um.")
            .LessThanOrEqualTo(5).WithMessage("A avaliação é maior que 5.");
    }
}