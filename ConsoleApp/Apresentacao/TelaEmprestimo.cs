using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;
using System.Linq;

namespace ClubedaLeiturateste.ConsoleApp.Telas;

public class TelaEmprestimo
{
    private RepositorioEmprestimo repoEmprestimo;
    private RepositorioAmigo repoAmigo;
    private RepositorioRevista repoRevista;

    private TelaAmigo telaAmigo;
    private TelaRevista telaRevista;

    public TelaEmprestimo(
        RepositorioEmprestimo repoEmprestimo,
        RepositorioAmigo repoAmigo,
        RepositorioRevista repoRevista,
        TelaAmigo telaAmigo,
        TelaRevista telaRevista
    )
    {
        this.repoEmprestimo = repoEmprestimo;
        this.repoAmigo = repoAmigo;
        this.repoRevista = repoRevista;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
    }

    public void Menu()
    {
        string opcao = "";
        while (opcao != "S")
        {
            Console.Clear();
            Console.WriteLine("1 - Inserir Empréstimo");
            Console.WriteLine("2 - Visualizar Empréstimos");
            Console.WriteLine("3 - Devolver Revista");
            Console.WriteLine("4 - Listar Atrasados");
            Console.WriteLine("S - Voltar");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine()?.ToUpper() ?? "";

            if (opcao == "1")
                InserirNovoEmprestimo();
            else if (opcao == "2")
                VisualizarEmprestimos();
            else if (opcao == "3")
                DevolverRevista();
            else if (opcao == "4")
                ListarAtrasados();
        }
    }
    public void VisualizarEmprestimos()
    {
        Console.Clear();
        Console.WriteLine("=== EMPRÉSTIMOS ===\n");

        List<Emprestimo> emprestimos =
            repoEmprestimo.SelecionarTodos()
                .Cast<Emprestimo>()      // ✔ converte tipo corretamente
                .ToList();

        if (emprestimos.Count == 0)
        {
            Console.WriteLine("Nenhum empréstimo cadastrado.");
        }
        else
        {
            foreach (Emprestimo e in emprestimos)
            {
                Console.WriteLine(
                    $"ID: {e.Id} | Amigo: {e.Amigo.Nome} | Revista: {e.Revista.Titulo} | Data: {e.DataEmprestimo:d}"
                );
            }
        }

        Console.ReadLine();
    }

    public void DevolverRevista()
    {
        Console.Clear();
        Console.WriteLine("=== DEVOLUÇÃO ===\n");

        VisualizarEmprestimos();

        Console.Write("\nDigite o ID do empréstimo: ");
        string id = Console.ReadLine() ?? "";

        Emprestimo? emprestimo = repoEmprestimo.SelecionarPorId(id) as Emprestimo;

        if (emprestimo == null)
        {
            Console.WriteLine("Empréstimo não encontrado!");
            Console.ReadLine();
            return;
        }

        if (!emprestimo.EstaAberto)
        {
            Console.WriteLine("Esse empréstimo já foi devolvido!");
            Console.ReadLine();
            return;
        }

        emprestimo.RegistrarDevolucao();
        emprestimo.Revista.Devolver();

        Console.WriteLine("\nRevista devolvida com sucesso!");
        Console.ReadLine();
    }
    public void ListarAtrasados()
    {
        Console.Clear();
        Console.WriteLine("=== EMPRÉSTIMOS ATRASADOS ===\n");

        var emprestimos =
           repoEmprestimo.SelecionarTodos()
               .Cast<Emprestimo>()
               .ToList();

        var atrasados = emprestimos
             .Where(e => e.EstaAberto && e.EstaAtrasado)
             .ToList();

        if (atrasados.Count == 0)
        {
            Console.WriteLine("Nenhum empréstimo atrasado.");
        }
        else
        {
            foreach (Emprestimo e in atrasados)
            {
                Console.WriteLine(
                    $"Amigo: {e.Amigo.Nome} | Revista: {e.Revista.Titulo} | Devolver até: {e.DataDevolucaoPrevista:d}"
                );
            }
        }

        Console.ReadLine();
    }
    public void InserirNovoEmprestimo()
    {
        Console.Clear();
        //1 Selecionar amigo
        telaAmigo.VisualizarAmigos(false);
        Console.Write("\nDigite o ID do amigo: ");

        string idAmigo = Console.ReadLine() ?? "";
        Amigo? amigoSelecionado =
            repoAmigo.SelecionarPorId(idAmigo) as Amigo;

        if (amigoSelecionado == null)
        {
            Console.WriteLine("Amigo não encontrado!");
            Console.ReadLine();
            return;
        }
        //2 Selecionar a Revista
        telaRevista.VisualizarRevistas(false);
        Console.WriteLine("\nDigite o ID da revista: ");

        string idRevista = Console.ReadLine() ?? "";
        Revista? revistaSelecionada =
           repoRevista.SelecionarPorId(idRevista) as Revista;


        if (revistaSelecionada == null)
        {
            Console.WriteLine("Revista não encontrada!");
            Console.ReadLine();
            return;
        }

        //Verificar se revista está disponivel
        if (revistaSelecionada.Status != StatusRevista.Disponivel)
        {
            Console.WriteLine("Esta revista não está disponível para empréstimo!");
            Console.ReadLine();
            return;
        }

        //Cria emprestimo
        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

        //Valida
        string[] erros = novoEmprestimo.Validar();
        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string erro in erros)
            {
                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.ReadLine();
            return;
        }

        //Cadastra e vincula nos dois lados
        repoEmprestimo.Cadastrar(novoEmprestimo);
        amigoSelecionado.AdicionarEmprestimo(novoEmprestimo);
        revistaSelecionada.Emprestar(); // usa método da classe Revista

        Console.WriteLine("\nEmpréstimo realizado com sucesso!");
        Console.ReadLine();
    }
}