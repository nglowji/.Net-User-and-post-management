using MediatR;
using WebReview.Application.Features.Auth.Commands;
using WebReview.Application.Features.Auth.DTOs;
using WebReview.Application.Interfaces;
using WebReview.Domain.Constants;
using WebReview.Domain.Entities;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
        }

        var user = new User
        {
            Username = request.Username.Trim(),
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = await _userRepository.HasAnyUserAsync() ? UserRoles.User : UserRoles.Admin
        };

        await _userRepository.AddAsync(user);
        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto(user.Id, user.Username, user.Role, token);
    }
}
