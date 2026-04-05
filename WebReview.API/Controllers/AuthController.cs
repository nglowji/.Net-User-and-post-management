using Microsoft.AspNetCore.Mvc;
using MediatR;
using WebReview.API.Contracts.Auth;
using WebReview.API.Contracts.Errors;
using WebReview.Application.Features.Auth.Commands;

namespace WebReview.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand(request.Username, request.Password));
        var response = new AuthResponse(result.UserId, result.Username, result.Role, result.Token);
        return Ok(new SuccessResponse<AuthResponse>("Đăng ký thành công.", response));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var result = await _mediator.Send(new LoginCommand(request.Username, request.Password));
        var response = new AuthResponse(result.UserId, result.Username, result.Role, result.Token);
        return Ok(new SuccessResponse<AuthResponse>("Đăng nhập thành công.", response));
    }
}
