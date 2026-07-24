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
    
    public decimal SaldoMinimo { get; set; }
    
    public decimal Saldo { get; set; }

    public string SaldoFormatado => $"{Saldo:C2}";

    public string CorIndicador =>
        Saldo <= SaldoMinimo
            ? "#DC2626"
            : "#16A34A";

    public List<MovimentacaoCompletaDTO> Movimentacoes { get; set; } = [];
}