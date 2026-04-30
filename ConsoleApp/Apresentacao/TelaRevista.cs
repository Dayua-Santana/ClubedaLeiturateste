using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;

namespace ClubedaLeiturateste.ConsoleApp.Telas;

public class TelaRevista
{
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;
    private TelaCaixa telaCaixa;

    public TelaRevista(RepositorioRevista repoRevista, RepositorioCaixa repoCaixa, TelaCaixa telaCaixa)
    {
        this.repositorioRevista = repoRevista;
        this.repositorioCaixa = repoCaixa;
        this.telaCaixa = telaCaixa
    }

    public void Menu()
    {
        string opcao = "";
        while (opcao != "S")
        {
            Console.Clear();
            Console.WriteLine("=== GESTÂO DE REVISTAS ===");
            Console.WriteLine("1 - Cadastrar Revista");
            Console.WriteLine("2 - Visualizar Revistas");
            Console.WriteLine("3 - Editar Revista");
            Console.WriteLine("4 - Excluir Revista");
            Console.WriteLine("S - Voltar ao Menu Principal");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine().ToUpper();

            if (opcao == "1")
                InserirNovaRevista();
            else if (opcao == "2")
                VisualizarRevistas(true);
            else if (opcao == "3")
                EditarRevista();
            else if (opcao == "4")
                ExcluirRevista();
        }
    }

    private void InserirNovaRevista()
    {
        Console.Clear();
        Console.WriteLine("Cadastrando nova revista...");

        Console.Write("Título: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Número da Edição: ");
        int edicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ano de Publicação: ");
        int ano = Convert.ToInt32(Console.ReadLine());

        //Precisamos selecionar uma caixa para revista
        telaCaixa.VisualizarCaixas(false);
        Console.Write("\nDigite o ID da caixa onde esta revista ficará guardada: ");
        string idCaixa = Console.ReadLine() ?? "";

        Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

        Revista novaRevista = new Revista(titulo, edicao, ano, caixaSelecionada);

        string[] erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string erro in erros)
                Console.WriteLine(erro);
            Console.ResetColor();
            Console.ReadLine();
            return;
        }

        RepositorioRevista.Cadastrar(novaRevista);

        Console.WriteLine("\nRevista cadastrada com sucesso!");
        Console.ReadLine();
    }

    public void VisualizarRevistas(bool aguardaBotao)
    {
        Console.Clear();
        Console.WriteLine("Listando Revistas....");
        Console.WriteLine("---------------------------------------------------------------------------------------");
        Console.WriteLine("{0,-10} | {1,-20} | {2,-10} | {3,-10} | {4,-15}", "ID", "Título", "Edição", "Status", "Caixa (Cor)");
        Console.WriteLine("---------------------------------------------------------------------------------------");

        EntidadeBase[] registros = repositorioRevista.SelecionarTodos();

        bool temRevista = false;
        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Revista r = (Revista)e;
                // Exibe o ID, Título, Edição, Status e a Cor da Caixa vinculada
                Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-10} | {4,-15}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.Status, r.Caixa.Cor);
                temRevista = true;
            }
        }
        if (!temRevista)
            Console.WriteLine("Nehuma revista cadastrada.");

        if (aguardaBotao)
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }
    public void EditarRevista()
    {
        VisualizarRevistas(false);
        Console.Write("\nDigite o ID da revista que deseja editar: ");
        string id = Console.ReadLine() ?? "";

        Console.Write("Novo Título: ");
        string titulo = Console.ReadLine();
        Console.Write("Novo Número da Edição: ");
        int edicao = Convert.ToInt32(Console.ReadLine());
        Console.Write("Novo Ano de Publicação: ");
        int ano = Convert.ToInt32(Console.ReadLine());

        telaCaixa.VisualizarCaixas(false);
        Console.Write("\nDigite o ID da nova Caixa: ");
        string idCaixa = Console.ReadLine() ?? "";

        Caixa caixa = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

        Revista revistaAtualizada = new Revista(titulo, edicao, ano, caixa);

        bool conseguiu = repositorioRevista.Editar(id, revistaAtualizada);

        if (conseguiu)
            Console.WriteLine("\nRevista editada com sucesso!");
        else
        {
            Console.WriteLine("\nErro: Revista não encontrada.");
        }
        Console.ReadLine();
    }

    private void ExcluirRevista()
    {
        VisualizarRevistas(false);
        Console.Write("\nDigite o ID da revista que deseja excluir: ");
        string id = Console.ReadLine() ?? "";

        bool conseguiu = repositorioRevista.Excluir(id);

        if (conseguiu)
            Console.WriteLine("\nRevista removida com sucesso!");
        else
            Console.WriteLine("\nErro: Revista não encontrada.");

        Console.ReadLine();
    }
}