using ControleCaixa.Business.Dtos;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.Business.Interfaces;

public interface IMovimentacaoService
{
    Task<IEnumerable<Movimentacao>> ObterMovimentacaos();

    Task<Movimentacao> ObterMovimentacaoPorId(int movimentacaoId);
    
    Task<IEnumerable<Movimentacao>> ObterPorTipoDeMovimentacaoDeUmCaixa(int caixaId, int tipo);
    
    void CadastrarMovimentacao(MovimentacaoDTO movimentacao);
    
    void EditarMovimentacao(MovimentacaoEditarDTO movimentacao);
    
    void ExcluirAsync(int id);
}