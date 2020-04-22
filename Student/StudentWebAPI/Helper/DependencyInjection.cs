using Microsoft.Extensions.DependencyInjection;
using StudentRepositroy;
using IStudentRepository;

namespace StudentWebAPI.Helper
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddSingleton<IStudentRepo, StudentRepo>();
        }
    }
}
