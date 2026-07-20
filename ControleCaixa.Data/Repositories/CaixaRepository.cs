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

    public void Apagar(Func<Caixa, bool> predicate)
    {
        _context.RemoveRange(_context.Caixas.Where(predicate));
    }

    public async Task<Caixa> ObterPorId(int caixaId)
    {
        return await _context.Caixas.FirstOrDefaultAsync(caixa => caixa.Id == caixaId);
    }

    public async Task<IEnumerable<Caixa>> ObterTodos()
    {
        return await _context.Caixas.Where(x => !x.Lixeira).ToListAsync();
    }
}