namespace Widgets
{
    public interface INavigationPopInterceptor
    {
        /// <summary>
        /// Servise property for iOS
        /// </summary>
        bool IsPopRequest { get; set; }

        /// <summary>
        /// Requests pop
        /// </summary>
        /// <returns>Return true if pop is alllowed.</returns>
        Task<bool> RequestPop();
    }
}
