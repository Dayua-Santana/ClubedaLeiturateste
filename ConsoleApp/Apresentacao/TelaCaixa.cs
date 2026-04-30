using System;
using ClubedaLeiturateste.ConsoleApp.Dominio;
using ClubedaLeiturateste.ConsoleApp.Infraestrutura;

namespace ClubedaLeiturateste.ConsoleApp.Telas;

public class TelaCaixa
{
    private RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }
    public void Menu()
    {
        string opcao = "";
        while (opcao != "S")
        {
            Console.Clear();
            Console.WriteLine("=== GESTÃO DE CAIXAS ===");
            Console.WriteLine("1 - Cadastrar Caixa");
            Console.WriteLine("2 - Visualizar Caixas");
            Console.WriteLine("3 - Editar Caixa");
            Console.WriteLine("4 - Excluir Caixa");
            Console.WriteLine("S - Voltar ao Menu Principal");
            Console.Write("\nOpção: ");
            opcao = Console.ReadLine()!.ToUpper();

            if (opcao == "1") InserirNovaCaixa();
            else if (opcao == "2") VisualizarCaixas(true);
            else if (opcao == "3") EditarCaixa();
            else if (opcao == "4") ExcluirCaixa();
        }
    }
    private void InserirNovaCaixa()
    {
        Console.Clear();
        Console.WriteLine("Cadastrando nova caixa...");

        Console.Write("Cor: ");
        string cor = Console.ReadLine()!;

        Console.Write("Etiqueta: ");
        string etiqueta = Console.ReadLine()!;

        Console.Write("Dias de Empréstimo permitidos: ");
        int dias = Convert.ToInt32(Console.ReadLine());

        Caixa novaCaixa = new Caixa(etiqueta, cor, dias);

        string[] erros = novaCaixa.Validar();

        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string erro in erros)
                Console.WriteLine(erro);
            Console.ResetColor();
            Console.ReadLine();
            return;
        }

        repositorioCaixa.Cadastrar(novaCaixa);

        Console.WriteLine("\nCaixa cadastrada com sucesso!");
        Console.ReadLine();
    }

    public void VisualizarCaixas(bool aguardaBotao)
    {
        Console.Clear();
        Console.WriteLine("Listando Caixas...");
        Console.WriteLine("---------------------------------------------------------------------------");
        Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-10}", "ID", "Cor", "Etiqueta", "Dias");
        Console.WriteLine("---------------------------------------------------------------------------");

        EntidadeBase?[] registros = repositorioCaixa.SelecionarTodos();

        bool temCaixa = false;
        foreach (EntidadeBase e in registros)
        {
            if (e != null)
            {
                Caixa c = (Caixa)e;
                Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-10}",
                    c.Id, c.Cor, c.Etiqueta, c.DiasDeEmprestimo);
                temCaixa = true;
            }
        }
        if (!temCaixa)
            Console.WriteLine("Nenhuma caixa cadastrada.");

        if (aguardaBotao)
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    private void EditarCaixa()
    {
        VisualizarCaixas(false);
        Console.Write("\nDigite o ID da caixa que deseja editar: ");
        string id = Console.ReadLine()!;

        Console.Write("Nova Cor: ");
        string cor = Console.ReadLine()!;
        Console.Write("Nova Etiqueta: ");
        string etiqueta = Console.ReadLine()!;
        Console.Write("Novos Dias de Empréstimo: ");
        int dias = Convert.ToInt32(Console.ReadLine());

        Caixa caixaAtualizada = new Caixa(etiqueta, cor, dias);

        bool conseguiu = repositorioCaixa.Editar(id, caixaAtualizada);

        if (conseguiu)
            Console.WriteLine("\nCaixa editada com sucesso!");
        else
            Console.WriteLine("\nErro: Caixa não encontrada.");

        Console.ReadLine();
    }

    private void ExcluirCaixa()
    {
        VisualizarCaixas(false);
        Console.WriteLine("\nDigite o ID da caixa que deseja excluir: ");
        string id = Console.ReadLine();

        bool conseguiu = repositorioCaixa.Excluir(id);

        if (conseguiu)
            Console.WriteLine("\nCaixa removida com sucesso!");
        else
            Console.WriteLine("\nErro: Caixa não encontrada.");

        Console.ReadLine();
    }
}
