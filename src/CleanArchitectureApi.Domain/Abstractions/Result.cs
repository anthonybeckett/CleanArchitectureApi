namespace CleanArchitectureApi.Domain.Abstractions;

public class Result<TEntity>
{
    private Result(TEntity? entity, int statusCode)
    {
        Data = entity;
        IsNotSuccessful = false;
        StatusCode = statusCode;
    }

    private Result(int statusCode)
    {
        IsNotSuccessful = false;
        StatusCode = statusCode;
    }

    public Result(int statusCode, string errorCode, string errorMessage)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = new Dictionary<string, string>
        {
            { errorCode, errorMessage }
        };
    }
    
    public Result(int statusCode, Dictionary<string, string> errors)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = errors;
    }

    public TEntity? Data { get; set; }

    public bool IsNotSuccessful { get; set; }
    
    public int StatusCode { get; set; }

    public Dictionary<string, string>? Errors { get; set; }
    
    public static Result<TEntity> Success(TEntity? entity, int statusCode) => new(entity, statusCode);

    public static Result<TEntity> Success(int statusCode) => new(statusCode);
    
    public static Result<TEntity> Failure(int statusCode, string errorCode, string errorMessage) 
        => new(statusCode, errorCode, errorMessage);
    
    public static Result<TEntity> Failure(int statusCode, Dictionary<string, string> errors) 
        => new(statusCode, errors);
}

public class NoContentDto;