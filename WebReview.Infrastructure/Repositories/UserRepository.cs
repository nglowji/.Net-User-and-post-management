using Microsoft.EntityFrameworkCore;
using WebReview.Domain.Entities;
using WebReview.Domain.Interfaces;
using WebReview.Infrastructure.Persistence;

namespace WebReview.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasAnyUserAsync()
    {
        return await _context.Users.AnyAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Include(x => x.Posts)
            .OrderBy(x => x.Username)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}