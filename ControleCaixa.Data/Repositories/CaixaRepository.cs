using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleCaixa.Data.Repositories;

public class CaixaRepository : ICaixaRepository
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public CaixaRepository(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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
        return await _context.Caixas.AsNoTracking()
            .Include(x => x.Movimentacoes.Where(m => !m.Lixeira))
            .Where(x => !x.Lixeira).ToListAsync();
    }

    //TODO
    public Task<IEnumerable<Movimentacao>> ObterMovimentacoesDeUmCaixaPorMes(int caixaId, int mes)
    {
        throw new NotImplementedException();
    }
}