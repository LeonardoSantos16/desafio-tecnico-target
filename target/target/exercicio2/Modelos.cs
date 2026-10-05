
namespace target.exercicio2
{
    public record Produto(int CodigoProduto, string DescricaoProduto, int Estoque);

    public record DadosEstoque(IReadOnlyList<Produto> Estoque);

    public enum TipoMovimentacao
    {
        Entrada,
        Saida
    }

    public record Movimentacao(
        int Id,
        int CodigoProduto,
        string DescricaoProduto,
        TipoMovimentacao Tipo,
        int Quantidade,
        string Descricao,
        DateTime DataHora,
        int EstoqueFinal);

}
