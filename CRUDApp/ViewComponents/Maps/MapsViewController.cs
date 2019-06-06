using System;
using Cirrious.FluentLayouts.Touch;
using CoreLocation;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Root;
using Foundation;
using MapKit;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Maps
{
    public class MapsViewController : UIViewController
    {
        private SideMenuManager _sideMenuManager;

        public MapsViewController(IntPtr handle) : base(handle)
        {
        }

        public MapsViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Maps, ConstantsHelper.Maps);

            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(ConstantsHelper.Menu, UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();

            var mapsView = new MKMapView();
            mapsView.MapType = MKMapType.Hybrid;
            mapsView.ZoomEnabled = true;
            mapsView.ScrollEnabled = true;
            mapsView.TranslatesAutoresizingMaskIntoConstraints = false;

            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();

            mapsView.ShowsUserLocation = true;

            View.AddSubview(mapsView);
            View.AddConstraints(mapsView.WithSameTop(View),
                mapsView.WithSameBottom(View),
                mapsView.WithSameLeft(View),
                mapsView.WithSameRight(View));
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