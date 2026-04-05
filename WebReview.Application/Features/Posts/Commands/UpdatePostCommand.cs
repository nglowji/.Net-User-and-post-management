using MediatR;
using WebReview.Application.Features.Posts.DTOs;

namespace WebReview.Application.Features.Posts.Commands;

public record UpdatePostCommand(int Id, string Title, string Content, int UserId, bool IsAdmin) : IRequest<PostDto>;
