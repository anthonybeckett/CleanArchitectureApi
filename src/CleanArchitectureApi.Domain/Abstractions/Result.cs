using System.Net;

namespace CleanArchitectureApi.Domain.Abstractions;

public class Result<TEntity> where TEntity : IResult
{
    private Result(TEntity? entity, HttpStatusCode statusCode)
    {
        Data = entity;
        IsNotSuccessful = false;
        StatusCode = statusCode;
    }

    private Result(HttpStatusCode statusCode)
    {
        IsNotSuccessful = false;
        StatusCode = statusCode;
    }

    public Result(HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = new Dictionary<string, string>
        {
            { errorCode, errorMessage }
        };
    }
    
    public Result(HttpStatusCode statusCode, Dictionary<string, string> errors)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = errors;
    }

    public TEntity? Data { get; set; }

    public bool IsNotSuccessful { get; set; }
    
    public HttpStatusCode StatusCode { get; set; }

    public Dictionary<string, string>? Errors { get; set; }
    
    public static Result<TEntity> Success(TEntity? entity, HttpStatusCode statusCode) => new(entity, statusCode);

    public static Result<TEntity> Success(HttpStatusCode statusCode) => new(statusCode);
    
    public static Result<TEntity> Failure(HttpStatusCode statusCode, string errorCode, string errorMessage) 
        => new(statusCode, errorCode, errorMessage);
    
    public static Result<TEntity> Failure(HttpStatusCode statusCode, Dictionary<string, string> errors) 
        => new(statusCode, errors);
}