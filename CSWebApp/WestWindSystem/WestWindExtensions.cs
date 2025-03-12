using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using WestWindSystem.BLL;
using WestWindSystem.DAL;

namespace WestWindSystem
{
    /// <summary>
    /// Public so it's accessable from WestWindApp.
    /// Static as it's purely datatypes.
    /// All of our services will be assigned and linked here.
    /// </summary>
    public static class WestWindExtensions
    {

        /// <summary>
        /// This method is going to hook up all of our services.
        /// </summary>
        /// <param name="services">IServiceCollection we're adding services to</param>
        /// <param name="options">DB Connection String</param>
        public static void WestWindExtensionServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<WestWindContext>(options);

            //All services in the BLL must be registered here.
            #region Services

            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new BuildVersionServices(context);
            });

            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new RegionServices(context);
            });

            services.AddTransient<ShipmentServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ShipmentServices(context);
            });

            services.AddTransient<ShipperServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();

                return new ShipperServices(context);
            });
            #endregion
        }
    }
}
