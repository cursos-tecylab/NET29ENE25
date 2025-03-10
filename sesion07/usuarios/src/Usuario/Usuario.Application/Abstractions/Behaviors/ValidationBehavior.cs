
using FluentValidation;
using MediatR;
using Usuario.Application.Abstractions.Messaging;
using Usuario.Application.Exceptions;

namespace Usuario.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{

    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
       if (!_validator.Any())
       {
         return await next();
       }

        var context = new ValidationContext<TRequest>(request);
        var validationErrores = _validator
        .Select(validator => validator.Validate(context) )
        .Where(validationResult => validationResult.Errors.Any())
        .SelectMany(validationResult => validationResult.Errors)
        .Select(
            validatorFailure => new ValidationError
            (
                validatorFailure.PropertyName,
                validatorFailure.ErrorMessage
            )
        ).ToList();

        if( validationErrores.Any())
        {
            throw new ValidationExceptions(validationErrores);
        }
        return await next();
    }
}