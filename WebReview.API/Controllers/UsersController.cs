using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebReview.API.Contracts.Errors;
using WebReview.API.Contracts.Users;
using WebReview.Application.Features.Users.Commands;
using WebReview.Application.Features.Users.Queries;
using WebReview.Domain.Constants;

namespace WebReview.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        var data = users.Select(x => new UserResponse(x.Id, x.Username, x.Role, x.SoBaiViet));
        return Ok(new SuccessResponse<IEnumerable<UserResponse>>("Lấy danh sách người dùng thành công.", data));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user is null
            ? NotFound(new ErrorResponse("Không tìm thấy người dùng."))
            : Ok(new SuccessResponse<UserResponse>("Lấy chi tiết người dùng thành công.", new UserResponse(user.Id, user.Username, user.Role, user.SoBaiViet)));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        var user = await _mediator.Send(new UpdateUserCommand(id, request.Username));
        return user is null
            ? NotFound(new ErrorResponse("Không tìm thấy người dùng."))
            : Ok(new SuccessResponse<UserResponse>("Cập nhật người dùng thành công.", new UserResponse(user.Id, user.Username, user.Role, user.SoBaiViet)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteUserCommand(id));
        return deleted
            ? Ok(new SuccessResponse<string>("Xóa người dùng thành công.", "Đã xóa người dùng."))
            : NotFound(new ErrorResponse("Không tìm thấy người dùng."));
    }

}
