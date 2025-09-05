namespace ApplicationCommon.CustomException
{
    public class UserDefinedException : Exception
    {
        public UserDefinedException()
        {

        }
        public UserDefinedException(string message) : base(String.Format("{0}", message))
        {

        }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class InvalidRequest : Exception
    {
        public InvalidRequest(string message) : base(message) { }
    }
    public class Badrequest : Exception
    {
        public Badrequest(string message) : base(message) { }
    }
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message) { }
    }
}
