using System.Windows.Input;

namespace Widgets
{
    /// <summary>
    /// Button abstract class
    /// </summary>
    public abstract class NIQButton : ContentView
    {
        /// <summary>
        /// Disabled state color
        /// </summary>
        public Color DisabledColor => NIQThemeController.Theme.DisabledColor;

        /// <summary>
        /// Gets or sets dialog button flag, default value is set as false
        /// </summary>
        public bool IsDialogButton { get; set; } = false;

        /// <summary>
        /// Gets or sets text start aligment flag, default value is set as false
        /// </summary>
        public bool IsTextAligmentStart { get; set; } = false;

        /// <summary>
        /// Gets or sets multiline text allowed flag, default value is set as false
        /// </summary>
        public bool IsMultilineAllowed { get; set; } = false;

        /// <summary>
        /// IsEnabled property
        /// </summary>
        public static new readonly BindableProperty IsEnabledProperty =
            BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(NIQButton), true);

        /// <summary>
        /// Gets or sets enabled state
        /// </summary>
        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        /// <summary>
        /// Text property
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(NIQButton));

        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Command property
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(NIQButton));

        /// <summary>
        /// Command parameter property
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(NIQButton));

        /// <summary>
        /// Gets or sets command
        /// </summary>
        public ICommand Command
        {
            get => GetButtonCommand();
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Text color property
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NIQButton));

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets command parameter
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Pressed color property
        /// </summary>
        public static readonly BindableProperty PressedColorProperty =
            BindableProperty.Create(nameof(PressedColor), typeof(Color), typeof(NIQButton), defaultValue: NIQThemeController.Theme.PressedColor);

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public Color PressedColor
        {
            get => (Color)GetValue(PressedColorProperty);
            set => SetValue(PressedColorProperty, value);
        }
        /// <summary>
        /// Pressed Text color property
        /// </summary>
        public static readonly BindableProperty PressedTextColorProperty =
          BindableProperty.Create(nameof(PressedTextColor), typeof(Color), typeof(NIQButton));

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public Color PressedTextColor
        {
            get => (Color)GetValue(PressedTextColorProperty);
            set => SetValue(PressedTextColorProperty, value);
        }
        /// <summary>
        /// Font family property
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(NIQButton), defaultValue: NIQThemeController.Theme == Theme.Light ? default : "AktivGrotesk");

        /// <summary>
        /// Font Family
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Corner radius property
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(NIQButton));

        /// <summary>
        /// Corner radius
        /// </summary>
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Wait indicator property
        /// </summary>
        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(NIQColoredButton),
                defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets wait indicator
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// Wait indicator property
        /// </summary>
        public static readonly BindableProperty ButtonPaddingProperty =
            BindableProperty.Create(nameof(ButtonPadding), typeof(Thickness), typeof(NIQButton), new Thickness(10, 5));

        /// <summary>
        /// Gets or sets wait indicator
        /// </summary>
        public Thickness ButtonPadding
        {
            get => (Thickness)GetValue(ButtonPaddingProperty);
            set => SetValue(ButtonPaddingProperty, value);
        }

        /// <summary>
        /// Image source property
        /// </summary>
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(NIQButton));

        /// <summary>
        /// Image source
        /// </summary>
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        /// Gets button command
        /// </summary>
        private ICommand GetButtonCommand()
        {
            return new Command(() =>
            {
                var page = GetParentPage();
                if (page?.IsBusy ?? false)
                {
                    return;
                }

                page?.NotifyControlActivated();
                ((ICommand)GetValue(CommandProperty))?.Execute(CommandParameter);
            });
        }

        /// <summary>
        /// Gets the parent page
        /// </summary>
        /// <returns>Pagent page</returns>
        private NIQContentPage GetParentPage()
        {
            var result = this.Parent;
            while (result != null)
            {
                if (result is NIQContentPage cp)
                {
                    return cp;
                }
                result = result.Parent;
            }
            return result as NIQContentPage;
        }
    }
}
