using Celebrai.Communication.Requests.PedidoProduto;
using FluentValidation;

namespace Celebrai.Application.UseCases.PedidoProduto.Register;
public class RegisterPedidoProdutoValidator : AbstractValidator<RequestRegisterPedidoProdutoJson>
{
    public RegisterPedidoProdutoValidator()
    {
        RuleFor(pedidoProduto => pedidoProduto.IdPedido)
            .NotEmpty().WithMessage("O ID do pedido está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O ID do pedido é inválido.");

        RuleFor(pedidoProduto => pedidoProduto.IdProduto)
            .NotEmpty().WithMessage("O ID do produto está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O ID do produto é inválido.");

        RuleFor(pedidoProduto => pedidoProduto.Quantidade)
            .NotEmpty().WithMessage("A quantidade está vazia.")
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade é menor que zero.");

        RuleFor(pedidoProduto => pedidoProduto.Preco)
            .NotEmpty().WithMessage("O preço está vazio.")
            .GreaterThanOrEqualTo(0).WithMessage("O preço é menor que zero.");
        
        RuleFor(pedidoProduto => pedidoProduto.Nota)
            .GreaterThanOrEqualTo(1).WithMessage("A avaliação é menor que um.")
            .LessThanOrEqualTo(5).WithMessage("A avaliação é maior que 5.");
    }
}