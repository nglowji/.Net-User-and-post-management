using MediatR;
using WebReview.Application.Features.Auth.Commands;
using WebReview.Application.Features.Auth.DTOs;
using WebReview.Application.Interfaces;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username.Trim());
        if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Tên đăng nhập hoặc mật khẩu không đúng.");
        }

        var token = _jwtService.GenerateToken(user);
        return new AuthResponseDto(user.Id, user.Username, user.Role, token);
    }
}
