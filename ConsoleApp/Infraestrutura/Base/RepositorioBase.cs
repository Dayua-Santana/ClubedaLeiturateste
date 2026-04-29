using ClubedaLeiturateste.ConsoleApp.Dominio.Base;

namespace ClubedaLeiturateste.ConsoleApp.Infraestrutura.Base;

public abstract class RepositorioBase
{
    protected EntidadeBase?[] registros = new EntidadeBase[100];

    public void Cadastrar(EntidadeBase entidade)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] == null)
            {
                registros[i] = entidade;
                break; // parou assim que achou espaço
            }
        }
    }
    //EXCLUIR: acha pelo Id e coloca null (apaga a gaveta)
    public bool Excluir(string id)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i]?.Id == id)
            {
                registros[i] = null;
                return true; // conseguiu excluir
            }
        }
        return false; // não achou
    }

    //BUSCAR: percorre até achar o Id
    public EntidadeBase? SelecionarPorId(string id)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i]?.Id == id)
                return registros[i];
        }
        return null;
    }
    //EDITAR: acha o registro e manda ele se atualizar
    public bool Editar(string id, EntidadeBase nova)
    {
        EntidadeBase? existente = SelecionarPorId(id);
        if (existente == null) return false;

        existente.AtualizarRegistro(nova); // o objeto se atualiza
        return true;
    }
    public EntidadeBase?[] SelecionarTodos() => registros;

}