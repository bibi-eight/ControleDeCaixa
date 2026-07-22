using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Results;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;

namespace ControleCaixa.Business.Interfaces;

public interface ICaixaService
{
    Task<IEnumerable<Caixa>> ObterCaixas();

    Task<Caixa> ObterCaixaPorId(int caixaId);
    
    Task<Result> Adicionar(CaixaDTO caixa);
    
    Task<Result> Atualizar(CaixaDTO caixa, int caixaId);
    
    Task<Result> Apagar(int id);
}