namespace Application.Models;

public class Rating
{
    public string SubjectType { get; init; } = string.Empty;

    public long SubjectId { get; init; }

    public long RaterId { get; init; }

    public int Stars { get; init; }

    public string Comment { get; init; } = string.Empty;
}