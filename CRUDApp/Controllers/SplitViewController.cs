using UIKit;

namespace CRUDApp.Controllers
{
    public class SplitViewController : UISplitViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;
        }
    }
}