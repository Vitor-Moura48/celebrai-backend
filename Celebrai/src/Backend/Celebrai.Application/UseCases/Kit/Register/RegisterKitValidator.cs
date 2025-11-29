using Celebrai.Communication.Requests.Kit;
using Celebrai.Domain.Repositories.Produto;
using FluentValidation;

namespace Celebrai.Application.UseCases.Kit.Register;

public class RegisterKitValidator : AbstractValidator<RequestKitJson>
{
    public RegisterKitValidator(IProdutoReadOnlyRepository produtoReadOnlyRepository)
    {
        RuleFor(kit => kit.Nome).NotEmpty().WithMessage("O nome é obrigatório.");
        RuleFor(kit => kit.Descricao).NotEmpty().WithMessage("A descrição é obrigatória.");
        RuleFor(kit => kit.KitPreco).GreaterThan(0).WithMessage("O preço do kit deve ser maior que zero.");
        RuleForEach(kit => kit.ProdutosIds)
                    .MustAsync(async (produtoId, cancellation) =>
                    {
                       var produto = await produtoReadOnlyRepository.GetProdutoByIdentifier(produtoId);
                       return produto != null; 
                    })
                    .WithMessage("O produto com o ID {PropertyValue} não foi encontrado.");
    }
}
