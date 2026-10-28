using PortfolioApi.Dtos.Blogpost;

namespace PortfolioApi.Services;

public interface IBlogpostService
{
    Task<List<BlogpostDto>> GetAllAsync();
    Task<BlogpostDto?> GetByIdAsync(int id);
    Task<BlogpostDto> CreateAsync(CreateBlogpostDto dto);
    Task<bool> UpdateAsync(int id, UpdateBlogpostDto dto);
    
    Task<bool> DeleteAsync(int id);
}