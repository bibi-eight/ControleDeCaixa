using ControleCaixa.Model.Entities;

namespace ControleCaixa.Data.Interfaces;

public interface ICaixaRepository
{
    void Adicionar(Caixa entity);

    void Atualizar(Caixa entity);

    void Apagar(int id);
    
    Task<Caixa> ObterPorId(int caixaId);
    Task<IEnumerable<Caixa>> ObterTodos();
    Task<int> ObterQuantidadeMovimentacoes(int caixaId);
    Task<IEnumerable<Movimentacao>> ObterMovimentacoesDeUmCaixaPorMes(int caixaId, int mes);
    
}