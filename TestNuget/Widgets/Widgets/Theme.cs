namespace Widgets
{
    /// <summary>
    /// Theme
    /// </summary>
    public class Theme
    {
        #region Constants
        public const string LightKey = "Light";
        public const string NIQLightKey = "NIQ Light";
        public const string NIQDarkKey = "NIQ Dark";
        public const string NIQRebrandKey = "NIQ Rebrand";
        #endregion

        #region Public properties
        public Color Primary { get; private set; }

        public Color PrimaryVariant { get; private set; }

        public Color Secondary { get; private set; }

        public Color SecondaryVariant { get; private set; }

        public Color OnSecondary { get; private set; }

        public Color Background { get; private set; }

        public Color Surface { get; private set; }

        public Color Positive { get; private set; }

        public Color Error { get; private set; }

        public Color Warning { get; private set; }

        public Color OnPrimary { get; private set; }

        public Color OnSurface { get; private set; }

        public Color OnSurfaceVariant { get; private set; }

        public Color OnBackground { get; private set; }

        public Color OnError { get; private set; }

        public Color SurfaceVariant { get; private set; }

        public Color ControlColor { get; private set; }

        public Color DisabledColor { get; set; }

        public Color PressedColor { get; set; }

        public Color OnPrimaryVariant { get; set; }
        public Color DialogInfoColor { get; set; }
        public Color HoverColor { get; set; }
        public Color PageBarBgColor { get; set; }

        public Color SelectedColor { get; set; }
        public Color TitleColor { get; set; }
        public Color DropDownPressedColor { get; set; }
        public Color StatusBarColor { get; set; }
        public Color AppBarBgColor { get; set; }

        #endregion

        private Theme() { }

        public static readonly Theme Light = new Theme()
        {
            Primary = Color.FromRgb(0, 174, 239),
            PrimaryVariant = Color.FromRgb(168, 235, 255),
            Secondary = Colors.Yellow,
            SecondaryVariant = Colors.LightYellow,
            OnSecondary = Colors.Black,
            Background = Colors.White,
            Surface = Color.FromRgb(238, 238, 238),
            Positive = Color.FromRgb(102, 209, 86),
            Error = Colors.Red,
            Warning = Color.FromRgb(255, 193, 7),
            OnPrimary = Colors.White,
            OnSurface = Colors.Black,
            OnSurfaceVariant = Color.FromRgb(130, 136, 148),
            OnBackground = Colors.Black,
            OnError = Colors.White,
            SurfaceVariant = Color.FromRgb(221, 221, 221),
            ControlColor = Color.FromRgb(0, 174, 239),
            DisabledColor = Colors.Gray,
            PressedColor = Color.FromRgb(168, 235, 255),
            OnPrimaryVariant = Color.FromRgb(0, 174, 239),
            HoverColor = Color.FromRgb(214, 254, 255),
            SelectedColor = Color.FromRgb(185, 248, 254),
            DropDownPressedColor = Color.FromHex("#d6feff"),
            DialogInfoColor = Colors.Black,
            TitleColor = Colors.Black,
            PageBarBgColor = Color.FromRgb(0, 174, 239),
            AppBarBgColor = Color.FromRgb(0, 174, 239)
        };

        public static readonly Theme NIQLight = new Theme()
        {
            Primary = Colors.Black,
            PrimaryVariant = Color.FromRgb(102, 102, 102),
            Secondary = Color.FromRgb(0, 240, 0),
            SecondaryVariant = Color.FromRgb(19, 130, 56),
            OnSecondary = Colors.Black,
            Background = Colors.White,
            Surface = Color.FromRgb(238, 238, 238),
            Positive = Color.FromRgb(0, 240, 0),
            Error = Color.FromRgb(240, 100, 0),
            Warning = Color.FromRgb(240, 100, 0),
            OnPrimary = Colors.White,
            OnSurface = Colors.Black,
            OnSurfaceVariant = Color.FromRgb(130, 136, 148),
            OnBackground = Colors.Black,
            OnError = Colors.White,
            SurfaceVariant = Color.FromRgb(221, 221, 221),
            ControlColor = Colors.Black,
            DisabledColor = Colors.Gray,
            PressedColor = Color.FromRgb(0, 240, 0),
            OnPrimaryVariant = Colors.Black,
            HoverColor = Color.FromRgb(214, 254, 255),
            SelectedColor = Color.FromRgb(185, 248, 254),
            DropDownPressedColor = Color.FromHex("#d6feff"),
            DialogInfoColor = Colors.Black,
            TitleColor = Colors.Black,
            PageBarBgColor = Colors.Black,
            AppBarBgColor = Colors.Black
        };

        public static readonly Theme NIQDark = new Theme()
        {
            Primary = Colors.White,
            PrimaryVariant = Color.FromRgb(102, 102, 102),
            Secondary = Color.FromRgb(0, 240, 0),
            SecondaryVariant = Color.FromRgb(19, 130, 56),
            OnSecondary = Colors.Black,
            Background = Colors.Black,
            Surface = Color.FromRgb(68, 68, 68),
            Positive = Color.FromRgb(0, 240, 0),
            Error = Color.FromRgb(240, 100, 0),
            Warning = Color.FromRgb(240, 100, 0),
            OnPrimary = Colors.Black,
            OnSurface = Colors.White,
            OnSurfaceVariant = Color.FromRgb(182, 182, 184),
            OnBackground = Colors.White,
            OnError = Colors.White,
            SurfaceVariant = Color.FromRgb(53, 53, 53),
            ControlColor = Colors.White,
            DisabledColor = Colors.Gray,
            PressedColor = Color.FromRgb(0, 240, 0),
            OnPrimaryVariant = Colors.Black,
            HoverColor = Color.FromRgb(214, 254, 255),
            SelectedColor = Color.FromRgb(185, 248, 254),
            DropDownPressedColor = Color.FromHex("#d6feff"),
            DialogInfoColor = Colors.White,
            TitleColor = Colors.White,
            PageBarBgColor = Colors.Black,
            AppBarBgColor = Colors.White
        };

        public static readonly Theme NIQRebrand = new Theme()
        {
            Primary = Color.FromRgb(45, 109, 246),
            Positive = Color.FromRgb(0, 138, 40),
            Error = Color.FromRgb(170, 33, 63),
            Background = Colors.White,
            Warning = Color.FromRgb(255, 181, 0),
            OnError = Colors.White,
            ControlColor = Color.FromRgb(45, 109, 246),
            DisabledColor = Colors.Gray,
            PrimaryVariant = Color.FromRgb(102, 102, 102),
            Secondary = Color.FromRgb(45, 109, 246),
            HoverColor = Color.FromRgb(231, 238, 255),
            SelectedColor = Color.FromRgb(230, 230, 230),
            PressedColor = Color.FromRgb(10, 82, 237),
            Surface = Color.FromRgb(242, 242, 242),
            SurfaceVariant = Color.FromRgb(141, 141, 141),
            SecondaryVariant = Color.FromRgb(45, 109, 246),
            OnSurface = Color.FromHex("#262626"),
            OnPrimaryVariant = Colors.White,
            OnBackground = Color.FromHex("#262626"),
            OnSecondary = Color.FromHex("#262626"),
            OnPrimary = Colors.White,
            OnSurfaceVariant = Color.FromRgb(141, 141, 141),
            TitleColor = Color.FromRgb(6, 10, 69),
            PageBarBgColor = Color.FromRgb(6, 10, 69),
            DropDownPressedColor = Color.FromRgb(230, 230, 230),
            DialogInfoColor = Color.FromHex("#3265D2"),
            StatusBarColor = Color.FromRgb(11, 17, 116),
            AppBarBgColor = Color.FromRgb(6, 10, 69)

        };
    }
}
