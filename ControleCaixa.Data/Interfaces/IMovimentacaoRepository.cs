using ControleCaixa.Model.Entities;

namespace ControleCaixa.Data.Interfaces;

public interface IMovimentacaoRepository
{
    void Adicionar(Movimentacao entity);

    void Atualizar(Movimentacao entity);

    void Apagar(Func<Movimentacao, bool> predicate);
    
    Task<Movimentacao> ObterPorId(int movimentacaoId);
    Task<IEnumerable<Movimentacao>> ObterPorCaixaId(int caixaId);

    Task<IEnumerable<Movimentacao>> ObterPorTipoDeMovimentacaoDeUmCaixa(int caixaId, int tipo);
    
}