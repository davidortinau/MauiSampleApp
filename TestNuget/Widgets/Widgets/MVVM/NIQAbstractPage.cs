using Utilities;

namespace Widgets
{
    public class NIQAbstractPage : ContentPage
    {
        /// <summary>
        /// Sets bindings context.
        /// </summary>
        /// <param name="viewModelType">Type of view model.</param>
        public void SetBindingContext(Type viewModelType)
        {
            BindingContext = DIController.GetService(viewModelType);
        }
    }
}
