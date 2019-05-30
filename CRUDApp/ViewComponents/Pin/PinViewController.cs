using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace CRUDApp.ViewComponents.Pin
{
    public class PinViewController : UIViewController
    {
        private PinView _pinView;

        public PinViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.SetNavigationBarHidden(true, false);
            _pinView = new PinView();

            View.AddSubview(_pinView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_pinView.WithSameRight(View),
                _pinView.WithSameLeft(View),
                _pinView.WithSameTop(View),
                _pinView.WithSameBottom(View));
        }
    }
}

