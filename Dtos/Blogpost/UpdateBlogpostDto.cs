namespace PortfolioApi.Dtos.Blogpost;

public record UpdateBlogpostDto(
    string Title,
    string Content,
    DateTime PublishedAt);