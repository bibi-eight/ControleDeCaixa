DECLARE @CaixaId INT;

INSERT INTO Caixas
(
    Nome,
    SaldoMinimo,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    (
        'Caixa Principal',
        1000,
        GETDATE(),
        GETDATE(),
        0
    );

SET @CaixaId = SCOPE_IDENTITY();

INSERT INTO Movimentacoes
(
    Descricao,
    Tipo,
    Categoria,
    Valor,
    CaixaId,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    ('Venda',        0, 0, 2000, @CaixaId, GETDATE(), GETDATE(), 0),
    ('Fornecedor',   1, 1,  500, @CaixaId, GETDATE(), GETDATE(), 0);


INSERT INTO Caixas
(
    Nome,
    SaldoMinimo,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    (
        'Caixa Reserva',
        1000,
        GETDATE(),
        GETDATE(),
        0
    );

SET @CaixaId = SCOPE_IDENTITY();

INSERT INTO Movimentacoes
(
    Descricao,
    Tipo,
    Categoria,
    Valor,
    CaixaId,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    ('Venda',         0, 0, 1500, @CaixaId, GETDATE(), GETDATE(), 0),
    ('Despesa Fixa',  1, 2,  500, @CaixaId, GETDATE(), GETDATE(), 0);



INSERT INTO Caixas
(
    Nome,
    SaldoMinimo,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    (
        'Caixa Secundário',
        1000,
        GETDATE(),
        GETDATE(),
        0
    );

SET @CaixaId = SCOPE_IDENTITY();

INSERT INTO Movimentacoes
(
    Descricao,
    Tipo,
    Categoria,
    Valor,
    CaixaId,
    DataCriacao,
    DataAlteracao,
    Lixeira
)
VALUES
    ('Venda',              0, 0, 1000, @CaixaId, GETDATE(), GETDATE(), 0),
    ('Conta de Energia',   1, 2,  600, @CaixaId, GETDATE(), GETDATE(), 0);