using ControleCaixa.Business.Dtos;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;

namespace ControleCaixa.Business.Interfaces;

public interface ICaixaService
{
    Task<IEnumerable<Caixa>> ObterCaixas();

    Task<Caixa> ObterCaixaPorId(int caixaId);
    
    void CadastrarCaixa(CaixaDTO caixa);
    
    void EditarCaixa(CaixaEditarDTO caixa);
    
    void ExcluirAsync(int id);
}