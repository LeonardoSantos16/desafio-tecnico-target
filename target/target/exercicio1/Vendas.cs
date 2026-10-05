using System;
using System.Collections.Generic;
using System.Text;

namespace target.exercicio1
{
    public record Vendas
    {
        public required string Vendedor { get; set; }
        public decimal Valor { get; set; }
    }
}
