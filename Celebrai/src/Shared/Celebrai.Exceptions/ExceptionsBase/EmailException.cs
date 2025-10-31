using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class EmailException : CelebraiException
{
    public EmailException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.ServiceUnavailable;
}
