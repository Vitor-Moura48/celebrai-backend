using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class UnauthorizedException : CelebraiException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
