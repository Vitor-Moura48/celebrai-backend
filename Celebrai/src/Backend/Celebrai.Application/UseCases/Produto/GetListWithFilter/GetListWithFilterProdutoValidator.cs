using Celebrai.Communication.Requests.Produto;
using FluentValidation;

namespace Celebrai.Application.UseCases.Produto.GetListWithFilter;

public class GetListWithFilterProdutoValidator : AbstractValidator<RequestFilterProdutoJson>
{
    public GetListWithFilterProdutoValidator()
    {
        RuleFor(x => x.Nome)
            .MaximumLength(80).WithMessage("O nome do produto deve ter no máximo 80 caracteres.");
        RuleFor(x => x.Categoria)
            .MaximumLength(80).WithMessage("O nome da categoria deve ter no máximo 80 caracteres.");
    }
}
