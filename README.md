# XamarinAndroidEntry
Custom Entry for Xamarin Forms with full control over the virtual keyboard

### Why this sample?
Our app (part of our ERP system) runs on several different devices with Windows CE, 
iOS and Android. Most of the data entered into the system is scanned but some data
(mostly quantities) has to be entered manually. Occasionally one has to enter some 
text as well. 

That implies that in some fields you would like to get the virtual keyboard immediately
while you want to get it in others only if needed.

In addition there are some devices that have a hardware keyboard. You don't want to
get the keyboard there altogether. Of course you would like to have full control over
this feature.

I have searched the internet quite a lot and found several samples but all of them 
lacked some functionality here and there for our requirements.

So I decided to combine them into one sample control and an create a small app to 
showcase the control.


### Features of the app:
- 3 Fields and a toolbar button to toggle the virtual keyboard on and off
- The first field does not show the keyboard on focus
- The second field has IsPassword=true set (keyboard off)
- The third field shows the virtual keyboard immediately on focus
- All fields have full support for copy (not on password, of course) and paste


### Features of the control:
1. Full control over the appearance and disappearance of the virtual keyboard
2. A property to control if the keyboard should appear on focus automatically
3. An additional object that gives an event when the keyboardheight changes
(= when the keyboard appears and diappears). It allows you to modify your
layout accordingly, if necessary. Please note that you don't need this if your
layout is done like in the sample app. In that case Android does quite a good
job to keep the important parts of your layout visible.


### A few notes regarding ExtendedEntry:

- The key point of this story is the following:
You **MUST** override the Focus() method of Entry and you **MUST NOT** call the base
implementation in your code. If you call base.Focus() the virtual keyvoard
will appear occasionally regardless what you do in your code. In my experience
you can reduce this but you will never get it fully under control.
- If you do not call base.Focus(), your code is much smaller and easier to 
understand and maintain. For example you do not have to meddle with the
inputtype-property to keep track of the keyboard type that should be shown.
- Using the IsPassword-Property works without any additional code (see sample)


### Some key lines in the code:
#### ExtendedEntryRenderer.OnElementChanged:
- Setting SetTextIsSelectable(true) has two purposes:
  1. It allows to copy and paste text without additional code
  2. The cursor will appear and blink even if the virtual keboard is not visible
     This is very helpful if you have a hardware keyboard or you would like to know
     where the next scan will enter text

- Setting SetSelectAllOnFocus(true) automatically selects the context of Entry() when it gets 
  the focus (regardless of the visibility of the keyboard)

#### App.OnStart:
- App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize)
  This is needed for the automatic resizing of your layout when the virtual keyboard appears 
  and disappears. Without that the whole screen will scroll not the scrollbar inside your
  view (see sample)

### Additions to Entry:
- Property ShowVirtualKeyboardOnFocusProperty (bindable)\
  When true shows the keyboard automatically when the cotrol gets the focus
- Method ShowKeyboard()\
  Show the virtual keyboard
- Method HideKeyboard()\
  Hide the virtual keyboard

### Object SoftwareKeyboardService:
- Event KeyboardHeightChanged(SoftwareKeyboardEventArgs e)\
  Fired when the area on the screen occupied by the keyboard changes.\
  SoftwareKeyboardEventArgs contaeins a field with the current height of the keyboard
  to allow you to justify your layout accordingly
- Property bool IsKeyboardVisible\
  Allows you to check if the virtual keyboard is currently visible
