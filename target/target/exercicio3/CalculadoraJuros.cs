using System.Globalization;

namespace target.exercicio3
{
    public class CalculadoraJuros
    {
        private const decimal TaxaDiaria = 0.025m;

        public static int CalcularDiasAtraso(DateOnly vencimento, DateOnly hoje)
        {
            int dias = hoje.DayNumber - vencimento.DayNumber;
            return Math.Max(dias, 0);
        }

        public static decimal CalcularJuros(decimal valor, DateOnly vencimento, DateOnly hoje)
        {
            int diasAtraso = CalcularDiasAtraso(vencimento, hoje);
            return Math.Round(valor * TaxaDiaria * diasAtraso, 2);
        }

        public void Executar()
        {
            var cultura = new CultureInfo("pt-BR");
            var hoje = DateOnly.FromDateTime(DateTime.Today);

            decimal valor = LerValor(cultura);
            DateOnly vencimento = LerData(cultura);

            int diasAtraso = CalcularDiasAtraso(vencimento, hoje);
            decimal juros = CalcularJuros(valor, vencimento, hoje);

            Console.WriteLine();
            Console.WriteLine($"Data de hoje:   {hoje.ToString("dd/MM/yyyy", cultura)}");
            Console.WriteLine($"Dias de atraso: {diasAtraso}");
            Console.WriteLine($"Juros:          {juros.ToString("C", cultura)}");
            Console.WriteLine($"Valor total:    {(valor + juros).ToString("C", cultura)}");
        }

        private static decimal LerValor(CultureInfo cultura)
        {
            while (true)
            {
                Console.Write("Valor (ex.: 1500,00): ");
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, cultura, out decimal valor) && valor > 0)
                    return valor;

                Console.WriteLine("Digite um valor válido maior que zero.");
            }
        }

        private static DateOnly LerData(CultureInfo cultura)
        {
            while (true)
            {
                Console.Write("Data de vencimento (dd/MM/aaaa): ");
                if (DateOnly.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", cultura, DateTimeStyles.None, out DateOnly data))
                    return data;

                Console.WriteLine("Digite uma data válida no formato dd/MM/aaaa.");
            }
        }
    }
}
