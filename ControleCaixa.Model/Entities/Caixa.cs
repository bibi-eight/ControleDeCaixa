namespace ControleCaixa.Model.Entities;

public class Caixa
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public decimal SaldoMinimo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAlteracao { get; private set; }
    public bool Lixeira { get; private set; }
    
    private readonly List<Movimentacao> _movimentacoes = new();
    public IReadOnlyCollection<Movimentacao> Movimentacoes => _movimentacoes;
    
    public decimal Saldo =>
        _movimentacoes.Sum(x => x.ObterValorAssinado());
    
    public int TotalMovimentacoes => _movimentacoes.Count;
    
    public Caixa(){}

    public Caixa(string nome, decimal saldoMinimo)
    {
        Nome = nome;
        SaldoMinimo = saldoMinimo;
        DataCriacao = DateTime.Now;
        DataAlteracao = DateTime.Now;
    }
    
    public void DefinirNome(string nome) => Nome = nome;
    public void DefinirSaldoMinimo(decimal saldoMinimo) => SaldoMinimo = saldoMinimo;
    public void EnviarParaLixeira() => Lixeira = true;
    

    public void AdicionarMovimentacao(Movimentacao movimentacao)
    {
        _movimentacoes.Add(movimentacao);
    }

    public void RemoverMovimentacao(Movimentacao movimentacao)
    {
        _movimentacoes.Remove(movimentacao);
    }
    
    public void DefinirDataDeAlteracao() => DataAlteracao = DateTime.Now;
    
}