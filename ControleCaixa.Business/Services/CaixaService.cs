using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Business.Results;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;
using FluentValidation;

namespace ControleCaixa.Business.Services;

public class CaixaService : ICaixaService
{
    private readonly ICaixaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CaixaDTO> _validator;

    public CaixaService(
        ICaixaRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<CaixaDTO> validator )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    
    public async Task<IEnumerable<Caixa>> ObterCaixas()
    {
        return await _repository.ObterTodos();
    }

    public async Task<CaixaDetalhesDTO?> ObterCaixaPorId(int caixaId)
    {
        var caixa = await _repository.ObterPorId(caixaId);
    
        if (caixa is null)
            return null;
    
        return new CaixaDetalhesDTO
        {
            Id = caixa.Id,
            Nome = caixa.Nome,
            SaldoMinimo = caixa.SaldoMinimo,
    
            Saldo = caixa.Movimentacoes.Sum(m =>
                m.Tipo == TipoMovimentacao.Entrada
                    ? m.Valor
                    : -m.Valor),
    
            Movimentacoes = caixa.Movimentacoes
                .OrderByDescending(m => m.DataCriacao)
                .Select(m => new MovimentacaoCompletaDTO()
                {
                    Id = m.Id,
                    Descricao = m.Descricao,
                    Categoria = m.Categoria,
                    Valor = m.Valor,
                    Tipo = m.Tipo,
                    Data = m.DataCriacao
                })
                .ToList()
        };
    }

    public async Task<Result> Adicionar(CaixaDTO caixa)
    {
        var validacao = await _validator.ValidateAsync(caixa);

        if (!validacao.IsValid)
            return Result.Fail(validacao);    
        
        var novoCaixa = new Caixa(caixa.Nome, caixa.SaldoMinimo);
        
        _repository.Adicionar(novoCaixa); 
        
        var alteracoes = await _unitOfWork.SaveChangesAsync();

        if (alteracoes <= 0)
            return Result.Fail("Não foi possível cadastrar o novo caixa.");

        return Result.Ok();
    }

    public async Task<Result> Atualizar(CaixaDTO caixa, int caixaId)
    {
        var validacao = await _validator.ValidateAsync(caixa);

        if (!validacao.IsValid)
            return Result.Fail(validacao);    
        
        var caixaExistente = await _repository.ObterPorIdPraEditar(caixaId);

        if (caixaExistente == null) return Result.Fail("Caixa pra edição não encontrado");
        
        caixaExistente.DefinirNome(caixa.Nome);
        caixaExistente.DefinirSaldoMinimo(caixa.SaldoMinimo);
        
        caixaExistente.DefinirDataDeAlteracao();
        
        _repository.Atualizar(caixaExistente); 
        
        var alteracoes = await _unitOfWork.SaveChangesAsync();

        if (alteracoes <= 0)
            return Result.Fail("Não foi possível editar o caixa.");
        
        return Result.Ok();    
    }

    public async Task<Result> Apagar(int id)
    {
        var caixa = await _repository.ObterPorId(id);
        
        if (caixa == null) return Result.Fail("Caixa não encontrado");
        
        _repository.Apagar(caixa.Id);
        
        var alteracoes = await _unitOfWork.SaveChangesAsync();

        if (alteracoes <= 0)
            return Result.Fail("Não foi possível apagar o caixa.");
        
        return Result.Ok();
        
    }
}