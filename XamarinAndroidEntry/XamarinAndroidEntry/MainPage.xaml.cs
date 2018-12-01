using System;
using Xamarin.Forms;

namespace XamarinAndroidEntry
{
    public partial class MainPage : ContentPage
    {
        private ISoftwareKeyboardService _softwarekeyboardservice;

        public MainPage(ISoftwareKeyboardService softwarekeyboardservice)
        {
            InitializeComponent();

            _softwarekeyboardservice = softwarekeyboardservice;

            _softwarekeyboardservice.KeyboardHeightChanged += _softwarekeyboardservice_KeyboardHeightChanged;
            UserNameEntry.Focused += Entry_Focused;
            PasswordEntry.Focused += Entry_Focused;
            DomainEntry.Focused += Entry_Focused;
        }

        private void _softwarekeyboardservice_KeyboardHeightChanged(object sender, SoftwareKeyboardEventArgs e)
        {
            // Handle layout changes regarding the keyboard here, if needed
            if (e.KeyboardHeight == 0)
            {
                // keyboard is not visible
            }
            else
            {
                // Keyboard is visible
            }
        }

        private ExtendedEntry _currententry;
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            _currententry = sender as ExtendedEntry;
        }

        private void KeyboardToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (_currententry != null)
            {
                if (_softwarekeyboardservice.IsKeyboardVisible)
                    _currententry.HideKeyboard();
                else
                    _currententry.ShowKeyboard();
            }
        }
    }
}
