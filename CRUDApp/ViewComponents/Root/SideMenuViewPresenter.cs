using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Login;
using CRUDApp.ViewComponents.Maps;
using CRUDApp.ViewComponents.Notes;
using CRUDApp.ViewComponents.Pin;
using CRUDApp.ViewComponents.Settings;
using CRUDApp.ViewComponents.ToDo;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Root
{
    public class SideMenuViewPresenter
    {
        private SideMenuViewController _controller;

        public SideMenuViewPresenter(SideMenuViewController controller)
        {
            _controller = controller;
        }

        public void NavigateToNotesSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
            var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();
            window.RootViewController = new UINavigationController(initialViewController);
        }

        public void NavigateToToDoSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var toDoController = new ToDoController();
            window.RootViewController = new UINavigationController(toDoController);
        }

        public void NavigateToMapsSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mapsViewController = new MapsViewController();
            window.RootViewController = new UINavigationController(mapsViewController);
        }

        public void NavigateToSettingsSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var settingsViewController = new SettingsViewController();
            window.RootViewController = new UINavigationController(settingsViewController);
        }

        public void Logout()
        {
            NSUserDefaults preferences = NSUserDefaults.StandardUserDefaults;
            var usePin = preferences.BoolForKey(ConstantsHelper.UsePinKey);

            var window = UIApplication.SharedApplication.KeyWindow;

            UIViewController rootViewController = usePin ? new PinViewController()
                : (UIViewController)new LoginViewController();
            window.RootViewController = new UINavigationController(rootViewController);
            Helpers.Settings.AppUser = string.Empty;
        }
    }
}