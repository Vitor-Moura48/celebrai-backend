using Celebrai.Domain.Dtos;

namespace Celebrai.Domain.Services.Cloudinary;
public interface ICloudinaryService
{
    public Task<CloudinaryUploadDto> UploadImage(Stream fileStream, string fileName);
}
