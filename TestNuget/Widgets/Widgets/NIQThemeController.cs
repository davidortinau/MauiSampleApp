namespace Widgets
{
    /// <summary>
    /// Theme Controller
    /// </summary>
    public static class NIQThemeController
    {
        private static Theme theme = Theme.NIQRebrand;
        private static ResourceDictionary widgetsStyles;

        /// <summary>
        /// Current theme
        /// </summary>
        public static Theme Theme
        {
            get => theme;
            set
            {
                theme = value;
                if (Application.Current == null)
                {
                    Application.Current = new ThemeApp();
                }
                Application.Current.UserAppTheme = value == Theme.NIQDark ? AppTheme.Dark : AppTheme.Light;
                InitTheme();
            }
        }

        /// <summary>
        /// Init theme
        /// </summary>
        public static void InitTheme()
        {
            if (Application.Current.Resources.MergedDictionaries.Contains(widgetsStyles))
            {
                Application.Current.Resources.MergedDictionaries.Remove(widgetsStyles);
            }
            widgetsStyles = new ResourceDictionary()
            {
                {
                    GetPageStyle()
                },
                {
                    GetColoredButtonStyle()
                },
                {
                    GetBorderlessButtonStyle()
                },
                {
                    GetOutlineButtonStyle()
                },
                {
                    GetProgressBarStyle()
                },
                {
                    GetDialogStyle()
                },
                {
                    GetLabelStyle()
                },
                {
                    GetEntryStyle()
                },
                {
                    GetRadioButtonStyle()
                },
                {
                    "TitleStyle", GetTitleStyle()
                },
                {
                    "ErrorStyle", GetErrorStyle()
                },
                {
                    "BoldStyle", GetBoldTextStyle()
                }
            };
            Application.Current.Resources.Add(widgetsStyles);
            var statusBar = DependencyService.Get<IStatusBar>();
            /*statusBar.UpdateStatusBarColor(Theme == Theme.NIQDark ?
                            Theme.Background : Theme == Theme.NIQRebrand ? Theme.StatusBarColor : Theme.Primary);*/
        }

        /// <summary>
        /// Gets page style
        /// </summary>
        /// <returns>Page style</returns>
        private static Style GetPageStyle()
        {
            return new Style(typeof(Page))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter()
                    {
                        Property = Page.BackgroundColorProperty,
                        Value = Theme.Background
                    },
                    new Setter()
                    {
                        Property = Shell.ForegroundColorProperty,
                          Value = Theme == Theme.NIQDark ?
                            Theme.OnBackground : Theme.OnPrimary
                    },
                    new Setter()
                    {
                        Property = Shell.TitleColorProperty,
                        Value = Theme == Theme.NIQDark ?
                            Theme.OnBackground : Theme.OnPrimary
                    },
                    new Setter()
                    {
                        Property = TabbedPage.BarBackgroundColorProperty,
                        Value = Theme.PageBarBgColor
                    },
                    new Setter()
                    {
                        Property = TabbedPage.BarTextColorProperty,
                        Value = Theme.OnPrimary
                    },
                    new Setter()
                    {
                        Property = NavigationPage.BarBackgroundColorProperty,
                        Value = Theme.PageBarBgColor
                    },
                    new Setter()
                    {
                        Property = NavigationPage.BackgroundColorProperty,
                        Value = Theme.Background
                    },
                    new Setter()
                    {
                        Property = NavigationPage.BarTextColorProperty,
                        Value = Theme == Theme.NIQDark ?
                            Theme.OnBackground : Theme.OnPrimary
                    },
                    new Setter()
                    {
                        Property = Shell.BackgroundColorProperty,
                        Value = Theme.PageBarBgColor
                    },
                }
            };
        }

        /// <summary>
        /// Gets colored button style
        /// </summary>
        /// <returns>Colored button style</returns>
        private static Style GetColoredButtonStyle()
        {
            return new Style(typeof(NIQColoredButton))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = NIQColoredButton.BackgroundColorProperty,
                        Value = Theme.Primary
                    },
                    new Setter()
                    {
                        Property = NIQColoredButton.TextColorProperty,
                        Value = Theme.OnPrimary
                    },
                    new Setter()
                    {
                        Property = NIQColoredButton.PressedColorProperty,
                        Value = Theme.PressedColor
                    },
                    new Setter()
                    {
                        Property = NIQColoredButton.PressedTextColorProperty,
                        Value = Theme.OnPrimaryVariant
                    },
                    new Setter()
                    {
                        Property = NIQColoredButton.CornerRadiusProperty,
                        Value = Theme == Theme.Light ?
                            4 :Theme==Theme.NIQRebrand ? 2 : 0
                    },
                    new Setter()
                    {
                        Property = NIQColoredButton.FontFamilyProperty,
                         Value = Theme == Theme.Light ?
                            default :Theme==Theme.NIQRebrand ? "AktivGroteskBold" : "AktivGrotesk"
                    }
                },
            };
        }

        /// <summary>
        /// Gets outline button style
        /// </summary>
        /// <returns>Outline button style</returns>
        private static Style GetOutlineButtonStyle()
        {
            return new Style(typeof(NIQOutlineButton))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = NIQOutlineButton.BackgroundColorProperty,
                        Value = Colors.Transparent
                    },
                    new Setter()
                    {
                        Property = NIQOutlineButton.BorderColorProperty,
                        Value = Theme.Primary
                    },
                    new Setter()
                    {
                        Property = NIQOutlineButton.TextColorProperty,
                        Value = Theme.ControlColor
                    },
                    new Setter()
                    {
                        Property = NIQOutlineButton.PressedColorProperty,
                        Value = Theme.PressedColor
                    },
                    new Setter()
                    {
                        Property = NIQOutlineButton.CornerRadiusProperty,
                         Value = Theme == Theme.Light ?
                            4 :Theme==Theme.NIQRebrand ? 2 : 0
                    },
                    new Setter()
                    {
                        Property = NIQOutlineButton.FontFamilyProperty,
                        Value = Theme == Theme.Light ? default : "AktivGrotesk"
                    }
                },
            };
        }

        /// <summary>
        /// Gets borderless button style
        /// </summary>
        /// <returns>Borderless button style</returns>
        private static Style GetBorderlessButtonStyle()
        {
            return new Style(typeof(NIQBorderlessButton))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = NIQBorderlessButton.TextColorProperty,
                        Value = Theme.ControlColor
                    },
                    new Setter()
                    {
                        Property = NIQBorderlessButton.PressedColorProperty,
                        Value = Theme.PressedColor
                    },
                    new Setter()
                    {
                        Property = NIQBorderlessButton.CornerRadiusProperty,
                         Value = Theme == Theme.Light ?
                            4 :Theme==Theme.NIQRebrand ? 2 : 0
                    },
                    new Setter()
                    {
                        Property = NIQBorderlessButton.FontFamilyProperty,
                        Value = Theme == Theme.Light ? default : "AktivGrotesk"
                    }
                }
            };
        }

        /// <summary>
        /// Gets progress bar style
        /// </summary>
        /// <returns>Progress bar style</returns>
        private static Style GetProgressBarStyle()
        {
            return new Style(typeof(ProgressBar))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = ProgressBar.ProgressColorProperty,
                        Value = Theme == Theme.Light ?
                            Theme.Primary : Theme.Secondary
                    }
                }
            };
        }

        /// <summary>
        /// Gets label style
        /// </summary>
        /// <returns>Label style</returns>
        private static Style GetLabelStyle()
        {
            return new Style(typeof(Label))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter()
                    {
                        Property = Label.TextColorProperty,
                        Value = Theme.OnBackground
                    }
                },
                Triggers =
                {
                    new Trigger(typeof(Label))
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.None,
                        Setters =
                        {
                            new Setter()
                            {
                                Property = Label.FontFamilyProperty,
                                Value = Theme == Theme.Light ? default : "AktivGrotesk"
                            }
                        }
                    },
                    new Trigger(typeof(Label))
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.Bold,
                        Setters =
                        {
                            new Setter()
                            {
                                Value = Theme == Theme.Light ? default : "AktivGroteskBold"
                            }
                        }
                    },
                    new Trigger(typeof(Label))
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.Italic,
                        Setters =
                        {
                            new Setter()
                            {
                                Value = Theme == Theme.Light ? default : "AktivGroteskItalic"
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Gets entry style
        /// </summary>
        /// <returns>Entry style</returns>
        private static Style GetEntryStyle()
        {
            return new Style(typeof(Entry))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter()
                    {
                        Property = Entry.TextColorProperty,
                        Value = Theme.OnBackground
                    }
                },
                Triggers =
                {
                    new Trigger(typeof(Entry))
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.None,
                        Setters =
                        {
                            new Setter()
                            {
                                Property = Entry.FontFamilyProperty,
                                Value = Theme == Theme.Light ? default : "AktivGrotesk"
                            }
                        }
                    },
                    new Trigger(typeof(Entry))
                    {
                        Property = Entry.FontAttributesProperty,
                        Value = FontAttributes.Bold,
                        Setters =
                        {
                            new Setter()
                            {
                                Value = Theme == Theme.Light ? default : "AktivGroteskBold"
                            }
                        }
                    },
                    new Trigger(typeof(Entry))
                    {
                        Property = Entry.FontAttributesProperty,
                        Value = FontAttributes.Italic,
                        Setters =
                        {
                            new Setter()
                            {
                                Value = Theme == Theme.Light ? default : "AktivGroteskItalic"
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Gets radio button style
        /// </summary>
        /// <returns>Radio button style</returns>
        private static Style GetRadioButtonStyle()
        {
            return new Style(typeof(RadioButton))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter()
                    {
                        Property = RadioButton.FontFamilyProperty,
                        Value = Theme == Theme.Light ? default : "AktivGrotesk"
                    }
                }
            };
        }

        /// <summary>
        /// Gets dialog style
        /// </summary>
        /// <returns>Dialog style</returns>
        private static Style GetDialogStyle()
        {
            return new Style(typeof(NIQDialog))
            {
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter()
                    {
                        Property = NIQDialog.BackgroundColorProperty,
                        Value = Theme.Background
                    }
                }
            };
        }

        /// <summary>
        /// Gets title style
        /// </summary>
        /// <returns>Title style</returns>
        private static Style GetTitleStyle()
        {
            return new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = Label.FontSizeProperty,
                        Value = 24
                    },
                    new Setter()
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.Bold,
                    }
                }
            };
        }

        /// Gets Bold Text style
        /// </summary>
        /// <returns>Bold style</returns>
        private static Style GetBoldTextStyle()
        {
            return new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = Label.FontSizeProperty,
                        Value = 18
                    },
                    new Setter()
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.Bold,
                    }
                }
            };
        }

        /// <summary>
        /// Gets error style
        /// </summary>
        /// <returns>Error style</returns>
        private static Style GetErrorStyle()
        {
            return new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = Label.TextColorProperty,
                        Value = Theme.Error
                    },
                    new Setter()
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.None,
                    }
                }
            };
        }
    }
}
