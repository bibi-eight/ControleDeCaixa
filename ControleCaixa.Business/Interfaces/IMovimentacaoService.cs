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
    
    Task<Result> CadastrarMovimentacao(MovimentacaoDTO movimentacao);
    
    Task<Result> EditarMovimentacao(MovimentacaoDTO movimentacao, int movimentacaoId);
    
    Task<Result> ExcluirAsync(int id);
}