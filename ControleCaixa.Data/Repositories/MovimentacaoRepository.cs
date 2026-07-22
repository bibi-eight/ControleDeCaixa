using System.Linq.Expressions;
using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleCaixa.Data;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly AppDbContext _context;

    public MovimentacaoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Adicionar(Movimentacao entity)
    {
        _context.Movimentacoes.Add(entity);
    }

    public void Atualizar(Movimentacao entity)
    {
        _context.Movimentacoes.Update(entity);
    }

    public void Apagar(int id)
    {
        var movimentacoes = _context.Movimentacoes
            .Where(x => x.Id == id );

        foreach (var movimentacao in movimentacoes)
        {
            movimentacao.EnviarParaLixeira();
        }
    }

    public async Task<Movimentacao> ObterPorId(int movimentacaoId)
    {
        return await  _context.Movimentacoes.FirstOrDefaultAsync(x => x.Id == movimentacaoId && !x.Lixeira);
    }

    public async Task<IEnumerable<Movimentacao>> ObterPorCaixaId(int caixaId)
    {
        return await _context.Movimentacoes.Where(x => x.CaixaId == caixaId
                                                       && !x.Lixeira).ToListAsync();
    }

    public async Task<IEnumerable<Movimentacao>> ObterPorTipoDeMovimentacaoDeUmCaixa(int caixaId, int tipo)
    {
        return await _context.Movimentacoes.Where(x => x.CaixaId == caixaId 
                                                       && x.Tipo == (TipoMovimentacao)tipo
                                                       && !x.Lixeira).ToListAsync();
    }

    public async Task<IEnumerable<Movimentacao>> ObterTodas()
    {
        return await  _context.Movimentacoes.Where(x => !x.Lixeira).ToListAsync();
    }
}