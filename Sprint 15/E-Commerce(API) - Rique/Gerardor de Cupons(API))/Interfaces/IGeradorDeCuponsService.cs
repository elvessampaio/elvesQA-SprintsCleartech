using Gerardor_de_Cupons_API__.Data;

namespace Gerardor_de_Cupons_API__.Interfaces;

public interface IGeradorDeCuponsService
{
    CupomToken GerarCupomDeDesconto(CreateCupom createCupom);
}
