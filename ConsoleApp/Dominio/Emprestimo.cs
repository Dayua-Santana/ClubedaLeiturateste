using System.Security.Cryptography;
using ClubedaLeiturateste.ConsoleApp.Dominio;

public class Emprestimo
{
    public string Id { get; set}
    public Revista Revista { get; set} //qual revista
    public Amigo Amigo { get; set}      //pra quem
    public DateTime Abertura { get; set}
    public StatusEmprestimo Status { get; set}

    // Calculado na hora, não precisa ser salvo
    // Pega a data de abertura e soma os dias da caixa da revista
    public DateTime ConclusaoPrevista => Abertura.AddDays(Revista.Caixa.DiasDeEmprestimo);

    //Esta atrasado se: esta Aberto E já passou da data prevista
    public bool EstaAtrasado => Status == StatusEmprestimo.Aberto && DateTime.Now > ConclusaoPrevista;

    public Emprestimo(Revista revista, Amigo amigo)
    {
        // Gera ID igual ao EntidadeBase
        Id = Convert.ToHexString(RandomNumberGenerator.GetBytes(20))
                    .ToLower().Substring(0, 7);

        Revista = revista;
        Amigo = amigo;
    }

    public void Abrir()
    {
        Abertura = DateTime.Now;
        Status = StatusEmprestimo.Aberto;
        Revista.Emprestar(); // muda status da revista
        Amigo.AdicionarEmprestimo(this); // registra no amigo
    }

    public void Concluir()
    {
        Status = StatusEmprestimo.Concluido;
        Revista.Devolver();
    }
}