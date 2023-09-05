using Android.Text;
using Android.Content;
using Android.Graphics.Drawables;
using Widgets.Droid.Renderers;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

[assembly: Microsoft.Maui.Controls.Compatibility.ExportRenderer(typeof(Editor), typeof(NIQEditorRenderer))]
namespace Widgets.Droid.Renderers
{
    /// <summary>
    /// Editor view renderer
    /// </summary>
    public class NIQEditorRenderer : EditorRenderer
    {
        public NIQEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}
