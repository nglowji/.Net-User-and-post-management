using MediatR;
using WebReview.Application.Features.Posts.DTOs;
using WebReview.Application.Features.Posts.Queries;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Posts.Handlers;

public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, List<PostDto>>
{
    private readonly IPostRepository _postRepository;

    public GetAllPostsHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetAllAsync();
        return posts
            .Select(x => new PostDto(x.Id, x.Title, x.Content, x.UserId, x.User?.Username ?? string.Empty))
            .ToList();
    }
}
