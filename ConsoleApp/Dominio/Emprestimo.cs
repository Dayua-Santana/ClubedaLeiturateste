using ClubedaLeiturateste.ConsoleApp.Dominio;
using System;


namespace ClubedaLeiturateste.ConsoleApp.Dominio;

public class Emprestimo : EntidadeBase
{
    public Revista Revista { get; set; }
    public Amigo Amigo { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public StatusEmprestimo Status { get; set; }

    public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo)
    {
        Amigo = amigo;
        Revista = revista;
        DataEmprestimo = dataEmprestimo
    }
}