namespace PortfolioApi.Dtos.Project;

public record ProjectDto(
    int Id,
    string Title,
    string Description,
    List<string> Tags,
    string GitHubUrl,
    DateTime Date);