using System.Data;
using Celebrai.Communication.Requests.Fornecedor;
using FluentValidation;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public class RegisterFornecedorValidator : AbstractValidator<RequestRegisterFornecedorJson>
{
    public RegisterFornecedorValidator()
    {
        RuleFor(fornecedor => fornecedor.RaioAtuacao).GreaterThan(0).WithMessage("O raio de atuação deve ser maior que zero.");
        RuleFor(fornecedor => fornecedor.AtendimentoPresencial).NotEmpty().WithMessage("O campo de atendimento presencial é obrigatório.");
    
        RuleFor(fornecedor => fornecedor.Lograduro).NotEmpty().WithMessage("O Logradouro é obrigatório.");
        RuleFor(fornecedor => fornecedor.Numero).NotEmpty().WithMessage("O Número é obrigatório.");
        RuleFor(fornecedor => fornecedor.CEP).NotEmpty().WithMessage("O CEP é obrigatório.");

        RuleFor(fornecedor => fornecedor.TipoFornecedor).
            NotEmpty().WithMessage("O Tipo de Fornecedor é obrigatório.")
            .Must(t => t == "PF" || t == "PJ").WithMessage("O Tipo de Fornecedor deve ser 'PF' para Pessoa Física ou 'PJ' para Pessoa Jurídica.");

        RuleFor(fornecedor => fornecedor.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório para Pessoa Física.")
            .Length(11).WithMessage("O CPF deve conter 11 caracteres.")
            .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.")
            .When(f => f.TipoFornecedor == "PF");

        RuleFor(fornecedor => fornecedor.NomeCompleto)
            .NotEmpty().WithMessage("O Nome Completo é obrigatório para Pessoa Física.")
            .When(f => f.TipoFornecedor == "PF");

        RuleFor(fornecedor => fornecedor.CNPJ)
            .NotEmpty().WithMessage("O CNPJ é obrigatório para Pessoa Jurídica.")
            .Length(14).WithMessage("O CNPJ deve conter 14 caracteres.")
            .Matches("^[0-9]*$").WithMessage("O CNPJ deve conter apenas números.")
            .When(f => f.TipoFornecedor == "PJ");
            
        RuleFor(fornecedor => fornecedor.RazaoSocial)
            .NotEmpty().WithMessage("A Razão Social é obrigatória para Pessoa Jurídica.")
            .When(f => f.TipoFornecedor == "PJ");
    }
}