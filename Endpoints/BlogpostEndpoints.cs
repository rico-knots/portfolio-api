using PortfolioApi.Dtos.Blogpost;
using PortfolioApi.Services;

namespace PortfolioApi.Endpoints;

public static class BlogpostEndpoints
{
    public static void MapBlogpostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/blogposts").WithTags("Blogposts");

        group.MapGet("/", async (IBlogpostService service) =>
            Results.Ok(await service.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IBlogpostService service) =>
            await service.GetByIdAsync(id) is { } dto ? Results.Ok(dto) : Results.NotFound());

        group.MapPost("/", async (CreateBlogpostDto dto, IBlogpostService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/blogposts/{created.Id}", created);
        });

        group.MapPut("/{id:int}", async (int id, UpdateBlogpostDto dto, IBlogpostService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("/{id:int}", async (int id, IBlogpostService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}