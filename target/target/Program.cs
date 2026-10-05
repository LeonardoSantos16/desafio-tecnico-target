using target.exercicio1;
using target.exercicio2;
using target.exercicio3;

namespace target
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Desafio Técnico Target ===");
                Console.WriteLine("1 - Comissão de vendedores");
                Console.WriteLine("2 - Movimentação de estoque");
                Console.WriteLine("3 - Cálculo de juros");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha um exercício: ");

                var opcao = Console.ReadLine()?.Trim();
                if (opcao == "0") return;

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": ExecutarExercicio1(); break;
                        case "2": ExecutarExercicio2(); break;
                        case "3": ExecutarExercicio3(); break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                }

                Console.WriteLine();
                Console.Write("Pressione qualquer tecla para voltar ao painel...");
                Console.ReadKey(intercept: true);
            }
        }

        private static void ExecutarExercicio1()
        {
            var caminho = Path.Combine(AppContext.BaseDirectory, "exercicio1", "vendas.json");
            var vendas = new RepositorioVendas(caminho).ObterTodas();

            new CalculadoraComissao(vendas).ExibirComissoes();
        }

        private static void ExecutarExercicio2()
        {
            var pasta = Path.Combine(AppContext.BaseDirectory, "exercicio2");
            var repositorio = new RepositorioEstoque(
                Path.Combine(pasta, "estoque.json"),
                Path.Combine(pasta, "movimentacoes.json"));

            new MenuEstoque(new ControleEstoque(repositorio)).Executar();
        }

        private static void ExecutarExercicio3()
        {
            new CalculadoraJuros().Executar();
        }
    }
}
