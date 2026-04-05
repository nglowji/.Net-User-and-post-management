using MediatR;
using WebReview.Application.Features.Users.Commands;
using WebReview.Application.Features.Users.DTOs;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Users.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return null;
        }

        var existed = await _userRepository.GetByUsernameAsync(request.Username.Trim());
        if (existed is not null && existed.Id != request.Id)
        {
            throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
        }

        user.Username = request.Username.Trim();
        await _userRepository.UpdateAsync(user);

        return new UserDto(user.Id, user.Username, user.Role, user.Posts.Count);
    }
}
