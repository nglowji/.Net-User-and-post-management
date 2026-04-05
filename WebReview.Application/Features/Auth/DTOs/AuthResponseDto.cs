namespace WebReview.Application.Features.Auth.DTOs;

public record AuthResponseDto(int UserId, string Username, string Role, string Token);
