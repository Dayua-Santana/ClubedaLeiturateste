namespace ClubedaLeiturateste.ConsoleApp.Dominio;

using System.Collections.Generic;
public class Revista : EntidadeBase
{
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public Caixa Caixa { get; set; }  // <- referência ao objeto Caixa
    public StatusRevista Status { get; set; }

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa;

        // Por padrão, toda revista nova nasce disponivel
        Status = StatusRevista.Disponivel;
    }

    // Muda o status quando é emprestada ou devolvida
    public void Emprestar() => Status = StatusRevista.Emprestada;
    public void Devolver() => Status = StatusRevista.Disponivel;
    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        // Regra: Titulo (2-100 caracteres)
        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O titulo deve ter entre 2 e 100 caracteres.");

        //Regra: Numero da edição (número positivo)
        if (NumeroEdicao <= 0)
            erros.Add("O número da edição deve ser um número positivo.");

        //Regra: Caixa (seleção obrigatoria)
        if (Caixa == null)
            erros.Add("A revista deve obrigatoriamente estar vinculada a uma caixa");

        // Regra: Ano publicação (não pode ser no futuro extremo)
        if (AnoPublicacao > DateTime.Now.Year)
            erros.Add("O ano de publicação não pode ser maior que o ano atual.");
        return erros.ToArray();
    }
    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Revista nova = (Revista)entidadeAtualizada;

        // Atualiza as propriedades 
        this.Titulo = nova.Titulo;
        this.NumeroEdicao = nova.NumeroEdicao;
        this.AnoPublicacao = nova.AnoPublicacao;
        this.Caixa = nova.Caixa;
    }

}