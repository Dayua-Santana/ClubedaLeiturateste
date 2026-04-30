using ClubedaLeiturateste.ConsoleApp.Infraestrutura;
using ClubedaLeiturateste.ConsoleApp.Telas;

namespace ClubedaLeiturateste.ConsoleApp;

internal class Progam
{
    static void Main(string[] args)
    {
        //1 Instaciando os Repositorios(Onde os dados ficamm guardados)
        RepositorioCaixa repoCaixa = new RepositorioCaixa();
        RepositorioAmigo repoAmigo = new RepositorioAmigo();
        RepositorioRevista repoRevista = new RepositorioRevista();
        RepositorioEmprestimo repoEmprestimo = new RepositorioEmprestimo();

        //2 Instanciamos as Telas Básicas
        TelaCaixa telaCaixa = new TelaCaixa(repoCaixa);
        TelaAmigo telaAmigo = new TelaAmigo(repoAmigo);

        //3 Instaciamos as Telas que dependem de outras(Injeção de Dependência)
        // A Telarevisa deve saber sobre as caixas ppara poder cadastrar uma revista
        TelaRevista telaRevista = new TelaRevista(repoRevista, repoCaixa, telaCaixa);

        //A TelaEmprestimo é a mais complexa. pois une Amigos e Revistas
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(
        repoEmprestimo,
        repoAmigo,
        repoRevista,
        telaAmigo,
        telaRevista
        );

        // 4. Menu principal
        string opcao = "";
        while (opcao != "S")
        {
            Console.Clear();
            Console.WriteLine("=== CLUBE DA LEITURA 1.0 ===");
            Console.WriteLine("1 - Gerenciar Caixas");
            Console.WriteLine("2 - Gerenciar Amigos");
            Console.WriteLine("3 - Gerenciar Revistas");
            Console.WriteLine("4 - Gerenciar Empréstimos");
            Console.WriteLine("S - Sair");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine().ToUpper();
            if (opcao == "1") telaCaixa.Menu();
            else if (opcao == "2") telaAmigo.Menu();
            else if (opcao == "3") telaRevista.Menu();
            else if (opcao == "4") telaEmprestimo.Menu();
        }
    }
}