using System.Text.Json;

namespace target.exercicio1
{
    public class RepositorioVendas
    {
        private static readonly JsonSerializerOptions Opcoes = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly string _caminhoArquivo;

        public RepositorioVendas(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
        }

        public List<Vendas> ObterTodas()
        {
            var json = File.ReadAllText(_caminhoArquivo);

            var dados = JsonSerializer.Deserialize<DadosDaVenda>(json, Opcoes)
                ?? throw new InvalidOperationException("Erro ao desserializar as vendas.");

            return dados.Vendas;
        }
    }
}
