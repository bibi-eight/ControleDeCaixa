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
