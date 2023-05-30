using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Attributes;

public class VerificacaoDaPesquisaAttribute : ValidationAttribute
{
    public VerificacaoDaPesquisaAttribute() : base("A pesquisa não pôde ser realizada.")
    {

    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minCaracteres = 3;
        var maxCaracteres = 128;

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        var nome = (string)value;

        if(nome == null)
        {
            return null;
        }

        nome = nome.ToLower();
        nome = ti.ToTitleCase(nome);
        if (!nome.Replace(" ", "").All(char.IsLetter))
        {
            return new ValidationResult("A pesquisa não aceita caracteres especiais.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length < minCaracteres)
        {
            return new ValidationResult($"A pesquisa não pode conter menos de {minCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length > maxCaracteres)
        {
            return new ValidationResult($"A pesquisa não pode conter mais de {maxCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        return null;

    }
}
