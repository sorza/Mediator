﻿using Mediator.Abstractions;

namespace Mediator;
public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        //Obtendo tipo
        var requestType = request.GetType();
        var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        //Lidando com Injeção de dependencia
        var handler = serviceProvider.GetService(handlerType);

        if (handler is null)
            throw new InvalidOperationException($"Handler not found for {requestType}");

        //Invocando métodos
        var method = handlerType.GetMethod("HandleAsync");
        
        var result = method?.Invoke(handler, [request, cancellationToken]);

        if (result is not Task<TResponse> task)
            throw new InvalidOperationException($"Method returned unexpected type {result}");

        return await task;

    }
}

