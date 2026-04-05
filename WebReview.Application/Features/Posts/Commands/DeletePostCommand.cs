using MediatR;

namespace WebReview.Application.Features.Posts.Commands;

public record DeletePostCommand(int Id, int UserId, bool IsAdmin) : IRequest<bool>;
