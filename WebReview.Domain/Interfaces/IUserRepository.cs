namespace WebReview.Domain.Interfaces;

using WebReview.Domain.Entities;

public interface IUserRepository
{
    Task<bool> HasAnyUserAsync();
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}