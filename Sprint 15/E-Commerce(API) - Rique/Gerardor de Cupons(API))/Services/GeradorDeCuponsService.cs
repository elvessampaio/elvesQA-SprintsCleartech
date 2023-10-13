using Gerardor_de_Cupons_API__.Data;
using Gerardor_de_Cupons_API__.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gerardor_de_Cupons_API__.Services;

public class GeradorDeCuponsService : IGeradorDeCuponsService
{
    public CupomToken GerarCupomDeDesconto(CreateCupom createCupom)
    {
        if(createCupom.Valor <= 0)
        {
            throw new ArgumentException("O valor do cupom não pode ser 0 ou menor.");
        }

        Claim[] infosDoCupom = new Claim[]
        {
            new Claim ("tipoDeDesconto", createCupom.TipoDeDesconto),
            new Claim ("valor", createCupom.Valor.ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xpTo0123vfnx465d3cCDG509h4fsgsad400"));

        var credentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var cupom = new JwtSecurityToken(
            claims: infosDoCupom,
            signingCredentials: credentials);

        var cupomString  = new JwtSecurityTokenHandler().WriteToken(cupom);

        return new CupomToken(cupomString);
    }
}
