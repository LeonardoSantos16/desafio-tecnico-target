# Desafio Técnico Target

Aplicação console em C# com a resolução dos três exercícios do desafio técnico. Um painel inicial permite escolher qual exercício executar e ao terminar, retornar ao menu.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Como executar

Pelo terminal, na pasta do projeto (onde fica o `.csproj`):

```bash
dotnet run
```
Os arquivos JSON de cada exercício são copiados automaticamente para a pasta de saída (`bin/`) durante a compilação.

## Estrutura

```
target/
├── exercicio1/
│   ├── CalculadoraComissao.cs
│   ├── RepositorioVendas.cs
│   ├── Venda.cs
│   └── vendas.json
├── exercicio2/
│   ├── ControleEstoque.cs
│   ├── MenuEstoque.cs
│   ├── Modelos.cs
│   ├── RepositorioEstoque.cs
│   └── estoque.json
├── exercicio3/
│   └── CalculadoraJuros.cs
└── Program.cs
```

Em todos os exercícios, a leitura de dados, a regra de negócio e a interação com o console ficam em classes separadas, o que facilita testar o cálculo de forma isolada.

## Exercício 1 — Comissão de vendedores

Lê as vendas do arquivo `vendas.json`, agrupa por vendedor e exibe o total vendido e a comissão de cada um.

A comissão é calculada venda a venda, com as seguintes faixas:

| Valor da venda      | Comissão |
|---------------------|----------|
| Abaixo de R$ 100,00 | 0%       |
| Até R$ 500,00       | 1%       |
| A partir de R$ 500,00 | 5%     |

Os valores usam `decimal` para evitar erros de arredondamento com dinheiro.

## Exercício 2 — Movimentação de estoque

Permite lançar entradas e saídas de mercadoria para os produtos cadastrados em `estoque.json`. Cada movimentação possui um identificador único, uma descrição informada pelo usuário e, ao ser registrada, exibe a quantidade final em estoque do produto.

Decisões tomadas:

- **Apenas produtos já cadastrados podem ser movimentados.** O cadastro de produtos não faz parte do escopo do enunciado; códigos inexistentes são rejeitados.
- **Saídas maiores que o estoque disponível são bloqueadas**, assim como quantidades zero ou negativas e descrições vazias.
- **O estoque é persistido.** Após cada movimentação, o `estoque.json` é atualizado.
- **O histórico de movimentações também é persistido**, em `movimentacoes.json`. Isso garante que os identificadores continuem únicos entre execuções do programa. O arquivo é criado automaticamente na primeira movimentação.

### Estado inicial e reinício

O `estoque.json` do projeto representa o **estado inicial** do estoque e não é alterado pelo programa. As movimentações modificam a cópia que fica na pasta de saída (`bin/Debug/net10.0/exercicio2/`).

Para voltar ao estado inicial, apague o `movimentacoes.json` dessa pasta e faça um *Rebuild* do projeto.

## Exercício 3 — Cálculo de juros

A partir de um valor e de uma data de vencimento, calcula os juros na data de hoje, com taxa de 2,5% ao dia.

```
juros = valor × 2,5% × dias de atraso
```

Exemplo: um valor de R$ 1.000,00 vencido há 4 dias gera R$ 100,00 de juros, totalizando R$ 1.100,00.
