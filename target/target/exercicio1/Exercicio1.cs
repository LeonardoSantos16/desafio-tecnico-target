using System.Text.Json;
using target.exercicio1;
using System.Linq;

public class Exercicio1
{
    private readonly List<Vendas> _sales;
  

    public void CalcularComissao()
    {
        var salesBySeller = _sales.GroupBy(s => s.Vendedor);

        foreach (var group in salesBySeller)
        {
            decimal total = group.Sum(s => s.Valor);
            decimal commission = group.Sum(s => ValidacaoDaComissao(s.Valor));

            Console.WriteLine($"{group.Key}: vendas {total:C}, comissão {commission:C}");
        }
    }



    private static decimal ValidacaoDaComissao(decimal valor)
    {
        if (valor < 100m) return 0m;
        if (valor < 500m) return valor * 0.01m;
        return valor * 0.05m;
    }
}





