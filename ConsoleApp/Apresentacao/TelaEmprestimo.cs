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

    public void InserirNovoEmprestimo()
    {
        Console.Clear();
        //1 Selecionar amigo
        telaAmigo.VisualizarAmigos(false);
        Console.Write("\nDigite o ID do amigo que está pegando a revista: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());
        Amigo amigoSelecionado = (Amigo)repoAmigo.SelecionarPorId(idAmigo);

        //2 Selecionar a Revista
        telaRevista.VisualizarRevistas(false);
        Console.WriteLine("\nDigite o ID da revista: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());
        Revista revistaSelecionada = (Revista)repoRevista.SelecionarPorId(idRevista);

        //3 Criar Emprestimo
        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

        //4. Ligar Tudo (A parte que você perguntou!)
        repoEmprestimo.Cadastrar(novoEmprestimo);
        amigoSelecionado.AdicionarEmprestimo(novoEmprestimo);
        revistaSelecionada.Emprestar();

        Console.WriteLine("\nEmpréstimo realizado com sucesso!");
        Console.ReadLine();


    }
}