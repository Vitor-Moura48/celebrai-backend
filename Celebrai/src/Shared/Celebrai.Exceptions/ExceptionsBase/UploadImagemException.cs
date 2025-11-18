using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class UploadImagemException : CelebraiException
{
    public UploadImagemException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadGateway;
}
