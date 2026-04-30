
namespace ClubedaLeiturateste.ConsoleApp.Dominio;

using System.Collections.Generic;
public class Amigo : EntidadeBase
{
    public string Nome { get; set; }
    public string NomeResponsavel { get; set; }
    public string Telefone { get; set; }

    // Um amigo pode ter vários empréstimos ao longo do tempo

    public Emprestimo?[] Emprestimos { get; set; } = new Emprestimo?[100];

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    //Adiciona um empréstimo no array do amigo 
    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        for (int i = 0; i < Emprestimos.Length; i++)
        {
            if (Emprestimos[i] == null)
            {
                Emprestimos[i] = emprestimo;
                break;
            }
        }
    }

    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo Telefone é obrigatório");

        return erros.ToArray();
    }

    public override void AtualizarRegistro(EntidadeBase novoRegistro)
    {
        Amigo atualizado = (Amigo)novoRegistro;
        this.Nome = atualizado.Nome;
        this.NomeResponsavel = atualizado.NomeResponsavel;
        this.Telefone = atualizado.Telefone;
    }
}
