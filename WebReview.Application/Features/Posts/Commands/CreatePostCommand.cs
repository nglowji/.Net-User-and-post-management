using MediatR;
using WebReview.Application.Features.Posts.DTOs;

namespace WebReview.Application.Features.Posts.Commands;

public record CreatePostCommand(string Title, string Content, int UserId) : IRequest<PostDto>;
