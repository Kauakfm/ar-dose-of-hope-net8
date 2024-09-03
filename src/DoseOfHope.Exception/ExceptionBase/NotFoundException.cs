using System.Net;

namespace DoseOfHope.Exception.ExceptionBase
{
    public class NotFoundException : DoseOfHopeException
    {
        public NotFoundException(string message) : base(message) { }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
