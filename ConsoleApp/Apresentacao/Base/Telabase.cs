using ClubedaLeiturateste.ConsoleApp.Dominio.Base;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura.Base;

namespace ClubedaLeiturateste.ConsoleApp.Apresentacao.Base;

public abstract class TelaBase : ITela
{
    public abstract class TelaBase : ITela
    {
        public string nomeEntidade = string.Empty;
        private RepositorioBase repositorio;

        protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
        {
            this.nomeEntidade = nomeEntidade;
            this.repositorio = repositorio;
        }

        protected abstract EntidadeBase ObterDadosCadastrais();
        public abstract void VisualizarTodos(bool deveExibirCabecalho);

        public string? ObterOpcaoMenu()
        {
            string nomeMinusculo = nomeEntidade.ToLower();

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Gestão de {nomeEntidade}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"1 - Cadastrar {nomeMinusculo}");
            Console.WriteLine($"2 - Editar {nomeMinusculo}");
            Console.WriteLine($"3 - Excluir {nomeMinusculo}");
            Console.WriteLine($"4 - Visualizar {nomeMinusculo}s");
            Console.WriteLine($"S - Voltar para o inicio");
            Console.WriteLine("----------------------------------------------");
            Console.Write(">");

            return Console.ReadLine()?.ToUpper();
        }
    }
    public string nomeEntidade = string.Empty;
    private RepositorioBase repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    protected abstract EntidadeBase ObterDadosCadastrais();
    public abstract void VisualizarTodos(bool deveExibirCabecalho);

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");
        EntidadeBase nova = ObterDadosCadastrais();
        string[] erros = nova.Validar();
        if (erros.Length > 0)
        {
            foreach (string erro in erros)
                Console.WriteLine(erro);
            Console.ResetColor();
            Console.WriteLine("---------------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Cadastrar();
            return;
        }

        repositorio.Cadastrar(nova);
        ExibirMensagem("Cadastrado com sucesso!");
    }

    public void Editar()
    {
        ExibirCabecalho($"Edição de {nomeEntidade}");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("-----------------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------------------");

        EntidadeBase novaEntidade = ObterDadosCadastrais();

        string[] erros = novaEntidade.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (string erro in erros)
                Console.WriteLine(erro);

            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Editar();
            return;
        }
        bool conseguiuEditar = repositorio.Editar(idSelecionado, novaEntidade);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Não foi possivel encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }

    public void Excluir()
    {
        ExibirCabecalho($"Exclusão de {nomeEntidade}");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("------------------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possivel encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluido com sucesso.");
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("-------------------------------------------------");
    }

    protected void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Digite ENTER para continuar....");
        Console.ReadLine();
    }
}