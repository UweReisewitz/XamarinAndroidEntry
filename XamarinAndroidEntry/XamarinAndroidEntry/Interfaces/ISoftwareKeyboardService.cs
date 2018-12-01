using System;

namespace XamarinAndroidEntry
{
    public interface ISoftwareKeyboardService
    {
        event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;
        bool IsKeyboardVisible { get; }
    }
}
