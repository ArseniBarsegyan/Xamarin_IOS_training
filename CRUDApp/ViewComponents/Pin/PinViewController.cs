using System;
using System.Text;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Controllers;
using CRUDApp.ViewComponents.Notes;
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

                if (_currentCount == 4)
                {
                    int.TryParse(_pinBuilder.ToString(), out _pin);
                    await Task.Delay(25);
                    ResetImagesAndCount();
                    await Login();
                }
            }
        }

        private void DeletePinNumber(object sender, EventArgs e)
        {
            if (_pinBuilder.Length > 0)
            {
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
            var userPin = preferences.StringForKey("UserPin");

            if (_pinBuilder.ToString() == userPin)
            {
                NavigateToInitialSection();
            }
        }

        private void ResetImagesAndCount()
        {
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

