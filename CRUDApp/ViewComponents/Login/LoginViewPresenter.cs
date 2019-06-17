using System;
using System.IO;
using System.Threading.Tasks;
using CRUDApp.Authentication;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Notes;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Login
{
    public class LoginViewPresenter
    {
        private readonly LoginViewController _controller;
        private readonly AuthenticationManager _authenticationManager;

        public LoginViewPresenter(LoginViewController controller)
        {
            _controller = controller;
            _authenticationManager = new AuthenticationManager(
                new UserRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName)));
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            return await _authenticationManager.Register(username, password);
        }
        
        public async Task AuthenticateUser(string username, string password)
        {
            bool result = await _authenticationManager.Authenticate(username, password);
            if (result)
            {
                NSUserDefaults.StandardUserDefaults.SetString(username, ConstantsHelper.UserName);
                //NavigationController.SetViewControllers(new UIViewController[] { new UISideMenuController() }, true);
                UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
                var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();
                _controller.NavigationController.PushViewController(initialViewController, true);
            }
            else
            {
                _controller.ShowAlert(ConstantsHelper.Error, ConstantsHelper.NoUserOrPasswordIncorret);
            }
        }
    }
}
