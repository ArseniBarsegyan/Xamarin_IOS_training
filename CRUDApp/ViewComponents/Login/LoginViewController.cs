using System;
using System.IO;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Authentication;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Notes;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Login
{
    public class LoginViewController : UIViewController
    {
        private LoginView _loginView;
        private AuthenticationManager _authenticationManager;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.SetNavigationBarHidden(true, false);

            _loginView = new LoginView();
            View.AddSubview(_loginView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_loginView.WithSameRight(View),
                _loginView.WithSameLeft(View),
                _loginView.WithSameTop(View),
                _loginView.WithSameBottom(View));
            
            _authenticationManager = new AuthenticationManager(
                    new UserRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName)));
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
                var okAlertController = UIAlertController.Create(ConstantsHelper.ValidationError, ConstantsHelper.EnterNameAndPassword,
                    UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
                return;
            }

            if (_loginView.IsRegisterMode)
            {
                var confirmPassword = _loginView.ConfirmPasswordTextField.Text;
                if (password != confirmPassword)
                {
                    var okAlertController = UIAlertController.Create(ConstantsHelper.PasswordError, ConstantsHelper.PasswordsDoesNotMatch,
                        UIAlertControllerStyle.Alert);
                    okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
                    PresentViewController(okAlertController, true, null);
                    return;
                }
                var authResult = await _authenticationManager.Register(userName, password);
                if (authResult)
                {
                    await AuthenticateUser(userName, password);
                }
                else
                {
                    var okAlertController = UIAlertController.Create(ConstantsHelper.Error, ConstantsHelper.UserAlreadyExists,
                        UIAlertControllerStyle.Alert);
                    okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
                    PresentViewController(okAlertController, true, null);
                }
            }
            else
            {
                await AuthenticateUser(userName, password);
            }
        }

        private async Task AuthenticateUser(string username, string password)
        {
            bool result = await _authenticationManager.Authenticate(username, password);
            if (result)
            {
                NSUserDefaults.StandardUserDefaults.SetString(username, ConstantsHelper.UserName);
                //NavigationController.SetViewControllers(new UIViewController[] { new UISideMenuController() }, true);
                UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
                var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();
                NavigationController.PushViewController(initialViewController, true);
            }
            else
            {
                var okAlertController = UIAlertController.Create(ConstantsHelper.Error, ConstantsHelper.NoUserOrPasswordIncorret,
                    UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
            }
        }
    }
}