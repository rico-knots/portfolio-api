namespace PortfolioApi.Dtos.Blogpost;

public record CreateBlogpostDto(
    string Title,
    string Content,
    DateTime PublishedAt);