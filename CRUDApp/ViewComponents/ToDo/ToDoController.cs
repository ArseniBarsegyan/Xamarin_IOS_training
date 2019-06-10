using System;
using System.IO;
using System.Linq;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Root;
using CRUDApp.ViewComponents.ToDo.Active;
using CRUDApp.ViewComponents.ToDo.Done;
using CRUDApp.ViewComponents.ToDo.ToDoEdit;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.ToDo
{
    public class ToDoController : UITabBarController
    {
        private UIViewController _activeTab, _doneTab;
        private SideMenuManager _sideMenuManager;
        private ToDoRepository _repository;

        public ToDoController()
        {
        }

        public ToDoController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(ConstantsHelper.Menu, UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, NavigateToEditToDoController)
            {
                AccessibilityLabel = ConstantsHelper.AddNewToDoButtonAccessibilityLabel
            };
            NavigationItem.RightBarButtonItem = addButton;

            _repository = new ToDoRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName));

            _activeTab = new ToDoActiveViewController(_repository);
            _doneTab = new ToDoDoneViewController(_repository);

            ViewControllers = new[] { _activeTab, _doneTab };
            SelectedIndex = 1;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var activeCount = _repository.GetAll().Count(x => x.Status == "Active");
            var doneCount = _repository.GetAll().Count(x => x.Status == "Done");

            if (activeCount > 0)
            {
                _activeTab.TabBarItem.BadgeValue = activeCount.ToString();
            }

            if (doneCount > 0)
            {
                _doneTab.TabBarItem.BadgeValue = doneCount.ToString();
            }
        }

        private void SetupSideMenu()
        {
            var sideMenuItems = MenuHelper.GetMenu();
            _sideMenuManager.FadeStatusBar = false;
            _sideMenuManager.LeftNavigationController = new UISideMenuNavigationController
                (_sideMenuManager, new SideMenuViewController(sideMenuItems), true);
            _sideMenuManager.BlurEffectStyle = null;
            _sideMenuManager.AnimationFadeStrength = 0;
            _sideMenuManager.ShadowOpacity = 1f;
            _sideMenuManager.AnimationTransformScaleFactor = 1f;
        }

        private void NavigateToEditToDoController(object sender, EventArgs e)
        {
            var toDoEditViewController = new ToDoEditViewController(0);
            toDoEditViewController.SetRepository(_repository);
            NavigationController.PushViewController(toDoEditViewController, true);
        }
    }
}