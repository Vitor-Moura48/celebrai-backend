using Celebrai.Application.SharedValidators;
using Celebrai.Communication.Requests.Usuario;
using FluentValidation;

namespace Celebrai.Application.UseCases.Usuario.Register;
public class RegisterUsuarioValidator : AbstractValidator<RequestRegisterUsuarioJson>
{
    public RegisterUsuarioValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage("O nome é obrigatória.");
        RuleFor(user => user.Email).NotEmpty().WithMessage("O e-mail é obrigatória.")
            .EmailAddress().WithMessage("O e-mail é inválido");
        RuleFor(user => user.Senha).SetValidator(new PasswordValidator<RequestRegisterUsuarioJson>());
        RuleFor(user => user.DataNascimento).NotEmpty().WithMessage("Data de nascimento é obrigatória.")
            .Must(BeOver18YearsOld).WithMessage("O Usuário deve ter mais de 18 anos.");
        RuleFor(user => user.CpfUsuario)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Length(11).WithMessage("CPF deve conter 11 caracteres.")
            .Matches("^[0-9]*$").WithMessage("CPF deve conter apenas números.");
    }

    private bool BeOver18YearsOld(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        int age = today.Year - dateOfBirth.Year;

        var birthdayThisYear = new DateOnly(today.Year, dateOfBirth.Month, dateOfBirth.Day);

        if (birthdayThisYear > today)
            age--;

        return age >= 18;
    }
}
