using System.Text.Json;

namespace target.exercicio1
{
    public class CalculadoraComissao
    {
        private readonly List<Vendas> _vendas;

        public CalculadoraComissao()
        {
            var vendas = CarregarVendas(Path.Combine(AppContext.BaseDirectory, "exercicio1", "sales.json"));
            _vendas = vendas;
        }

        private static List<Vendas> CarregarVendas(string path)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = File.ReadAllText(path);

            var data = JsonSerializer.Deserialize<DadosDaVenda>(json, options)
                ?? throw new InvalidOperationException("Erro ao desserializar as vendas.");

            return data.Vendas;
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
