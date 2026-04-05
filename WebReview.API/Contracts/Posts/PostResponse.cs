namespace WebReview.API.Contracts.Posts;

public record PostResponse(int Id, string Title, string Content, int UserId, string Username);
