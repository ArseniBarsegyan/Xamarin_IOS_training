using System;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Helpers;
using UIKit;

namespace CRUDApp.ViewComponents.Login
{
    public class LoginViewController : UIViewController
    {
        private LoginView _loginView;
        private LoginViewPresenter _presenter;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _presenter = new LoginViewPresenter(this);
            NavigationController.SetNavigationBarHidden(true, false);

            _loginView = new LoginView();
            View.AddSubview(_loginView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_loginView.WithSameRight(View),
                _loginView.WithSameLeft(View),
                _loginView.WithSameTop(View),
                _loginView.WithSameBottom(View));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _loginView.LoginButton.TouchUpInside += LoginButtonOnTouchUpInside;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _loginView.StartAnimation();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _loginView.LoginButton.TouchUpInside -= LoginButtonOnTouchUpInside;
        }

        private async void LoginButtonOnTouchUpInside(object sender, EventArgs e)
        {
            var userName = _loginView.LoginTextField.Text;
            var password = _loginView.PasswordTextField.Text;

            //TODO: name and password regex
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ShowAlert(ConstantsHelper.ValidationError, ConstantsHelper.EnterNameAndPassword);
                return;
            }

            if (_loginView.IsRegisterMode)
            {
                var confirmPassword = _loginView.ConfirmPasswordTextField.Text;
                if (password != confirmPassword)
                {
                    ShowAlert(ConstantsHelper.PasswordError, ConstantsHelper.PasswordsDoesNotMatch);
                    return;
                }
                var authResult = await _presenter.RegisterUser(userName, password);
                if (authResult)
                {
                    await _presenter.AuthenticateUser(userName, password);
                }
                else
                {
                    ShowAlert(ConstantsHelper.Error, ConstantsHelper.UserAlreadyExists);
                }
            }
            else
            {
                await _presenter.AuthenticateUser(userName, password);
            }
        }

        public void ShowAlert(string title, string message)
        {
            var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
            PresentViewController(okAlertController, true, null);
        }
    }
}
