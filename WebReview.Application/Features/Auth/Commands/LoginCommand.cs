using MediatR;
using WebReview.Application.Features.Auth.DTOs;

namespace WebReview.Application.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<AuthResponseDto>;
