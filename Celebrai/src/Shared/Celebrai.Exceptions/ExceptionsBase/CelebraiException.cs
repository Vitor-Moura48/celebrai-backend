using System.Net;

namespace Celebrai.Exceptions.ExceptionsBase;
public abstract class CelebraiException : SystemException
{
    protected CelebraiException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
