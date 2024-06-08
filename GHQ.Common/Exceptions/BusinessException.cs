namespace GHQ.Common.Exceptions;

/// <summary>
/// Abstract base class for business value exceptions that require feedback towards the user.
/// </summary>
public abstract class BusinessException : Exception
{
    protected BusinessException()
    {
    }

    protected BusinessException(string message)
        : base(message)
    {
    }

    protected BusinessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
