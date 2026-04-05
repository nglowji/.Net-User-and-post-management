using MediatR;
using WebReview.Application.Features.Users.DTOs;
using WebReview.Application.Features.Users.Queries;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Users.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(x => new UserDto(x.Id, x.Username, x.Role, x.Posts.Count)).ToList();
    }
}
