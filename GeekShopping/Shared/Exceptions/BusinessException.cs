namespace Shared.Exceptions;

public sealed class BusinessException : Exception
{
    public BusinessException(string error) : base(error) { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new BusinessException(error);
        }
    }
}
