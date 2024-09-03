using System.Net;

namespace DoseOfHope.Exception.ExceptionBase;

public class InvalidLoginException : DoseOfHopeException
{
    public InvalidLoginException() : base(ResourceErrorMessages.LOGIN_INVALIDO) { }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
