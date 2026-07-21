using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.Business.Services;

public class CaixaService : ICaixaService
{
    public Task<IEnumerable<Caixa>> ObterCaixas()
    {
        throw new NotImplementedException();
    }

    public Task<Caixa> ObterCaixaPorId(int caixaId)
    {
        throw new NotImplementedException();
    }

    public void CadastrarCaixa(CaixaDTO caixa)
    {
        throw new NotImplementedException();
    }

    public void EditarCaixa(CaixaDTO caixa)
    {
        throw new NotImplementedException();
    }

    public void ExcluirAsync(int id)
    {
        throw new NotImplementedException();
    }
}