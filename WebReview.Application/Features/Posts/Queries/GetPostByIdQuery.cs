using MediatR;
using WebReview.Application.Features.Posts.DTOs;

namespace WebReview.Application.Features.Posts.Queries;

public record GetPostByIdQuery(int Id) : IRequest<PostDto?>;
