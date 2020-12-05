using System;

namespace XamarinAndroidEntry.Droid
{
    public class SoftwareKeyboardService : ISoftwareKeyboardService
    {
        public virtual event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;

        private readonly Android.App.Activity activity;
        private readonly GlobalLayoutListener globalLayoutListener;

        public bool IsKeyboardVisible => globalLayoutListener.IsKeyboardVisible;

        public SoftwareKeyboardService(Android.App.Activity activity)
        {
            this.activity = activity;
            globalLayoutListener = new GlobalLayoutListener(activity, this);
            this.activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(this.globalLayoutListener);
        }

        internal void InvokeKeyboardHeightChanged(SoftwareKeyboardEventArgs args)
        {
            var handler = KeyboardHeightChanged;
            handler?.Invoke(this, args);
        }
    }
}
