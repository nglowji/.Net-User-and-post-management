using MediatR;

namespace WebReview.Application.Features.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<bool>;
