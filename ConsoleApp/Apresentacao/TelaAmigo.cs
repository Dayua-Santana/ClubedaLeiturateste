using System.Runtime.CompilerServices;

namespace ClubedaLeiturateste.ConsoleApp.Telas;

using System.Runtime.CompilerServices;
using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;

public class TelaAmigo
{
    private RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorio)
    {
        this.repositorioAmigo = repositorio;
    }

    public void Menu()
    {
        string opcao = "";
        while (opcao != "S")
        {
            Console.Clear();
            Console.WriteLine("== GESTÃO DE AMIGOS ===");
            Console.WriteLine("1 - Cadastrar Amigo");
            Console.WriteLine("2 - Visualizaar Amigos");
            Console.WriteLine("3 - Editar Amigo");
            Console.WriteLine("4 - Excluir Amigos");
            Console.WriteLine("S - Voltar ao Menu Principal");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine().ToUpper();

            if (opcao == "1") InserirNovoAmigo();
            else if (opcao == "2") VisualizarAmigos(true);
            else if (opcao == "3") EditarAmigo();
            else if (opcao == "4") ExcluirAmigo();
        }
    }
    private void InserirNovoAmigo()
    {
        Console.Clear();
        Console.WriteLine("Cadastrando novo amigo...");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Telefone: ");
        string telefone = Console.ReadLine();

        Console.Write("Nome do Responsável: ");
        string responsavel = Console.ReadLine();

        //AQUI É ONDE A ENTIDADE È CRIADA
        Amigo novoAmigo = new Amigo(nome, responsavel, telefone);

        //Validação dos dados
        string[] erros = novoAmigo.Validar();
        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string erro in erros) Console.WriteLine(erro);
            Console.ResetColor();
            Console.ReadLine();
            return;
        }


        //ENVIAR PARA O REPOSITORIO
        repositorioAmigo.Cadastrar(novoAmigo);

        Console.WriteLine(" Amigo cadastrado com sucesso!");
        Console.ReadLine();
    }
    public void VisualizarAmigos(bool aguardaBotao)
    {
        Console.Clear();
        Console.WriteLine("Listando Amigos...");
        Console.WriteLine("-------------------------------------------------------------------------------");
        Console.WriteLine("{0, -5} | {1, 20} | {2, -20} | {3, -15}", "ID", "Nome", "Responsável", "Telefone");
        Console.WriteLine("-------------------------------------------------------------------------------");

        //BUSCA OS DADOS DO REPOSITÓRIO
        EntidadeBase[] registros = repositorioAmigo.SelecionarTodos();

        bool temAmigo = false;
        foreach (EntidadeBase entidadebase in registros)
        {
            if (entidadebase != null)
            {
                Amigo amigo = (Amigo)entidadebase;
                Console.WriteLine("{0, -5} | {1, -20} | {2, -20} | {3, -15}", amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone);
                temAmigo = true;
            }
        }
        if (!temAmigo)
            Console.WriteLine("Nenhum amigo cadastrado até momento.");

        if (aguardaBotao)
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    private void EditarAmigo()
    {
        VisualizarAmigos(false);
        Console.Write("\nDigite o ID do amigo que deseja editar: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Novo nome: ");
        string nome = Console.ReadLine();
        Console.Write("Novo Responsável: ");
        string responsavel = Console.ReadLine();
        Console.Write("Novo Telefone: ");
        string telefone = Console.ReadLine();

        Amigo amigoAtualizado = new Amigo(nome, responsavel, telefone);

        bool conseguiuEditar = repositorioAmigo.Editar(id, amigoAtualizado);

        if (conseguiuEditar)
            Console.WriteLine("\nAmigo editado com sucesso!");
        else
            Console.WriteLine("\nErro: Amigo não encontrado.");

        Console.ReadLine();
    }
    private void ExcluirAmigo()
    {
        VisualizarAmigos(false);
        Console.Write("\nDigite o ID do amigo que deseja excluir: ");
        int id = Convert.ToInt32(Console.ReadLine());

        bool conseguiuExcluir = repositorioAmigo.Excluir(id);

        if (conseguiuExcluir)
            Console.WriteLine("\nAmigo removido com sucesso!");
        else
            Console.WriteLine("\nErro: Amigo não encontrado");

        Console.ReadLine();
    }
}


