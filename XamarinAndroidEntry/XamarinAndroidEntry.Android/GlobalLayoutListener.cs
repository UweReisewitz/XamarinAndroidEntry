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
        private static InputMethodManager inputManager;
        private static Activity activity;
        private readonly SoftwareKeyboardService softwarekeyboardservice;
        private static Android.Views.View childOfContent;
        private static float displayDensity;
        private static int displayheight;
        private static int extrascreenheight;
        private static int keyboardheight;

        private static void ObtainInputManager()
        {
            inputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
        }

        public GlobalLayoutListener(Android.App.Activity activity, SoftwareKeyboardService softwarekeyboardservice)
        {
            this.softwarekeyboardservice = softwarekeyboardservice;
            GlobalLayoutListener.activity = activity;
            ObtainInputManager();
            GetDisplayData();
        }

        private void GetDisplayData()
        {
            var content = (FrameLayout)activity.FindViewById(Android.Resource.Id.Content);
            childOfContent = content.GetChildAt(0);

            var metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            displayDensity = metrics.Density;

            CalculateDisplayHeight();
        }

        public void OnGlobalLayout()
        {
            if (inputManager.Handle == IntPtr.Zero)
            {
                ObtainInputManager();
            }

            if (displayheight != childOfContent.RootView.Height)
            {
                CalculateDisplayHeight();
            }

            var keyboardheight = CalculateKeyboardHeight();

            if (keyboardheight != GlobalLayoutListener.keyboardheight)
            {
                GlobalLayoutListener.keyboardheight = keyboardheight;
                this.softwarekeyboardservice.InvokeKeyboardHeightChanged(new SoftwareKeyboardEventArgs(ConvertPixelsToDp((float)keyboardheight)));
            }
        }

        public bool IsKeyboardVisible => keyboardheight != 0;

        private static void CalculateDisplayHeight()
        {
            var r = new Rect();
            childOfContent.GetWindowVisibleDisplayFrame(r);
            var visibleheight = r.Height();
            displayheight = childOfContent.RootView.Height;
            extrascreenheight = displayheight - visibleheight;
        }

        private static int CalculateKeyboardHeight()
        {
            var r = new Rect();
            childOfContent.GetWindowVisibleDisplayFrame(r);

            var visibleheight = r.Height();
            return Math.Max(displayheight - visibleheight - extrascreenheight, 0);
        }

        private static int ConvertPixelsToDp(float pixelValue)
        {
            return (int)((pixelValue) / displayDensity);
        }
    }
}
