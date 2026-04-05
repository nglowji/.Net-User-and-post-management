using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebReview.API.Contracts.Errors;
using WebReview.API.Contracts.Posts;
using WebReview.Application.Features.Posts.Commands;
using WebReview.Application.Features.Posts.Queries;
using WebReview.Domain.Constants;

namespace WebReview.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _mediator.Send(new GetAllPostsQuery());
        var data = posts.Select(x => new PostResponse(x.Id, x.Title, x.Content, x.UserId, x.Username));
        return Ok(new SuccessResponse<IEnumerable<PostResponse>>("Lấy danh sách bài viết thành công.", data));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _mediator.Send(new GetPostByIdQuery(id));
        return post is null
            ? NotFound(new ErrorResponse("Không tìm thấy bài viết."))
            : Ok(new SuccessResponse<PostResponse>("Lấy chi tiết bài viết thành công.", new PostResponse(post.Id, post.Title, post.Content, post.UserId, post.Username)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertPostRequest request)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized(new ErrorResponse("Bạn chưa đăng nhập."));
        }

        var created = await _mediator.Send(new CreatePostCommand(request.Title, request.Content, userId));
        var response = new PostResponse(created.Id, created.Title, created.Content, created.UserId, created.Username);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new SuccessResponse<PostResponse>("Tạo bài viết thành công.", response));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertPostRequest request)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized(new ErrorResponse("Bạn chưa đăng nhập."));
        }

        var isAdmin = User.IsInRole(UserRoles.Admin);
        var updated = await _mediator.Send(new UpdatePostCommand(id, request.Title, request.Content, userId, isAdmin));
        return Ok(new SuccessResponse<PostResponse>("Cập nhật bài viết thành công.", new PostResponse(updated.Id, updated.Title, updated.Content, updated.UserId, updated.Username)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized(new ErrorResponse("Bạn chưa đăng nhập."));
        }

        var isAdmin = User.IsInRole(UserRoles.Admin);
        await _mediator.Send(new DeletePostCommand(id, userId, isAdmin));
        return Ok(new SuccessResponse<string>("Xóa bài viết thành công.", "Đã xóa bài viết."));
    }

    private bool TryGetCurrentUserId(out int userId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claim, out userId);
    }
}
