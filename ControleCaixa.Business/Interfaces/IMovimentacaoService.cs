using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Results;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.Business.Interfaces;

public interface IMovimentacaoService
{
    Task<IEnumerable<Movimentacao>> ObterMovimentacaos();

    Task<Movimentacao> ObterMovimentacaoPorId(int movimentacaoId);
    Task<IEnumerable<Movimentacao>> ObterPorCaixaId(int caixaId);
    
    Task<IEnumerable<Movimentacao>> ObterPorTipoDeMovimentacaoDeUmCaixa(int caixaId, int tipo);
    
    Task<Result> Adicionar(MovimentacaoDTO movimentacao);
    
    Task<Result> Atualizar(MovimentacaoDTO movimentacao, int movimentacaoId);
    
    Task<Result> Apagar(int id);
    Task<int> ObterQuantidadeMovimentacoes(int caixaId);
    
}