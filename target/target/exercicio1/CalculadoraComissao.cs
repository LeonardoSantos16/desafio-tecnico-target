using System.Text.Json;

namespace target.exercicio1
{
    public class CalculadoraComissao
    {
        private readonly List<Vendas> _vendas;

        public CalculadoraComissao(List<Vendas> vendas)
        {
            _vendas = vendas;
        }

        public void ExibirComissoes()
        {
            var vendasPorVendedor = _vendas.GroupBy(v => v.Vendedor);

            foreach (var grupo in vendasPorVendedor)
            {
                decimal totalVendas = grupo.Sum(v => v.Valor);
                decimal totalComissao = grupo.Sum(v => CalcularComissao(v.Valor));

                Console.WriteLine($"{grupo.Key}: vendas {totalVendas:C}, comissão {totalComissao:C}");
            }
        }

        private static decimal CalcularComissao(decimal valor)
        {
            if (valor < 100m) return 0m;
            if (valor < 500m) return valor * 0.01m;
            return valor * 0.05m;
        }
    }
}
