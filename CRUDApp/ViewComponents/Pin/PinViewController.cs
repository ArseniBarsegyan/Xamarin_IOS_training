using System;
using System.Text;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Notes;
using CRUDApp.ViewComponents.Root;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Pin
{
    public class PinViewController : UIViewController
    {
        private PinView _pinView;
        private static int _currentCount;
        private readonly StringBuilder _pinBuilder;
        private int _pin;

        public PinViewController()
        {
            _pinBuilder = new StringBuilder();
        }

        public override void ViewDidLoad()
        {
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

        private async void Button_OnTouchUpInside(object sender, EventArgs e)
        {
            if (sender is UIButton button)
            {
                var text = button.TitleLabel.Text;
                _currentCount++;

                _pinBuilder.Append(text);

                switch (_currentCount)
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
                        int.TryParse(_pinBuilder.ToString(), out _pin);
                        await Login();
                        ResetImagesAndCount();
                        break;
                }

                //if (_currentCount == 4)
                //{
                //    int.TryParse(_pinBuilder.ToString(), out _pin);
                //    await Task.Delay(25);
                //    ResetImagesAndCount();
                //    await Login();
                //}
            }
        }

        private void DeletePinNumber(object sender, EventArgs e)
        {
            if (_pinBuilder.Length > 0)
            {
                switch (_pinBuilder.Length)
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
                _pinBuilder.Length--;
            }

            if (_currentCount > 0)
            {
                _currentCount--;
            }
        }

        private async Task Login()
        {
            NSUserDefaults preferences = NSUserDefaults.StandardUserDefaults;
            var userPin = preferences.StringForKey(ConstantsHelper.UserPin);

            if (_pinBuilder.ToString() == userPin)
            {
                NavigateToInitialSection();
            }
        }

        private void ResetImagesAndCount()
        {
            UIView.Animate(0.3, () => { _pinView.Pin1.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin2.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin3.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            UIView.Animate(0.3, () => { _pinView.Pin4.Layer.BackgroundColor = UIColor.Clear.CGColor; });
            _pinBuilder.Length = 0;
            _currentCount = 0;
        }

        private void NavigateToInitialSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
            var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();

            mainController.ShowDetailViewController(new UINavigationController(initialViewController), this);
            window.RootViewController = mainController;
        }
    }
}

