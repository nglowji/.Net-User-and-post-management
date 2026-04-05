namespace WebReview.API.Contracts.Auth;

public record AuthResponse(int UserId, string Username, string Role, string Token);
