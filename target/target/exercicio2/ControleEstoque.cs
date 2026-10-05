using System.Text.Encodings.Web;
using System.Text.Json;

namespace target.exercicio2
{
    public class ControleEstoque
    {
        private readonly RepositorioEstoque _repositorio;
        private readonly Dictionary<int, Produto> _produtos;
        private readonly List<Movimentacao> _movimentacoes;
        private int _proximoId;

        public ControleEstoque(RepositorioEstoque repositorio)
        {
            _repositorio = repositorio;
            _produtos = repositorio.ObterProdutos().ToDictionary(p => p.CodigoProduto);
            _movimentacoes = repositorio.ObterMovimentacoes().ToList();
            _proximoId = _movimentacoes.Count == 0 ? 1 : _movimentacoes.Max(m => m.Id) + 1;
        }

        public IEnumerable<Produto> Produtos => _produtos.Values.OrderBy(p => p.CodigoProduto);

        public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

        public Movimentacao Movimentar(int codigoProduto, TipoMovimentacao tipo, int quantidade, string descricao)
        {
            if (!_produtos.TryGetValue(codigoProduto, out var produto))
                throw new ArgumentException($"Produto {codigoProduto} não encontrado.");

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Informe uma descrição para a movimentação.");

            int novoEstoque = tipo == TipoMovimentacao.Entrada
                ? produto.Estoque + quantidade
                : produto.Estoque - quantidade;

            if (novoEstoque < 0)
                throw new InvalidOperationException(
                    $"Estoque insuficiente. Disponível: {produto.Estoque}, solicitado: {quantidade}.");

            var movimentacao = new Movimentacao(
                Id: _proximoId,
                CodigoProduto: codigoProduto,
                DescricaoProduto: produto.DescricaoProduto,
                Tipo: tipo,
                Quantidade: quantidade,
                Descricao: descricao.Trim(),
                DataHora: DateTime.Now,
                EstoqueFinal: novoEstoque);

            _produtos[codigoProduto] = produto with { Estoque = novoEstoque };
            _movimentacoes.Add(movimentacao);
            _proximoId++;

            _repositorio.SalvarProdutos(_produtos.Values);
            _repositorio.SalvarMovimentacoes(_movimentacoes);

            return movimentacao;
        }
    }
}
