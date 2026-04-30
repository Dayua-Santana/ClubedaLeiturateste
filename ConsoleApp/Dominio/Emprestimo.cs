using ClubedaLeiturateste.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

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
        DataEmprestimo = dataEmprestimo;
        Status = StatusEmprestimo.Aberto;
    }

    //Calculando: Pega a data de abertura e soma os dias da caixa da revista
    public DateTime DataDevolucaoPrevista => DataEmprestimo.AddDays(Revista.Caixa.DiasDeEmprestimo);
    public bool EstaAtrasado => Status == StatusEmprestimo.Aberto && DateTime.Now > DataDevolucaoPrevista;

    public bool EstaAberto => Status == StatusEmprestimo.Aberto;

    public void RegistrarDevolucao()
    {
        DataDevolucao = DateTime.Now;
        Status = StatusEmprestimo.Concluido;
    }

    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        if (Amigo == null)
        {
            erros.Add("O empréstimo precisa de um amigo vinculado.");
        }

        if (Revista == null)
            erros.Add("O empréstimo  precisa de um revista vinculada.");

        if (DataEmprestimo > DateTime.Now)
            erros.Add("A data do empréstimo não pode ser no futuro.");
        return erros.ToArray();
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Emprestimo atualizado = (Emprestimo)entidadeAtualizada;
        this.Amigo = atualizado.Amigo;
        this.Revista = atualizado.Revista;
        this.DataEmprestimo = atualizado.DataEmprestimo;
        this.Status = atualizado.Status;
    }
}