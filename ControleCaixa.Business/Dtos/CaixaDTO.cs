namespace ControleCaixa.Business.Dtos;

public class CaixaDTO
{
    public string Nome { get; set; }
    public decimal SaldoMinimo { get; set; }
}

public class CaixaDetalhesDTO
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Saldo { get; set; }

    public decimal SaldoMinimo { get; set; }

    public List<MovimentacaoCompletaDTO> Movimentacoes { get; set; } = [];
}