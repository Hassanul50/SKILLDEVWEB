
using SKILLDEVWEB.DataAccess.Repository;
using SKILLDEVWEB.DataAccess.Repository.IRepository;

namespace SKILLDEVWEB
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
