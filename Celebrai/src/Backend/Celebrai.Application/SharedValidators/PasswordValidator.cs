using FluentValidation;
using FluentValidation.Validators;

namespace Celebrai.Application.SharedValidators;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => throw new NotImplementedException();

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "A senha não pode ser vazia");

            return false;
        }

        if (password.Length < 6)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "A senha não pode ter menos de 6 caracteres");

            return false;
        }

        return true;
    }
}
