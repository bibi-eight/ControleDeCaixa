using ControleCaixa.Model.Enums;

namespace ControleCaixa.Business.Dtos;

public class MovimentacaoDTO
{
    public string Descricao { get; set; }

    public TipoMovimentacao Tipo { get; set; }

    public Categoria Categoria { get; set; }

    public decimal Valor { get; set; }
    
    public int CaixaId { get; set; }
    
    
}

public class MovimentacaoCompletaDTO
{
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public TipoMovimentacao Tipo { get; set; }
    public Categoria Categoria { get; set; }

    public DateTime Data { get; set; }
    
    public int CaixaId { get; set; }
    
    public string ValorFormatado =>
        Tipo == TipoMovimentacao.Saida
            ? $"- R$ {Valor:N2}"
            : $"+ R$ {Valor:N2}";
    

    public string CorValor =>
        Tipo == TipoMovimentacao.Saida
            ? "#DC2626"
            : "#16A34A";
}