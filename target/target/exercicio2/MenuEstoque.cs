namespace target.exercicio2
{
    public class MenuEstoque
    {
        private readonly ControleEstoque _controle;

        public MenuEstoque(ControleEstoque controle)
        {
            _controle = controle;
        }

        public void Executar()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Controle de Estoque ===");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Lançar entrada");
                Console.WriteLine("3 - Lançar saída");
                Console.WriteLine("4 - Histórico de movimentações");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": ListarProdutos(); break;
                    case "2": LancarMovimentacao(TipoMovimentacao.Entrada); break;
                    case "3": LancarMovimentacao(TipoMovimentacao.Saida); break;
                    case "4": ListarMovimentacoes(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        private void ListarProdutos()
        {
            Console.WriteLine();
            Console.WriteLine($"{"Código",-8}{"Produto",-30}{"Estoque",8}");

            foreach (var p in _controle.Produtos)
                Console.WriteLine($"{p.CodigoProduto,-8}{p.DescricaoProduto,-30}{p.Estoque,8}");
        }

        private void LancarMovimentacao(TipoMovimentacao tipo)
        {
            Console.WriteLine();
            Console.WriteLine(tipo == TipoMovimentacao.Entrada ? "--- Nova entrada ---" : "--- Nova saída ---");

            int codigo = LerInteiro("Código do produto: ");
            int quantidade = LerInteiro("Quantidade: ");
            Console.Write("Descrição da movimentação: ");
            string descricao = Console.ReadLine() ?? string.Empty;

            try
            {
                var mov = _controle.Movimentar(codigo, tipo, quantidade, descricao);

                Console.WriteLine();
                Console.WriteLine($"Movimentação #{mov.Id} registrada.");
                Console.WriteLine($"Estoque final de {mov.DescricaoProduto}: {mov.EstoqueFinal}");
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void ListarMovimentacoes()
        {
            Console.WriteLine();

            if (_controle.Movimentacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma movimentação lançada.");
                return;
            }

            foreach (var m in _controle.Movimentacoes)
            {
                string sinal = m.Tipo == TipoMovimentacao.Entrada ? "+" : "-";
                Console.WriteLine(
                    $"#{m.Id} | {m.DataHora:dd/MM/yyyy HH:mm} | {m.DescricaoProduto} | " +
                    $"{sinal}{m.Quantidade} | {m.Descricao} | estoque final: {m.EstoqueFinal}");
            }
        }

        private static int LerInteiro(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out int valor))
                    return valor;

                Console.WriteLine("Digite um número inteiro válido.");
            }
        }
    }
}
