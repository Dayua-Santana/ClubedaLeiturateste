using ClubedaLeiturateste.ConsoleApp.Dominio;

namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura;

public class RepositorioRevista : RepositorioBase
{
    public Revista SelecionarPorTitulo(string TituloProcurado)
    {
        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Revista revista = (Revista)e;
                if (revista.Titulo.Equals(TituloProcurado, StringComparison.OrdinalIgnoreCase))
                {
                    return revista;
                }
            }
        }
        return null;
    }
    public Revista[] SelecionarRevistasDisponiveis()
    {
        List<Revista> disponiveis = new List<Revista>();

        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Revista revista = (Revista)e;
                if (revista.Status == StatusRevista.Disponivel)
                {
                    disponiveis.Add(revista);
                }
            }
        }
        return disponiveis.ToArray();
    }
}