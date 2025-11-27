using Celebrai.Communication.Requests.Fornecedor;
using FluentValidation;

namespace Celebrai.Application.UseCases.Fornecedor.Update;
public class UpdateFornecedorValidator : AbstractValidator<RequestUpdateFornecedorJson>
{
    public UpdateFornecedorValidator()
    {
        RuleFor(fornecedor => fornecedor.RaioAtuacao).GreaterThan(0).When(f => f.RaioAtuacao.HasValue).WithMessage("O raio de atuação deve ser maior que zero.");
        
        RuleFor(fornecedor => fornecedor.Lograduro).NotEmpty().WithMessage("O Logradouro é obrigatório.");
        RuleFor(fornecedor => fornecedor.Numero).NotEmpty().WithMessage("O Número é obrigatório.");
        RuleFor(fornecedor => fornecedor.CEP).NotEmpty().WithMessage("O CEP é obrigatório.");
    }
}
