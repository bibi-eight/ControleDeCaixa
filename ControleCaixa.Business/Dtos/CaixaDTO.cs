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

    public string CorIndicador
    {
        get
        {
            if (Saldo < SaldoMinimo)
                return "#DC2626"; // Vermelho

            if (Saldo == SaldoMinimo)
                return "#EAB308"; // Amarelo

            return "#16A34A"; // Verde
        }
    }

    public List<MovimentacaoCompletaDTO> Movimentacoes { get; set; } = [];
}