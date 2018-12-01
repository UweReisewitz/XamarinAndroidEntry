using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;


namespace XamarinAndroidEntry.Droid
{
    internal class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private static InputMethodManager _inputManager;
        private static Activity _activity;
        private SoftwareKeyboardService _softwarekeyboardservice;
        private static Android.Views.View _childOfContent;
        private static float _displayDensity;
        private static int _displayheight;
        private static int _extrascreenheight;
        private static int _keyboardheight;

        private static void ObtainInputManager()
        {
            _inputManager = (InputMethodManager)_activity.GetSystemService(Context.InputMethodService);
        }

        public GlobalLayoutListener(Android.App.Activity activity, SoftwareKeyboardService softwarekeyboardservice)
        {
            _softwarekeyboardservice = softwarekeyboardservice;
            _activity = activity;
            ObtainInputManager();
            GetDisplayData();
        }

        private void GetDisplayData()
        {
            FrameLayout content = (FrameLayout)_activity.FindViewById(Android.Resource.Id.Content);
            _childOfContent = content.GetChildAt(0);

            DisplayMetrics metrics = new DisplayMetrics();
            _activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            _displayDensity = metrics.Density;

            CalculateDisplayHeight();
        }

        public void OnGlobalLayout()
        {
            if (_inputManager.Handle == IntPtr.Zero)
            {
                ObtainInputManager();
            }

            if (_displayheight != _childOfContent.RootView.Height)
            {
                CalculateDisplayHeight();
            }

            int keyboardheight = CalculateKeyboardHeight();

            if (keyboardheight != _keyboardheight)
            {
                _keyboardheight = keyboardheight;
                _softwarekeyboardservice.InvokeKeyboardHeightChanged(new SoftwareKeyboardEventArgs(ConvertPixelsToDp((float)keyboardheight)));
            }
        }

        public bool IsKeyboardVisible { get { return _keyboardheight != 0; } }

        private static void CalculateDisplayHeight()
        {
            Rect r = new Rect();
            _childOfContent.GetWindowVisibleDisplayFrame(r);
            int visibleheight = r.Height();
            _displayheight = _childOfContent.RootView.Height;
            _extrascreenheight = _displayheight - visibleheight;
        }

        private static int CalculateKeyboardHeight()
        {
            Rect r = new Rect();
            _childOfContent.GetWindowVisibleDisplayFrame(r);

            int visibleheight = r.Height();
            return Math.Max(_displayheight - visibleheight - _extrascreenheight, 0);
        }

        private static int ConvertPixelsToDp(float pixelValue)
        {
            return (int)((pixelValue) / _displayDensity);
        }
    }
}
