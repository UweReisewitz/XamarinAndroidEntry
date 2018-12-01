using System;

namespace XamarinAndroidEntry.Droid
{
    public class SoftwareKeyboardService : ISoftwareKeyboardService
    {
        public virtual event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;

        private readonly Android.App.Activity _activity;
        private readonly GlobalLayoutListener _globalLayoutListener;

        public bool IsKeyboardVisible { get { return _globalLayoutListener.IsKeyboardVisible; } }

        public SoftwareKeyboardService(Android.App.Activity activity)
        {
            _activity = activity;
            _globalLayoutListener = new GlobalLayoutListener(activity, this);
            _activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(_globalLayoutListener);
        }

        internal void InvokeKeyboardHeightChanged(SoftwareKeyboardEventArgs args)
        {
            var handler = KeyboardHeightChanged;
            handler?.Invoke(this, args);
        }
    }
}
