using Celebrai.Communication.Requests.Fornecedor;
using FluentValidation;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public class RegisterFornecedorValidator : AbstractValidator<RequestRegisterFornecedorJson>
{
    public RegisterFornecedorValidator()
    {
        RuleFor(fornecedor => fornecedor)
            .Must(f => !string.IsNullOrWhiteSpace(f.RazaoSocial) || !string.IsNullOrWhiteSpace(f.NomeCompleto))
            .WithMessage("Informe a Razão Social (PJ) ou o Nome Completo (PF).");

        RuleFor(fornecedor => fornecedor.RaioAtuacao).GreaterThan(0).WithMessage("O raio de atuação deve ser maior que zero.");
        RuleFor(fornecedor => fornecedor.AtendimentoPresencial).NotEmpty().WithMessage("O campo de atendimento presencial é obrigatório.");

    }
}
