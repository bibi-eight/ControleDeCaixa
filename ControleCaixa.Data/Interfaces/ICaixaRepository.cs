using ControleCaixa.Model.Entities;

namespace ControleCaixa.Data.Interfaces;

public interface ICaixaRepository
{
    void Adicionar(Caixa entity);

    void Atualizar(Caixa entity);

    void Apagar(int id);
    
    Task<Caixa> ObterPorId(int caixaId);
    Task<IEnumerable<Caixa>> ObterTodos();
    
}