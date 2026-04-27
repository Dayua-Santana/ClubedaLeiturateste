namespace ClubedaLeiturateste.ConsoleApp.Dominio

public class Revista : EntidadeBase
{
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public Caixa Caixa { get; set}  // <- referência ao objeto Caixa
    public StatusRevista Status { get; set}

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa; // Garante que nenhuma revista fique "solta" no sistema sem saber aonde deve fica guardada
        // Status começa como Disponivel por padrão (valor 0 do enum)
    }

    // Muda o status quando é emprestada ou devolvida
    public void Emprestar() => Status = StatusRevista.Emprestada;
    public void Devolver() => Status = StatusRevista.Disponivel;
    public override string[] Validar() { ... }
    public override void AtualizarRegistro(EntidadeBase e) { ... }

}