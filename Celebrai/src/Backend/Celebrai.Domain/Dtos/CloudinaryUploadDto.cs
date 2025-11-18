namespace Celebrai.Domain.Dtos;
public record CloudinaryUploadDto
{
    public string ImageUrl { get; init; } = string.Empty;
    public string ImagemPublicId { get; init; } = string.Empty;
}
