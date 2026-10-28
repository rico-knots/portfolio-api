namespace PortfolioApi.Dtos.Project;

public record CreateProjectDto(
    string Title,
    string Description,
    List<string> Tags,
    string GitHubUrl,
    DateTime Date);