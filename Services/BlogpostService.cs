using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos.Blogpost;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class BlogpostService(AppDbContext db) : IBlogpostService
{
    public async Task<List<BlogpostDto>> GetAllAsync() =>
        await db.Blogposts
            .Select(p => ToDto(p))
            .ToListAsync();

    public async Task<BlogpostDto?> GetByIdAsync(int id)
    {
        var post = await db.Blogposts.FindAsync(id);
        return post is null ? null : ToDto(post);
    }
    
    public async Task<BlogpostDto> CreateAsync(CreateBlogpostDto dto)
    {
        var post = new Blogpost
        {
            Title = dto.Title,
            Content = dto.Content,
            PublishedAt = dto.PublishedAt
        };
        db.Blogposts.Add(post);
        await db.SaveChangesAsync();
        return ToDto(post);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogpostDto dto)
    {
        var post = await db.Blogposts.FindAsync(id);
        if (post is null) return false;

        post.Title = dto.Title;
        post.Content = dto.Content;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await db.Blogposts.FindAsync(id);
        if (post is null) return false;
        db.Blogposts.Remove(post);
        await db.SaveChangesAsync();
        return true;
    }


    private static BlogpostDto ToDto(Blogpost b) =>
        new(b.Id, b.Title, b.Content, b.PublishedAt);
}