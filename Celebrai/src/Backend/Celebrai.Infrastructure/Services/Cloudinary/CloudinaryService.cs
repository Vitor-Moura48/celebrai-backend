using Celebrai.Domain.Dtos;
using Celebrai.Domain.Services.Cloudinary;
using Celebrai.Exceptions.ExceptionsBase;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Celebrai.Infrastructure.Services.Cloudinary;
public class CloudinaryService : ICloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryService(CloudinaryDotNet.Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<CloudinaryUploadDto> UploadImage(Stream fileStream, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Folder = "produtos"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            var erroDetalhado = $"Erro ao enviar a imagem para o Cloudinary: {result.Error?.Message}";

            throw new UploadImagemException(erroDetalhado);
        }

        return new CloudinaryUploadDto
        {
            ImageUrl = result.SecureUrl.ToString(),
            ImagemPublicId = result.PublicId
        };
    }
}
