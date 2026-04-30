using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;

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
            Console.WriteLine("=== GESTÃO DE EMPRÉSTIMOS ===");
            Console.WriteLine("1 - Inserir Empréstimo");
            Console.WriteLine("S - Voltar");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine()?.ToUpper() ?? "";

            if (opcao == "1") InserirNovoEmprestimo();
        }
    }
    public void InserirNovoEmprestimo()
    {
        Console.Clear();
        //1 Selecionar amigo
        telaAmigo.VisualizarAmigos(false);
        Console.Write("\nDigite o ID do amigo: ");

        string idAmigo = Console.ReadLine() ?? "";
        Amigo amigoSelecionado = (Amigo)repoAmigo.SelecionarPorId(idAmigo);

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
        Revista revistaSelecionada = (Revista)repoRevista.SelecionarPorId(idRevista);

        if (revistaSelecionada == null)
        {
            Console.WriteLine("Revista não encontrada!");
            Console.ReadLine();
            return;
        }

        //3 Criar Emprestimo
        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

        //4. Ligar Tudo (A parte que você perguntou!)
        repoEmprestimo.Cadastrar(novoEmprestimo);

        // Verifique se esses métodos existem na sua classe Amigo e Revista:
        revistaSelecionada.Status = StatusRevista.Emprestada;

        Console.WriteLine("\nEmpréstimo realizado com sucesso!");
        Console.ReadLine();
    }
}