using ClubedaLeiturateste.ConsoleApp.Apresentacao.;
using ClubedaLeiturateste.ConsoleApp.Apresentacao.Base;
using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Dominio.Base;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;

namespace ClubedaLeiturateste.ConsoleApp.Apresentacao;

public class TelaCaixa : TelaBase
{
    private RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa rC) : base("Caixa", rC)
    {
        repositorioCaixa = rC;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Caixas")
    }
}
