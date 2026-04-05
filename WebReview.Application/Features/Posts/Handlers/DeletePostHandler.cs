using MediatR;
using WebReview.Application.Exceptions;
using WebReview.Application.Features.Posts.Commands;
using WebReview.Domain.Interfaces;

namespace WebReview.Application.Features.Posts.Handlers;

public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _postRepository;

    public DeletePostHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post is null)
        {
            throw new NotFoundException("Không tìm thấy bài viết.");
        }

        if (!request.IsAdmin && post.UserId != request.UserId)
        {
            throw new ForbiddenException("Bạn không có quyền xóa bài viết này.");
        }

        await _postRepository.DeleteAsync(post);
        return true;
    }
}
