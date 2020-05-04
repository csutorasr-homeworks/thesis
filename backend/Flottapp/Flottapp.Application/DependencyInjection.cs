using AutoMapper;
using Flottapp.Application.Providers;
using Flottapp.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructue(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
