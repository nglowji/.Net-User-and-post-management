using FluentValidation;
using MediatR;

namespace WebReview.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errors = _validators
            .Select(v => v.Validate(request))
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .ToList();

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
