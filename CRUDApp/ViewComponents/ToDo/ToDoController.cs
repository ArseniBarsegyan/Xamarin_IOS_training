using System;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Root;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.ToDo
{
    public class ToDoController : UITabBarController
    {
        private UIViewController _activeTab, _doneTab;
        private SideMenuManager _sideMenuManager;

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

            _activeTab = new UIViewController
            {
                Title = "Active",
                View =
                {
                    BackgroundColor = UIColor.Blue
                }
            };
            _doneTab = new UIViewController
            {
                Title = "Done",
                View =
                {
                    BackgroundColor = UIColor.Green
                }
            };

            ViewControllers = new[] { _activeTab, _doneTab };
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
    }
}