using MediatR;
using WebReview.Application.Features.Posts.DTOs;
using WebReview.Application.Features.Posts.Queries;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Posts.Handlers;

public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        return post is null
            ? null
            : new PostDto(post.Id, post.Title, post.Content, post.UserId, post.User?.Username ?? string.Empty);
    }
}
