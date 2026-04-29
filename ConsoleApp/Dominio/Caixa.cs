namespace ClubedaLeiturateste.ConsoleApp.Dominio;



public class Caixa : EntidadeBase
{

    // Propriedades exclusivas da Caixa
    public string Etiqueta { get; set; }
    public string Cor { get; set; }
    public int DiasDeEmprestimo { get; set; }

    // Construtor: chamado quando você escreve "new caixa(...)"
    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
        //O id ja foi gerado pelo construtor do pai (EntidadeBase)
    }

    //Override = "estou cumprindo o contrato do pai aqui"
    public override string[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Etiqueta))
            erros += "Etiqueta é obrigatória;";

        if (DiasDeEmprestimo < 1)
            erros += "Dias de empréstimo deve ser maior que 0;";

        //Split separa os erros pelo ";" e joga fora os vazios
        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }


    // Atualiza os dados sem trocar o ID
    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Caixa nova = (Caixa)entidadeAtualizada;  //Conerte o tipo
        this.Etiqueta = nova.Etiqueta;
        this.Cor = nova.Cor;
        this.DiasDeEmprestimo = nova.DiasDeEmprestimo;
    }
}