namespace ControleCaixa.Business.Dtos;

public class CaixaDTO
{
    public string Nome { get; set; }
    public decimal SaldoMinimo { get; set; }
}

public class CaixaEditarDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal SaldoMinimo { get; set; }
}