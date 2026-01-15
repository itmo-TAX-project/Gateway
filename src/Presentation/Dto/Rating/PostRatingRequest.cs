namespace Presentation.Dto.Rating;

public record PostRatingRequest(string? SubjectType, long SubjectId, long RaterId, int Stars, string? Comment);