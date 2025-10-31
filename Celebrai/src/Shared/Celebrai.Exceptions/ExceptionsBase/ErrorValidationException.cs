using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class ErrorValidationException : CelebraiException
{
    private readonly IList<string> _errorMessages;
    public ErrorValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
