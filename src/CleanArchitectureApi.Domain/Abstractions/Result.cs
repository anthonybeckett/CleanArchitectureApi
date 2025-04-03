using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace CleanArchitectureApi.Domain.Abstractions;

public class Result<TEntity> : ILoggable where TEntity : IResult
{
    public Result()
    {
        //
    }

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

    private Result(HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = new Error
        {
            ErrorCode = errorCode,
            ErrorMessage = [errorMessage]
        };
    }

    private Result(HttpStatusCode statusCode, Error errors)
    {
        IsNotSuccessful = true;
        StatusCode = statusCode;
        Errors = errors;
    }

    public TEntity? Data { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore] public bool IsNotSuccessful { get; set; }

    public Error? Errors { get; set; }

    public static Result<TEntity> Success(TEntity? entity, HttpStatusCode statusCode)
    {
        return new Result<TEntity>(entity, statusCode);
    }

    public static Result<TEntity> Success(HttpStatusCode statusCode)
    {
        return new Result<TEntity>(statusCode);
    }

    public static Result<TEntity> Failure(HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        return new Result<TEntity>(statusCode, errorCode, errorMessage);
    }

    public static Result<TEntity> Failure(HttpStatusCode statusCode, Error errors)
    {
        return new Result<TEntity>(statusCode, errors);
    }
}

public class Error
{
    [Required] public string ErrorCode { get; set; }

    [Required] public List<string> ErrorMessage { get; set; }
}