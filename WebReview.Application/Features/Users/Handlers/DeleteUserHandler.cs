using MediatR;
using WebReview.Application.Features.Users.Commands;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Users.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return false;
        }

        await _userRepository.DeleteAsync(user);
        return true;
    }
}
