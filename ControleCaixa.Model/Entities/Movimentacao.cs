using ControleCaixa.Model.Enums;

namespace ControleCaixa.Model.Entities;

public class Movimentacao
{
    public int Id { get; private set; }

    public string Descricao { get; private set; }

    public TipoMovimentacao Tipo { get; private set; }

    public Categoria Categoria { get; private set; }

    public decimal Valor { get; private set; }
    
    public int CaixaId { get; private set; }

    public Caixa Caixa { get; private set; }

    public DateTime DataCriacao { get; private set; }
    public DateTime DataAlteracao { get; private set; } 
    public bool Lixeira { get; private set; }     

     public Movimentacao(){}

    public Movimentacao(string descricao, TipoMovimentacao tipo, Categoria categoria, decimal valor, int caixaId)
    {
        Descricao = descricao;
        Tipo = tipo;
        Categoria = categoria;
        Valor = valor;
        DataCriacao = DateTime.Now;
        CaixaId = caixaId;
    }
    
    public void DefinirTipo(TipoMovimentacao tipo) => Tipo = tipo;

    public void DefinirCategoria(Categoria categoria) => Categoria = categoria;
    
    public void DefinirValor(decimal valor) => Valor = valor;
    
    public void DefinirDescricao(string descricao) => Descricao = descricao;
    
    public void DefinirDataDeAlteracao(DateTime dataAlteracao) => DataAlteracao = dataAlteracao;
    
    public void EnviarParaLixeira() => Lixeira = true;
    
    public bool EhEntrada()
    {
        return Tipo == TipoMovimentacao.Entrada;
    }

    public bool EhSaida()
    {
        return Tipo == TipoMovimentacao.Saida;
    }
    
    public decimal ObterValorAssinado()
    {
        return EhEntrada()
            ? Valor
            : -Valor;
    }
    
}