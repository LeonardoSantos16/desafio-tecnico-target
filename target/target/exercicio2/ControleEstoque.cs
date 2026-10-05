using System.Text.Encodings.Web;
using System.Text.Json;

namespace target.exercicio2
{
    public class ControleEstoque
    {
        private readonly string _caminhoJson;
        private readonly List<Produto> _produtos;
        private int _proximoId = 1;

        public ControleEstoque(string caminhoJson)
        {
            _caminhoJson = caminhoJson;
            var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = File.ReadAllText(caminhoJson);
            _produtos = JsonSerializer.Deserialize<DadosEstoque>(json, opcoes)!.Estoque;
        }

        public int Movimentar(int codigoProduto, int quantidade, string descricao)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto)
                ?? throw new ArgumentException($"Produto {codigoProduto} não encontrado.");

            if (produto.Estoque + quantidade < 0)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}.");

            produto.Estoque += quantidade;
            Salvar();
            int id = _proximoId++;
            Console.WriteLine($"Movimentação #{id} - {descricao} - {produto.DescricaoProduto}: {quantidade:+#;-#}");

            return produto.Estoque;
        }

        private void Salvar()
        {
            var dados = new DadosEstoque { Estoque = _produtos };
            File.WriteAllText(_caminhoJson, JsonSerializer.Serialize(dados, Opcoes));
        }

        private static readonly JsonSerializerOptions Opcoes = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public void Executar()
        {
            while (true)
            {
                Console.Write("\nCódigo do produto (Enter para sair): ");
                var entrada = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entrada)) return;

                Console.Write("Quantidade (positiva = entrada, negativa = saída): ");
                int quantidade = int.Parse(Console.ReadLine()!);

                Console.Write("Descrição: ");
                string descricao = Console.ReadLine()!;

                try
                {
                    int estoqueFinal = Movimentar(int.Parse(entrada), quantidade, descricao);
                    Console.WriteLine($"Estoque final: {estoqueFinal}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
    }
}
