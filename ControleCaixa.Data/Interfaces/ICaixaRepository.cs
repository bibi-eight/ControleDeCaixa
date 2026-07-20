using ControleCaixa.Model;

namespace ControleCaixa.Data.Interfaces;

public interface ICaixaRepository
{
    void Adicionar(Caixa entity);

    void Atualizar(Caixa entity);

    void Apagar(Func<Caixa, bool> predicate);
    
    Task<Caixa> ObterPorId(int caixaId);
    Task<IEnumerable<Caixa>> ObterTodos();
    
}