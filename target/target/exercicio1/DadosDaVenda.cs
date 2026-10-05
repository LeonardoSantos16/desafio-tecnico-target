using System;
using System.Collections.Generic;
using System.Text;

namespace target.exercicio1
{
    public record DadosDaVenda
    {
        public List<Vendas> Vendas { get; set; } = new();
    }
}
