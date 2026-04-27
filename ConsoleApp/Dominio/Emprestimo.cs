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
    public bool EstaAtrasado => StatusEmprestimo.Aberto && DateTime.Now > ConclusaoPrevista;
}