using Cirrious.FluentLayouts.Touch;
using Foundation;
using System;
using UIKit;

namespace CRUDApp
{
    public partial class PinView : UIView
    {
        public PinView (IntPtr handle) : base (handle)
        {
        }

        public PinView()
        {
            var arr = NSBundle.MainBundle.LoadNib(nameof(PinView), this, null);
            var rootView = ObjCRuntime.Runtime.GetNSObject(arr.ValueAt(0)) as PinView;

            var backgroundImage = new UIImageView(UIScreen.MainScreen.Bounds);
            backgroundImage.Image = new UIImage("login_background.png");
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            rootView.InsertSubview(backgroundImage, 0);

            Button1 = rootView.button1;
            Button2 = rootView.button2;
            Button3 = rootView.button3;
            Button4 = rootView.button4;
            Button5 = rootView.button5;
            Button6 = rootView.button6;
            Button7 = rootView.button7;
            Button8 = rootView.button8;
            Button9 = rootView.button9;

            SetupButtons();
            SubscribeOnEvents();

            AddSubview(rootView);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints(
                rootView.WithSameTop(this),
                rootView.WithSameBottom(this),
                rootView.WithSameLeft(this),
                rootView.WithSameRight(this));
        }

        public UIButton Button1 { get; private set; }
        public UIButton Button2 { get; private set; }
        public UIButton Button3 { get; private set; }
        public UIButton Button4 { get; private set; }
        public UIButton Button5 { get; private set; }
        public UIButton Button6 { get; private set; }
        public UIButton Button7 { get; private set; }
        public UIButton Button8 { get; private set; }
        public UIButton Button9 { get; private set; }

        private void SetupButtons()
        {
            ResetButtonStyle(Button1);
            ResetButtonStyle(Button2);
            ResetButtonStyle(Button3);
            ResetButtonStyle(Button4);
            ResetButtonStyle(Button5);
            ResetButtonStyle(Button6);
            ResetButtonStyle(Button7);
            ResetButtonStyle(Button8);
            ResetButtonStyle(Button9);
        }

        private void ResetButtonStyle(UIButton button) 
        {
            button.Layer.BorderWidth = 1.0f;
            button.Layer.BackgroundColor = UIColor.Clear.CGColor;
            button.Layer.BorderColor = UIColor.White.CGColor;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
        }

        private void SetButtonPressStyle(UIButton button)
        {
        }

        private void SubscribeOnEvents() 
        {
            Button1.TouchUpInside += Button_TouchUpInside;
            Button2.TouchUpInside += Button_TouchUpInside;
            Button3.TouchUpInside += Button_TouchUpInside;
            Button4.TouchUpInside += Button_TouchUpInside;
            Button5.TouchUpInside += Button_TouchUpInside;
            Button6.TouchUpInside += Button_TouchUpInside;
            Button7.TouchUpInside += Button_TouchUpInside;
            Button8.TouchUpInside += Button_TouchUpInside;
            Button9.TouchUpInside += Button_TouchUpInside;
        }

        private void UnsubscribeFromEvents()
        {
            Button1.TouchUpInside -= Button_TouchUpInside;
            Button2.TouchUpInside -= Button_TouchUpInside;
            Button3.TouchUpInside -= Button_TouchUpInside;
            Button4.TouchUpInside -= Button_TouchUpInside;
            Button5.TouchUpInside -= Button_TouchUpInside;
            Button6.TouchUpInside -= Button_TouchUpInside;
            Button7.TouchUpInside -= Button_TouchUpInside;
            Button8.TouchUpInside -= Button_TouchUpInside;
            Button9.TouchUpInside -= Button_TouchUpInside;
        }

        private void Button_TouchUpInside(object sender, EventArgs e)
        {
            if (sender is UIButton button)
            {
                button.Layer.BackgroundColor = UIColor.White.CGColor;
                button.SetTitleColor(UIColor.FromRGB(50, 50, 50), UIControlState.Normal);

                var timer = NSTimer.CreateTimer(TimeSpan.FromSeconds(0.2), delegate
                {
                    button.Layer.BackgroundColor = UIColor.Clear.CGColor;
                    button.SetTitleColor(UIColor.White, UIControlState.Normal);
                });
                timer.Fire();
            }
        }
    }
}