using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OneClickSocialMedia.Business
{
    public static class MediatRInjection
    {
        /// <summary>
        /// Method to add all mediatR handlers to dependency injection from it's assembly.
        /// </summary>
        /// <param name="services">Reference to service collection to add handlers to.</param>
        /// <returns>Service collection with mediatR handlers added.</returns>
        public static IServiceCollection AddMediatRContracts(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
