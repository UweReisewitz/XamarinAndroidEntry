using System;
using Xamarin.Forms;

namespace XamarinAndroidEntry
{
    public class ExtendedEntry : Entry
    {
        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(ExtendedEntry), new Font());

        /// <summary>
        /// The XAlign property
        /// </summary>
        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(ExtendedEntry), TextAlignment.Start);

        /// <summary>
        /// The YAlign property
        /// </summary>
        public static readonly BindableProperty YAlignProperty =
            BindableProperty.Create("YAlign", typeof(TextAlignment), typeof(ExtendedEntry), TextAlignment.Start);

        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedEntry), true);

        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(ExtendedEntry), Color.Default);

        /// <summary>
        /// The ShowVirtualKeyboardOnFocus property
        /// </summary>
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty =
            BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);

        public IVirtualKeyboard VirtualKeyboardHandler { get; set; }

        public ExtendedEntry()
        {
            this.Focused += OnFocused;
        }

        public new bool Focus()
        {
            if (ShowVirtualKeyboardOnFocus)
            {
                ShowKeyboard();
            }
            else
            {
                HideKeyboard();
            }

            return true;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                if (ShowVirtualKeyboardOnFocus)
                {
                    ShowKeyboard();
                }
                else
                {
                    HideKeyboard();
                }
            }
        }

        public void ShowKeyboard()
        {
            if (VirtualKeyboardHandler != null)
            {
                VirtualKeyboardHandler.ShowKeyboard();
            }
        }

        public void HideKeyboard()
        {
            if (VirtualKeyboardHandler != null)
            {
                VirtualKeyboardHandler.HideKeyboard();
            }
        }

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        /// <summary>
        /// Gets or sets the Font
        /// </summary>
        public Font Font
        {
            get => (Font)GetValue(FontProperty);
            set => SetValue(FontProperty, value);
        }

        /// <summary>
        /// Gets or sets the X alignment of the text
        /// </summary>
        public TextAlignment XAlign
        {
            get => (TextAlignment)GetValue(XAlignProperty);
            set => SetValue(XAlignProperty, value);
        }

        /// <summary>
        /// Gets or sets the Y alignment of the text
        /// </summary>
        public TextAlignment YAlign
        {
            get => (TextAlignment)GetValue(YAlignProperty);
            set => SetValue(YAlignProperty, value);
        }

        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get => (bool)GetValue(HasBorderProperty);
            set => SetValue(HasBorderProperty, value);
        }

        /// <summary>
        /// Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get => (Color)GetValue(PlaceholderTextColorProperty);
            set => SetValue(PlaceholderTextColorProperty, value);
        }

        public event EventHandler<EventArgs> LeftSwipe;
        public event EventHandler<EventArgs> RightSwipe;

        public void OnLeftSwipe(object sender, EventArgs e)
        {
            LeftSwipe?.Invoke(this, e);
        }

        public void OnRightSwipe(object sender, EventArgs e)
        {
            RightSwipe?.Invoke(this, e);
        }

    }
}
