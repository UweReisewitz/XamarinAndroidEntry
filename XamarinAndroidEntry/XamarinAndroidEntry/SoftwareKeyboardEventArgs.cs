using System;

namespace XamarinAndroidEntry
{
    public class SoftwareKeyboardEventArgs : EventArgs
    {
        public SoftwareKeyboardEventArgs(int keyboardheight)
        {
            KeyboardHeight = keyboardheight;
        }

        public int KeyboardHeight { get; private set; }
    }
}
