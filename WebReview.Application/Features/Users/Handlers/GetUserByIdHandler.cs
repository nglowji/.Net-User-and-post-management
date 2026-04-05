using MediatR;
using WebReview.Application.Features.Users.DTOs;
using WebReview.Application.Features.Users.Queries;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Users.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        return user is null ? null : new UserDto(user.Id, user.Username, user.Role, user.Posts.Count);
    }
}
