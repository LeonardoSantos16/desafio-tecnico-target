
namespace target.exercicio2
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; } = string.Empty;
        public int Estoque { get; set; }
    }

    public class DadosEstoque
    {
        public List<Produto> Estoque { get; set; } = new();
    }

}
