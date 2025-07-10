namespace Application.Common.ServiceHandler;

public interface IServiceHandler<TRequest, TResponse>
{
    Task<TResponse> Handler(TRequest request);
}