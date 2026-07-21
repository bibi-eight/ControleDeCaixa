using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Business.Results;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;
using FluentValidation;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _repository;
    private readonly ICaixaRepository _repositoryCaixa;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<MovimentacaoDTO> _validator;

    public MovimentacaoService(
        IMovimentacaoRepository repository, ICaixaRepository repositoryCaixa,
        IUnitOfWork unitOfWork,
        IValidator<MovimentacaoDTO> validator )
    {
        _repository = repository;
        _repositoryCaixa = repositoryCaixa;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }


    public async Task<IEnumerable<Movimentacao>> ObterMovimentacaos()
    {
        var movimentacoes = await _repository.ObterTodas();
        
        return movimentacoes;
    }

    public async Task<Movimentacao> ObterMovimentacaoPorId(int movimentacaoId)
    {
        var movimentacao = await _repository.ObterPorId(movimentacaoId);

        return movimentacao;
    }

    public async Task<IEnumerable<Movimentacao>> ObterPorTipoDeMovimentacaoDeUmCaixa(int caixaId, int tipo)
    {
        var movimentacoes = await _repository.ObterPorTipoDeMovimentacaoDeUmCaixa(caixaId, tipo);
        return movimentacoes;
    }

    public async Task<Result> CadastrarMovimentacao(MovimentacaoDTO movimentacao)
    {
        var validacao = await _validator.ValidateAsync(movimentacao);

        if (!validacao.IsValid)
            return Result.Fail(validacao);
        
        var caixa = await _repositoryCaixa.ObterPorId(movimentacao.CaixaId);
        
        if(caixa == null) return Result.Fail("É necessário um caixa pra fazer essa movimentação");
        
        var novaMovimentacao = new Movimentacao(
            movimentacao.Descricao,
            movimentacao.Tipo,
            movimentacao.Categoria,
            movimentacao.Valor,
            movimentacao.CaixaId);
        
        _repository.Adicionar(novaMovimentacao); 
        
        var alteracoes = await _unitOfWork.SaveChangesAsync();

        if (alteracoes <= 0)
            return Result.Fail("Não foi possível cadastrar a movimentação.");

        return Result.Ok();
    }

    public async Task<Result> EditarMovimentacao(MovimentacaoDTO movimentacao, int movimentacaoId)
    {
        var validacao = await _validator.ValidateAsync(movimentacao);

        if (!validacao.IsValid)
            return Result.Fail(validacao);    
        
        var movimentacaoExistente = await _repository.ObterPorId(movimentacaoId);

        if (movimentacaoExistente == null) return Result.Fail("Movimentação pra edição não encontrada");
        
        movimentacaoExistente.DefinirDescricao(movimentacao.Descricao);
        movimentacaoExistente.DefinirCategoria(movimentacao.Categoria);
        movimentacaoExistente.DefinirTipo(movimentacao.Tipo);
        movimentacaoExistente.DefinirValor(movimentacao.Valor);
        
        movimentacaoExistente.DefinirDataDeAlteracao();
        
        _repository.Atualizar(movimentacaoExistente); 
        
        var alteracoes = await _unitOfWork.SaveChangesAsync();

        if (alteracoes <= 0)
            return Result.Fail("Não foi possível editar a movimentação.");
        
        return Result.Ok();
    }

    public async Task<Result> ExcluirAsync(int id)
    {
        throw new NotImplementedException();

    }
}