namespace Application.Common.Services;

public interface IServiceHandler<TRequest, TResponse>
{
    Task<TResponse> Handler(TRequest request);
}