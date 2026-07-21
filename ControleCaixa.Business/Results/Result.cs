using FluentValidation.Results;

namespace ControleCaixa.Business.Results;

public class Result
{
    public bool Success { get; init; }

    public List<string> Errors { get; init; } = [];

    public static Result Ok()
        => new()
        {
            Success = true
        };

    public static Result Fail(params string[] errors)
        => new()
        {
            Success = false,
            Errors = errors.ToList()
        };
    
    public static Result Fail(ValidationResult validationResult)
        => new()
        {
            Success = false,
            Errors = validationResult.Errors
                .Select(x => x.ErrorMessage)
                .ToList()
        };
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    public static Result<T> Ok(T data)
        => new()
        {
            Success = true,
            Data = data
        };

    public new static Result<T> Fail(params string[] errors)
        => new()
        {
            Success = false,
            Errors = errors.ToList()
        };
}