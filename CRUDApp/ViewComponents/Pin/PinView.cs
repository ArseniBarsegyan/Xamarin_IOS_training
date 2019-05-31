using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Pin
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

            var backgroundImage = new UIImageView(UIScreen.MainScreen.Bounds)
            {
                Image = new UIImage("login_background.png"),
                ContentMode = UIViewContentMode.ScaleAspectFill
            };
            rootView?.InsertSubview(backgroundImage, 0);

            Pin1 = rootView?.pin1;
            Pin2 = rootView?.pin2;
            Pin3 = rootView?.pin3;
            Pin4 = rootView?.pin4;

            Button1 = rootView?.button1;
            Button2 = rootView?.button2;
            Button3 = rootView?.button3;
            Button4 = rootView?.button4;
            Button5 = rootView?.button5;
            Button6 = rootView?.button6;
            Button7 = rootView?.button7;
            Button8 = rootView?.button8;
            Button9 = rootView?.button9;
            Button0 = rootView?.button0;
            ButtonX = rootView?.buttonX;

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

        public UIButton Pin1 { get; }
        public UIButton Pin2 { get; }
        public UIButton Pin3 { get; }
        public UIButton Pin4 { get; }

        public UIButton Button1 { get; }
        public UIButton Button2 { get; }
        public UIButton Button3 { get; }
        public UIButton Button4 { get; }
        public UIButton Button5 { get; }
        public UIButton Button6 { get; }
        public UIButton Button7 { get; }
        public UIButton Button8 { get; }
        public UIButton Button9 { get; }
        public UIButton Button0 { get; }
        public UIButton ButtonX { get; }

        private void SetupButtons()
        {
            ResetButtonStyle(Pin1);
            ResetButtonStyle(Pin2);
            ResetButtonStyle(Pin3);
            ResetButtonStyle(Pin4);
            ResetButtonStyle(Button1);
            ResetButtonStyle(Button2);
            ResetButtonStyle(Button3);
            ResetButtonStyle(Button4);
            ResetButtonStyle(Button5);
            ResetButtonStyle(Button6);
            ResetButtonStyle(Button7);
            ResetButtonStyle(Button8);
            ResetButtonStyle(Button9);
            ResetButtonStyle(Button0);
            ResetButtonStyle(ButtonX);
        }

        private void ResetButtonStyle(UIButton button)
        {
            button.Layer.BorderWidth = 1.0f;
            button.Layer.BackgroundColor = UIColor.Clear.CGColor;
            button.Layer.BorderColor = UIColor.White.CGColor;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
        }

        private void SubscribeOnEvents()
        {
            Pin1.TouchUpInside += Button_TouchUpInside;
            Pin2.TouchUpInside += Button_TouchUpInside;
            Pin3.TouchUpInside += Button_TouchUpInside;
            Pin4.TouchUpInside += Button_TouchUpInside;
            Button1.TouchUpInside += Button_TouchUpInside;
            Button2.TouchUpInside += Button_TouchUpInside;
            Button3.TouchUpInside += Button_TouchUpInside;
            Button4.TouchUpInside += Button_TouchUpInside;
            Button5.TouchUpInside += Button_TouchUpInside;
            Button6.TouchUpInside += Button_TouchUpInside;
            Button7.TouchUpInside += Button_TouchUpInside;
            Button8.TouchUpInside += Button_TouchUpInside;
            Button9.TouchUpInside += Button_TouchUpInside;
            Button0.TouchUpInside += Button_TouchUpInside;
            ButtonX.TouchUpInside += Button_TouchUpInside;
        }

        private void UnsubscribeFromEvents()
        {
            Pin1.TouchUpInside -= Button_TouchUpInside;
            Pin2.TouchUpInside -= Button_TouchUpInside;
            Pin3.TouchUpInside -= Button_TouchUpInside;
            Pin4.TouchUpInside -= Button_TouchUpInside;
            Button1.TouchUpInside -= Button_TouchUpInside;
            Button2.TouchUpInside -= Button_TouchUpInside;
            Button3.TouchUpInside -= Button_TouchUpInside;
            Button4.TouchUpInside -= Button_TouchUpInside;
            Button5.TouchUpInside -= Button_TouchUpInside;
            Button6.TouchUpInside -= Button_TouchUpInside;
            Button7.TouchUpInside -= Button_TouchUpInside;
            Button8.TouchUpInside -= Button_TouchUpInside;
            Button9.TouchUpInside -= Button_TouchUpInside;
            Button0.TouchUpInside -= Button_TouchUpInside;
            ButtonX.TouchUpInside -= Button_TouchUpInside;
        }

        private void Button_TouchUpInside(object sender, EventArgs e)
        {
            if (sender is UIButton button)
            {
                button.BackgroundColor = UIColor.White;
                Animate(0.5, () => { button.Layer.BackgroundColor = UIColor.White.CGColor; });
                Animate(0.5, () => { button.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            }
        }
    }
}