namespace ControleCaixa.Model.Entities;

public class Caixa
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public decimal SaldoMinimo { get; private set; }
    public decimal Saldo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    public bool Lixeira { get; private set; }
    
    private readonly List<Movimentacao> _movimentacoes = new();
    public IReadOnlyCollection<Movimentacao> Movimentacoes => _movimentacoes;
    
    public Caixa(){}

    public Caixa(string nome, decimal saldoMinimo, DateTime dataCriacao)
    {
        Nome = nome;
        SaldoMinimo = saldoMinimo;
        DataCriacao = dataCriacao;
    }
    
    public void DefinirNome(string nome) => Nome = nome;
    public void DefinirSaldoMinimo(decimal saldoMinimo) => SaldoMinimo = saldoMinimo;
    public void EnviarParaLixeira() => Lixeira = true;
    
    public decimal ObterSaldo =>
        _movimentacoes.Sum(x => x.ObterValorAssinado());

    public void AdicionarMovimentacao(Movimentacao movimentacao)
    {
        _movimentacoes.Add(movimentacao);
    }

    public void RemoverMovimentacao(Movimentacao movimentacao)
    {
        _movimentacoes.Remove(movimentacao);
    }
    
}