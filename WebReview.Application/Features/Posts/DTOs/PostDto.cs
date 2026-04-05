namespace WebReview.Application.Features.Posts.DTOs;

public record PostDto(int Id, string Title, string Content, int UserId, string Username);
