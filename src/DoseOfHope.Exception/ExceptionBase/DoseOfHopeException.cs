namespace DoseOfHope.Exception.ExceptionBase;

public abstract class DoseOfHopeException : SystemException
{
    protected DoseOfHopeException(string message) : base(message) { }

    public abstract int StatusCode { get;}

    public abstract List<string> GetErrors();
}
