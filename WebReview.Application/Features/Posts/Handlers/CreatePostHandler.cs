using MediatR;
using WebReview.Application.Features.Posts.Commands;
using WebReview.Application.Features.Posts.DTOs;
using WebReview.Domain.Entities;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Posts.Handlers;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CreatePostHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId)
            ?? throw new InvalidOperationException("Người dùng không tồn tại.");

        var post = new Post
        {
            Title = request.Title.Trim(),
            Content = request.Content.Trim(),
            UserId = request.UserId
        };

        await _postRepository.AddAsync(post);
        return new PostDto(post.Id, post.Title, post.Content, post.UserId, user.Username);
    }
}
