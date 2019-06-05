﻿using System;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Root;
using Foundation;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Settings
{
    public class SettingsViewController : UIViewController
    {
        private SettingsView _settingsView;
        private SideMenuManager _sideMenuManager;

        public SettingsViewController(IntPtr handler) : base(handler)
        {
        }

        public SettingsViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Settings, ConstantsHelper.Settings);
            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(ConstantsHelper.Menu, UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();

            _settingsView = new SettingsView();
            View.AddSubview(_settingsView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_settingsView.WithSameRight(View),
                _settingsView.WithSameLeft(View),
                _settingsView.WithSameTop(View),
                _settingsView.WithSameBottom(View));
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
