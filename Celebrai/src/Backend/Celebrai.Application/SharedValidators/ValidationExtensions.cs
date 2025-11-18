using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Celebrai.Application.SharedValidators;
public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("A senha não pode ser vazia")
            .MinimumLength(8).WithMessage("A senha não pode ter menos de 8 caracteres")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches("[0-9]").WithMessage("A senha deve conter números")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter caracteres especiais");
    }

    public static IRuleBuilderOptions<T, IFormFile> ValidImage<T>(this IRuleBuilder<T, IFormFile> rule)
    {
        return rule
            .NotNull().WithMessage("A imagem é obrigatória.")
            .Must(f => f.Length > 0).WithMessage("A imagem não pode estar vazia.")
            .Must(f => f.Length <= 2 * 1024 * 1024).WithMessage("A imagem não pode ultrapassar 2MB.")
            .Must(f => IsValidType(f)).WithMessage("Formato de imagem inválido. Use JPG ou PNG.");
    }

    private static bool IsValidType(IFormFile file)
    {
        if (file == null) return false;

        var allowedTypes = new[] { "image/jpeg", "image/png" };
        return allowedTypes.Contains(file.ContentType);
    }
}
