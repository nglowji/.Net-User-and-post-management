namespace WebReview.Application.Interfaces;

using WebReview.Domain.Entities;

public interface IJwtService
{
    string GenerateToken(User user);
}