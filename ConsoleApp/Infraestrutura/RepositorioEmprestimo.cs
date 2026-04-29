using ClubedaLeiturateste.ConsoleApp.Dominio;

namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura;

public class RepositorioEmprestimo : RepositorioBase
{
    public Emprestimo[] SelecionarAtrasados()
    {
        List<Emprestimo> atrasados = new List<Emprestimo>();
        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Emprestimo emp = (Emprestimo)e;
                if (emp.EstaAtrasado)
                    atrasados.Add(emp);
            }
        }
        return atrasados.ToArray();
    }

}