using CommunityToolkit.Mvvm.DependencyInjection;

namespace Utilities
{
    /// <summary>
    /// Dependency injection controller
    /// </summary>
    public class DIController
    {
        /// <summary>
        /// Configures services
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public static void ConfigureServices(IServiceProvider serviceProvider)
        {
            Ioc.Default.ConfigureServices(serviceProvider);
        }

        /// <summary>
        /// Gets the service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service</returns>
        public static T GetService<T>() where T : class
        {
            return Ioc.Default.GetService<T>();
        }

        /// <summary>
        /// Gets the service
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Service</returns>
        public static object GetService(Type type)
        {
            return Ioc.Default.GetService(type);
        }
    }
}
