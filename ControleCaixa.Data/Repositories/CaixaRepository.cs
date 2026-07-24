using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleCaixa.Data.Repositories;

public class CaixaRepository : ICaixaRepository
{
    private readonly AppDbContext _context;

    public CaixaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    
    public void Adicionar(Caixa entity)
    {
        _context.Add(entity);
    }

    public void Atualizar(Caixa entity)
    {
        _context.Update(entity);
    }

    public void Apagar(int id)
    {
        var caixas = _context.Caixas
            .Where(x => x.Id == id);

        foreach (var caixa in caixas)
        {
            caixa.EnviarParaLixeira();
        }    
    }

    public async Task<Caixa> ObterPorId(int caixaId)
    {
        return await _context.Caixas
            .AsNoTracking()
            .Include(x => x.Movimentacoes.Where(m => !m.Lixeira))
            .FirstOrDefaultAsync(caixa => caixa.Id == caixaId);
    }

    public async Task<IEnumerable<Caixa>> ObterTodos()
    {
        return await _context.Caixas
            .Include(x => x.Movimentacoes.Where(m => !m.Lixeira))
            .Where(x => !x.Lixeira).ToListAsync();
    }
}