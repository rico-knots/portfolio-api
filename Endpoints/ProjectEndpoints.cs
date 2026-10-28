using PortfolioApi.Dtos.Project;
using PortfolioApi.Services;

namespace PortfolioApi.Endpoints;

public static class ProjectEndpoints
{
    public static void MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/projects").WithTags("Projects");

        group.MapGet("/", async (IProjectService service) =>
            Results.Ok(await service.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IProjectService service) =>
            await service.GetByIdAsync(id) is { } dto ? Results.Ok(dto) : Results.NotFound());

        group.MapPost("/", async (CreateProjectDto dto, IProjectService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/projects/{created.Id}", created);
        });

        group.MapPut("/{id:int}", async (int id, UpdateProjectDto dto, IProjectService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:int}", async (int id, IProjectService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}