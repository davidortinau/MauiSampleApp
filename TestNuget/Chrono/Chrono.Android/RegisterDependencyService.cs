using Widgets.Droid;
using Widgets;
using Microsoft.Maui.Controls;

namespace Authority.Droid
{
    /// <summary>
    /// Register All the Dependencies
    /// </summary>
    public class RegisterDependencyService
    {
        public static void Register()
        {
            DependencyService.Register<IViewHelper, ViewHelper>();
            DependencyService.Register<IStatusBar, StatusBar>();
        }
    }
}
