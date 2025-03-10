using System.Net;
using CleanArchitectureApi.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using IResult = CleanArchitectureApi.Domain.Abstractions.IResult;

namespace CleanArchitectureApi.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    public IActionResult CreateResult<TDto>(Result<TDto> result) where TDto : IResult
        => result.StatusCode == HttpStatusCode.NoContent ? NoContent() : Ok(result.Data);
}