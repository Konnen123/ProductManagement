using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Application.Utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IValidator<CreateProductCommand>), typeof(CreateProductCommandValidator));
            services.AddTransient(typeof(IValidator<UpdateProductCommand>), typeof(UpdateProductCommandValidator));
            services.AddTransient(typeof(IValidator<DeleteProductCommand>), typeof(DeleteProductCommandValidator));
            services.AddTransient(typeof(IValidator<GetProductByIdQuery>), typeof(GetProductByIdQueryValidator));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

    }
}
