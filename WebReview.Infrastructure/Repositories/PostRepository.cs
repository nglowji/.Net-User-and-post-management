using Microsoft.EntityFrameworkCore;
using WebReview.Domain.Entities;
using WebReview.Domain.Interfaces;
using WebReview.Infrastructure.Persistence;

namespace WebReview.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _context.Posts
            .AsNoTracking()
            .Include(x => x.User)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}