using target.exercicio1;
using target.exercicio2;
using target.exercicio3;

namespace target
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorio = new RepositorioVendas(Path.Combine(AppContext.BaseDirectory, "exercicio1", "vendas.json"));
            var vendas = repositorio.ObterTodas();

            var repositorioVendas = new CalculadoraComissao(vendas);
            repositorioVendas.ExibirComissoes();
            //var exercicio2 = new ControleEstoque(Path.Combine(AppContext.BaseDirectory, "exercicio2", "estoque.json"));
            //exercicio2.Executar();
            //var exercicio3 = new CalculadoraJuros();
            //exercicio3.Executar();
        }
    }
}
