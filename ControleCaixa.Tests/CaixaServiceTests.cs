using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Services;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Model.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ControleCaixa.Tests.Business;

public class CaixaServiceTests
{
    private readonly Mock<ICaixaRepository> _caixaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<CaixaDTO>> _validatorMock;

    private readonly CaixaService _service;

    public CaixaServiceTests()
    {
        _caixaRepositoryMock = new Mock<ICaixaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validatorMock = new Mock<IValidator<CaixaDTO>>();

        _service = new CaixaService(
            _caixaRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task CadastrarCaixa_DeveAdicionarESalvar_QuandoDadosForemValidos()
    {
        // Arrange
        var dto = CriarDtoValido();

        _validatorMock.Setup(x => x.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var resultado = await _service.Adicionar(dto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Empty(resultado.Errors);

        _caixaRepositoryMock.Verify(x => x.Adicionar(It.Is<Caixa>(caixa =>
                    caixa.Nome == dto.Nome &&
                    caixa.SaldoMinimo == dto.SaldoMinimo)), Times.Once);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);

        _validatorMock.Verify(x => x.ValidateAsync(dto,It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static CaixaDTO CriarDtoValido()
    {
        return new CaixaDTO
        {
            Nome = "Caixa principal",
            SaldoMinimo = 100m
        };
    }
}