﻿using Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mediator.Extensions
{
    public static class MediatorExtension
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient<IMediator, Mediator>();

            var handlerType = typeof(IHandler<,>);

            foreach (var assembly in assemblies)
            {
                var handlers = assembly
                    .GetTypes()
                    .Where(type => !type.IsAbstract && !type.IsInterface)
                    .SelectMany(x => x.GetInterfaces(), (t,i) => new { Type = t, Interface = i})
                    .Where(ti => ti.Interface.IsGenericType && ti.Interface.GetGenericTypeDefinition() == handlerType);

                foreach (var handler in handlers)
                    services.AddTransient(handler.Interface, handler.Type);
            }

            return services;
        }
    }
}
