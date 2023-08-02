using E_Commerce_API_.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.InteropServices;

namespace E_Commerce_API_.Attributes;

public class VerificacaoDoNomeAttribute : ValidationAttribute
{
    public VerificacaoDoNomeAttribute() : base("O nome está fora dos critérios de aceite.")
    {
        
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minCaracteres = 3;
        var maxCaracteres = 128;

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        var nome = (string)value;
        nome = nome.ToLower();
        nome = ti.ToTitleCase(nome);

        if(nome == null || nome.Replace(" ", "") == "")
        {
            return new ValidationResult("O nome não pode ficar vazio.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }
        if (!nome.Replace(" ", "").All(char.IsLetter))
        {
            return new ValidationResult("O nome não aceita caracteres especiais.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length < minCaracteres && nome.Replace(" ", "").Length != 0)
        {
            return new ValidationResult($"O nome não pode conter menos de {minCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length > maxCaracteres)
        {
            return new ValidationResult($"O nome não pode conter mais de {maxCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        return null;

    }
}
