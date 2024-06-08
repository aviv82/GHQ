namespace GHQ.Common.Exceptions;

public class InvalidFilterException : BusinessException
{
    public InvalidFilterException(string propertyName) : base($"Invalid filter-value for '{propertyName}' received.") { }
}
