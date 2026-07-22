using ControleCaixa.Business.Dtos;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.Model.Enums;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ControleCaixa.Tests.Business;

public class MovimentacaoServiceTests
{
    private readonly Mock<IMovimentacaoRepository> _movimentacaoRepositoryMock;
    private readonly Mock<ICaixaRepository> _caixaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<MovimentacaoDTO>> _validatorMock;

    private readonly MovimentacaoService _service;

    public MovimentacaoServiceTests()
    {
        _movimentacaoRepositoryMock = new Mock<IMovimentacaoRepository>();
        _caixaRepositoryMock = new Mock<ICaixaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validatorMock = new Mock<IValidator<MovimentacaoDTO>>();

        _service = new MovimentacaoService(
            _movimentacaoRepositoryMock.Object,
            _caixaRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object);
    }
    
    private static MovimentacaoDTO CriarDtoValido()
    {
        return new MovimentacaoDTO
        {
            CaixaId = 1,
            Descricao = "Venda de produto",
            Tipo = TipoMovimentacao.Entrada,
            Categoria = Categoria.Vendas,
            Valor = 150
        };
    }
    
    private static MovimentacaoDTO CriarDtoInvalido()
    {
        return new MovimentacaoDTO
        {
            CaixaId = 1,
            Descricao = string.Empty,
            Tipo = TipoMovimentacao.Entrada,
            Categoria = Categoria.Vendas,
            Valor = 150
        };
    }

    [Fact]
    public async Task CadastrarMovimentacao_DeveRetornarFalha_QuandoCaixaNaoExistir()
    {
        // Arrange
        var dto = CriarDtoValido();

        _validatorMock.Setup(x => x.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _caixaRepositoryMock.Setup(x => x.ObterPorId(dto.CaixaId)).ReturnsAsync((Caixa?)null);

        // Act
        var resultado = await _service.Adicionar(dto);

        // Assert
        Assert.False(resultado.Success);

        Assert.Contains("É necessário um caixa pra fazer essa movimentação", resultado.Errors);

        _caixaRepositoryMock.Verify(x => x.ObterPorId(dto.CaixaId), Times.Once);

        _movimentacaoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Movimentacao>()), Times.Never);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Fact]
    public async Task CadastrarMovimentacao_DeveRetornarFalha_QuandoDtoForInvalido()
    {
        // Arrange
        var dto = CriarDtoInvalido();

        var falhas = new List<ValidationFailure>
        {
            new(nameof(MovimentacaoDTO.Descricao), "A descrição é obrigatória."),

            new(nameof(MovimentacaoDTO.Valor), "O valor deve ser maior que zero.")
        };

        _validatorMock.Setup(x => x.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(falhas));

        // Act
        var resultado = await _service.Adicionar(dto);

        // Assert
        Assert.False(resultado.Success);

        Assert.Contains("A descrição é obrigatória.", resultado.Errors);

        Assert.Contains("O valor deve ser maior que zero.", resultado.Errors);

        _caixaRepositoryMock.Verify(x => x.ObterPorId(It.IsAny<int>()), Times.Never);

        _movimentacaoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Movimentacao>()), Times.Never);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}