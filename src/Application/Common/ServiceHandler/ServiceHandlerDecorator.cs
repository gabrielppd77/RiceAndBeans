using ErrorOr;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Services;

public class ServiceHandlerDecorator<TRequest, TResponse> : IServiceHandler<TRequest, TResponse>
{
    private readonly IServiceHandler<TRequest, TResponse> _inner;
    private readonly IValidator<TRequest>? _validator;

    public ServiceHandlerDecorator(
        IServiceHandler<TRequest, TResponse> inner,
        IServiceProvider provider)
    {
        _inner = inner;
        _validator = provider.GetService<IValidator<TRequest>>();
    }

    public async Task<TResponse> Handler(TRequest request)
    {
        if (_validator is null)
        {
            return await _inner.Handler(request);
        }

        var validationResult = await _validator.ValidateAsync(request);

        if (validationResult.IsValid)
        {
            return await _inner.Handler(request);
        }

        var errors = validationResult.Errors
            .ConvertAll(validationFailure =>
                Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}