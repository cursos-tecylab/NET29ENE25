namespace Usuario.Application.Exceptions;

public class ValidationExceptions : Exception
{
    public ValidationExceptions(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get;}

}