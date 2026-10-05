using target.exercicio1;

namespace target
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorioVendas = new CalculadoraComissao();
            repositorioVendas.ExibirComissoes();

        }
    }
}
