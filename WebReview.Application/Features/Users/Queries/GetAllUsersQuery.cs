using MediatR;
using WebReview.Application.Features.Users.DTOs;

namespace WebReview.Application.Features.Users.Queries;

public record GetAllUsersQuery : IRequest<List<UserDto>>;
