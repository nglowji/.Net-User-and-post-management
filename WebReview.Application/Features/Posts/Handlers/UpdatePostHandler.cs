using MediatR;
using WebReview.Application.Exceptions;
using WebReview.Application.Features.Posts.Commands;
using WebReview.Application.Features.Posts.DTOs;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Posts.Handlers;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post is null)
        {
            throw new NotFoundException("Không tìm thấy bài viết.");
        }

        if (!request.IsAdmin && post.UserId != request.UserId)
        {
            throw new ForbiddenException("Bạn không có quyền cập nhật bài viết này.");
        }

        post.Title = request.Title.Trim();
        post.Content = request.Content.Trim();
        await _postRepository.UpdateAsync(post);

        return new PostDto(post.Id, post.Title, post.Content, post.UserId, post.User?.Username ?? string.Empty);
    }
}
