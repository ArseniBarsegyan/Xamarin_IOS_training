using System;
using System.IO;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace CRUDApp.Controllers
{
    public class LoginViewController : UIViewController
    {
        private LoginView _loginView;
        private AuthenticationManager _authenticationManager;

        public override void ViewDidLoad()
        {
            NavigationController.SetNavigationBarHidden(true, false);
            base.ViewDidLoad();

            var view = Runtime.GetNSObject(NSBundle.MainBundle.LoadNib(nameof(LoginView), this, null).ValueAt(0));
            _loginView = view as LoginView;
            View.AddSubview(_loginView);
            
            _authenticationManager = new AuthenticationManager(
                    new UserRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "rm.db3")));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _loginView.LoginButton.TouchUpInside += LoginButtonOnTouchUpInside;
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
                var okAlertController = UIAlertController.Create("Validation error", "Please enter name and password",
                    UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
                return;
            }

            if (_loginView.IsRegisterMode)
            {
                var confirmPassword = _loginView.ConfirmPasswordTextField.Text;
                if (password != confirmPassword)
                {
                    var okAlertController = UIAlertController.Create("Passwords error", "Passwords doesn't match",
                        UIAlertControllerStyle.Alert);
                    okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
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
                    var okAlertController = UIAlertController.Create("Error", "User already exists",
                        UIAlertControllerStyle.Alert);
                    okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
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
                Settings.AppUser = username;
                NavigationController.SetViewControllers(new UIViewController[] { new NotesViewController() }, true);
            }
            else
            {
                var okAlertController = UIAlertController.Create("Error", "No such user or password incorrect",
                    UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
            }
        }
    }
}