using target.exercicio1;
using target.exercicio2;

namespace target
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var repositorioVendas = new CalculadoraComissao();
            //repositorioVendas.ExibirComissoes();
            var exercicio2 = new ControleEstoque(Path.Combine(AppContext.BaseDirectory, "exercicio2", "estoque.json"));
            exercicio2.Executar();
        }
    }
}
