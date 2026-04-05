using MediatR;
using WebReview.Application.Features.Posts.DTOs;

namespace WebReview.Application.Features.Posts.Queries;

public record GetAllPostsQuery : IRequest<List<PostDto>>;
