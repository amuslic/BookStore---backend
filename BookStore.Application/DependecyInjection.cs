using BookStore.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application
{
    public static class DependecyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserHandler, UserHandler>();

        }
    }
}
