namespace PortfolioApi.Dtos.Blogpost;

public record BlogpostDto(
    int Id,
    string Title,
    string Content,
    DateTime PublishedAt);