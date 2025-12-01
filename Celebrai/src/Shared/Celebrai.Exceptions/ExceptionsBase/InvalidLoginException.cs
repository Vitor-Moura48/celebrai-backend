using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public class InvalidLoginException : CelebraiException
{
    public InvalidLoginException() : base("Email ou senha inválido")
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
