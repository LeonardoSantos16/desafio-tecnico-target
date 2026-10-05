using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace target.exercicio2
{
    public class RepositorioEstoque
    {
        private static readonly JsonSerializerOptions Opcoes = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };

        private readonly string _caminhoEstoque;
        private readonly string _caminhoMovimentacoes;

        public RepositorioEstoque(string caminhoEstoque, string caminhoMovimentacoes)
        {
            _caminhoEstoque = caminhoEstoque;
            _caminhoMovimentacoes = caminhoMovimentacoes;
        }

        public IReadOnlyList<Produto> ObterProdutos()
        {
            var json = File.ReadAllText(_caminhoEstoque);

            var dados = JsonSerializer.Deserialize<DadosEstoque>(json, Opcoes)
                ?? throw new InvalidOperationException("Erro ao desserializar o estoque.");

            return dados.Estoque ?? new List<Produto>();
        }

        public void SalvarProdutos(IEnumerable<Produto> produtos)
        {
            var dados = new DadosEstoque(produtos.OrderBy(p => p.CodigoProduto).ToList());
            File.WriteAllText(_caminhoEstoque, JsonSerializer.Serialize(dados, Opcoes));
        }

        public IReadOnlyList<Movimentacao> ObterMovimentacoes()
        {
            if (!File.Exists(_caminhoMovimentacoes))
                return new List<Movimentacao>();

            var json = File.ReadAllText(_caminhoMovimentacoes);
            return JsonSerializer.Deserialize<List<Movimentacao>>(json, Opcoes) ?? new List<Movimentacao>();
        }

        public void SalvarMovimentacoes(IEnumerable<Movimentacao> movimentacoes)
        {
            File.WriteAllText(_caminhoMovimentacoes, JsonSerializer.Serialize(movimentacoes, Opcoes));
        }
    }
}
