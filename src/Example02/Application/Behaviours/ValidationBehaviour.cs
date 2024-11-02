using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Example02.Application.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            await ValidateAsync(request, cancellationToken);
        }
        
        return await next();
    }

    private async Task ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var tasks = _validators
            .Select(x => x.ValidateAsync(context, cancellationToken))
            .ToList();
        var validationResults = await Task.WhenAll(tasks);
        var failures = validationResults
            .Where(x => x.Errors.Count != 0)
            .SelectMany(x => x.Errors)
            .ToList();
        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }
    }
}