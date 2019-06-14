using System;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace CRUDApp.ViewComponents.Pin
{
    public class PinViewController : UIViewController
    {
        private PinView _pinView;
        private PinViewPresenter _presenter;

        public PinViewController()
        {
        }

        public override void ViewDidLoad()
        {
            _presenter = new PinViewPresenter(this);
            base.ViewDidLoad();
            NavigationController.SetNavigationBarHidden(true, false);
            _pinView = new PinView();

            View.AddSubview(_pinView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_pinView.WithSameRight(View),
                _pinView.WithSameLeft(View),
                _pinView.WithSameTop(View),
                _pinView.WithSameBottom(View));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _pinView.Button1.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button2.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button3.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button4.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button5.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button6.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button7.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button8.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button9.TouchUpInside += Button_OnTouchUpInside;
            _pinView.Button0.TouchUpInside += Button_OnTouchUpInside;
            _pinView.ButtonX.TouchUpInside += DeletePinNumber;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _pinView.Button1.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button2.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button3.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button4.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button5.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button6.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button7.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button8.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button9.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.Button0.TouchUpInside -= Button_OnTouchUpInside;
            _pinView.ButtonX.TouchUpInside -= DeletePinNumber;
        }

        private void Button_OnTouchUpInside(object sender, EventArgs e)
        {
            if (sender is UIButton button)
            {
                _presenter.CheckPin(button.TitleLabel.Text);

                switch (_presenter.CurrentCount)
                {
                    case 1:
                        UIView.Animate(0.3, () => { _pinView.Pin1.Layer.BackgroundColor = UIColor.White.CGColor; });
                        break;
                    case 2:
                        UIView.Animate(0.3, () => { _pinView.Pin2.Layer.BackgroundColor = UIColor.White.CGColor; });
                        break;
                    case 3:
                        UIView.Animate(0.3, () => { _pinView.Pin3.Layer.BackgroundColor = UIColor.White.CGColor; });
                        break;
                    case 4:
                        UIView.Animate(0.3, () => { _pinView.Pin4.Layer.BackgroundColor = UIColor.White.CGColor; });
                        _presenter.Login();
                        ResetImagesAndCount();
                        break;
                }
            }
        }

        private void DeletePinNumber(object sender, EventArgs e)
        {
            switch (_presenter.PinLength)
            {
                // Since we reset counter we will never get length 4
                case 3:
                    UIView.Animate(0.3, () => { _pinView.Pin3.Layer.BackgroundColor = UIColor.Clear.CGColor; });
                    break;
                case 2:
                    UIView.Animate(0.3, () => { _pinView.Pin2.Layer.BackgroundColor = UIColor.Clear.CGColor; });
                    break;
                case 1:
                    UIView.Animate(0.3, () => { _pinView.Pin1.Layer.BackgroundColor = UIColor.Clear.CGColor; });
                    break;
            }
            _presenter.DecreasePinLength();
            _presenter.DeleteLastPinNumber();
        }

        private void ResetImagesAndCount()
        {
            UIView.Animate(0.3, () => { _pinView.Pin1.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin2.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin3.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin4.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            _presenter.ResetCount();
        }
    }
}
