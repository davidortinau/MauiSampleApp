namespace DependencyInjection
{
    /// <summary>
    /// Container module interfcace
    /// </summary>
    public interface IContainerModule
    {
        /// <summary>
        /// Adds services to the colelction
        /// </summary>
        /// <param name="collection">Collection</param>
        void AddServices(IServiceCollection collection);
    }
}
