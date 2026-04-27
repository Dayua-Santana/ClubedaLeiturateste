
namespace ClubedaLeiturateste.ConsoleApp.Dominio;

public class Amigo : EntidadeBase
{
    public string Nome { get; set}
    public string NomeResponsavel { get; set}
    public string Telefone { get; set}

    // Um amigo pode ter vários empréstimos ao longo do tempo

    public Emprestimo?[] Emprestimos { get; set} = new Emprestimo[100];

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    //Adiciona um empréstimo no array do amigo 
    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        for (int = 0; int < Emprestimos.Length; int++)
        {
            if (emprestimo[i] == null)
            {
                Emprestimos[i] = emprestimo;
                break;
            }
        }
    }

    public override string[] Validar() { ... }
    public override void AtualizarRegistro(EntidadeBase e) { ... }
}