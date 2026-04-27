namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura
using ClubedaLeiturateste.ConsoleApp.Dominio;

public abstract class RepositorioBase
{
    protected EntidadeBase?[] registros = new EntidadeBase[100];

    public void Cadastrar(EntidadeBase entidade) { ... }
    public bool Editar(string Id, EntidadeBase entidade) { ... }
    public bool Excluir(string Id) { ... }
    public EntidadeBase? SelecionarPorId(string id) { ... }
    public EntidadeBase? SelecionarTodos() { ... }

}