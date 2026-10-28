using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Dtos.Project;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class ProjectService(AppDbContext db) : IProjectService
{
    public async Task<List<ProjectDto>> GetAllAsync() =>
        await db.Projects
                .Select(p => ToDto(p))
                .ToListAsync();

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
            var project = await db.Projects.FindAsync(id);
            return project is null ? null : ToDto(project);
    }
    
    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Title = dto.Title,
            Description = dto.Description,
            Tags = dto.Tags,
            GitHubUrl = dto.GitHubUrl,
            LastUpdated = dto.Date
        };
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return ToDto(project);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
    {
        var project = await db.Projects.FindAsync(id);
        if (project is null) return false;

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Tags = dto.Tags;
        project.GitHubUrl = dto.GitHubUrl;
        project.LastUpdated = dto.Date;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await db.Projects.FindAsync(id);
        if (project is null) return false;
        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return true;
    }


    private static ProjectDto ToDto(Project p) =>
        new(p.Id, p.Title, p.Description, p.Tags, p.GitHubUrl, p.LastUpdated);
}