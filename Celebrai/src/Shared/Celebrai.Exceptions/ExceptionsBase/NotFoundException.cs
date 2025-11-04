using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class NotFoundException : CelebraiException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
