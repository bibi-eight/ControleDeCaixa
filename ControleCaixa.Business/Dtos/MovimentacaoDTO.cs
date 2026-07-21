namespace ControleCaixa.Business.Dtos;

public class MovimentacaoDTO
{
    public string Descricao { get; private set; }

    public int Tipo { get; private set; }

    public int Categoria { get; private set; }

    public decimal Valor { get; private set; }
    
    public int CaixaId { get; private set; }
}

public class MovimentacaoEditarDTO
{
    public int Id { get; private set; }

    public string Descricao { get; private set; }

    public int Tipo { get; private set; }

    public int Categoria { get; private set; }

    public decimal Valor { get; private set; }
    
    public int CaixaId { get; private set; }
}