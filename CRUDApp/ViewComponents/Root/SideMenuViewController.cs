using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Login;
using CRUDApp.ViewComponents.Maps;
using CRUDApp.ViewComponents.Notes;
using CRUDApp.ViewComponents.Pin;
using CRUDApp.ViewComponents.Settings;
using CRUDApp.ViewComponents.ToDo;
using Foundation;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Root
{
    public class SideMenuViewController : UITableViewController
    {
        private readonly List<MasterPageItem> _items;

        public SideMenuViewController(List<MasterPageItem> items)
        {
            _items = items;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.RegisterClassForCellReuse(typeof(SideMenuViewCell), nameof(SideMenuViewCell));
            TableView.SeparatorColor = UIColor.Clear;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(SideMenuViewCell));
            cell.BackgroundColor = UIColor.White;
            cell.ImageView.Image = UIImage.FromBundle(_items.ElementAt(indexPath.Row).IconSource);
            cell.TextLabel.Text = _items.ElementAt(indexPath.Row).Title;
            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var item = _items.ElementAt(indexPath.Row);
            switch (item.Index)
            {
                case MenuViewIndex.NotesView:
                    NavigateToNotesSection();
                    break;
                case MenuViewIndex.ToDoView:
                    NavigateToToDoSection();
                    break;
                case MenuViewIndex.MapsView:
                    NavigateToMapsSection();
                    break;
                case MenuViewIndex.SettingsView:
                    NavigateToSettingsSection();
                    break;
                case MenuViewIndex.Logout:
                    Logout();
                    break;
            }
        }

        private void NavigateToNotesSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            UIStoryboard helloWorldStoryboard = UIStoryboard.FromName(nameof(NotesController), null);
            var initialViewController = helloWorldStoryboard.InstantiateInitialViewController();

            mainController.ShowDetailViewController(new UINavigationController(initialViewController), this);
            window.RootViewController = mainController;
        }

        private void NavigateToToDoSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            var toDoController = new ToDoController();

            mainController.ShowDetailViewController(new UINavigationController(toDoController), this);
            window.RootViewController = mainController;
        }

        private void NavigateToMapsSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            var mapsViewController = new MapsViewController();

            mainController.ShowDetailViewController(new UINavigationController(mapsViewController), this);
            window.RootViewController = mainController;
        }

        private void NavigateToSettingsSection()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            var settingsViewController = new SettingsViewController();

            mainController.ShowDetailViewController(new UINavigationController(settingsViewController), this);
            window.RootViewController = mainController;
        }

        private void Logout()
        {
            NSUserDefaults preferences = NSUserDefaults.StandardUserDefaults;
            var usePin = preferences.BoolForKey(ConstantsHelper.UsePinKey);

            var window = UIApplication.SharedApplication.KeyWindow;
            var mainController = new SplitViewController();

            UIViewController rootViewController = usePin ? new PinViewController()
                : (UIViewController)new LoginViewController();
            mainController.ShowDetailViewController(new UINavigationController(rootViewController), this);
            window.RootViewController = mainController;
            Helpers.Settings.AppUser = string.Empty;
        }
    }

    public class SideMenuViewCell : UITableViewVibrantCell
    {
        public SideMenuViewCell(IntPtr handle) : base(handle)
        {
        }
    }
}
