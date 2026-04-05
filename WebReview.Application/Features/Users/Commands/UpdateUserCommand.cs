using MediatR;
using WebReview.Application.Features.Users.DTOs;

namespace WebReview.Application.Features.Users.Commands;

public record UpdateUserCommand(int Id, string Username) : IRequest<UserDto?>;
