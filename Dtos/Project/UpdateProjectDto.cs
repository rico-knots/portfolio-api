namespace PortfolioApi.Dtos.Project;

public record UpdateProjectDto(
    string Title,
    string Description,
    List<string> Tags,
    string GitHubUrl,
    DateTime Date);