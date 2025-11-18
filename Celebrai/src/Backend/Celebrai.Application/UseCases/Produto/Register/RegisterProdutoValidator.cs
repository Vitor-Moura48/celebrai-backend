using Celebrai.Application.SharedValidators;
using Celebrai.Communication.Requests.Produto;
using FluentValidation;

namespace Celebrai.Application.UseCases.Produto.Register;
public class RegisterProdutoValidator : AbstractValidator<RequestRegisterProdutoFormData>
{
    public RegisterProdutoValidator()
    {
        RuleFor(product => product.Nome).NotEmpty().WithMessage("O nome é obrigatória.");
        RuleFor(product => product.Descricao).NotEmpty().WithMessage("A descrição é obrigatória.");
        RuleFor(product => product.Imagem).ValidImage();
    }
}
