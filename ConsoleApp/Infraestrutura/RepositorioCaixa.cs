using System;
using ClubedaLeiturateste.ConsoleApp.Dominio;

namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura;


public class RepositorioCaixa : RepositorioBase
{
    public Caixa SelecionarPorCor(string corProcurada)
    {
        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Caixa caixa = (Caixa)e;
                if (caixa.Cor.Equals(corProcurada, StringComparison.OrdinalIgnoreCase))
                {
                    return caixa;
                }
            }
        }
        return null;
    }
}