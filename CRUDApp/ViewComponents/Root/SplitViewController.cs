using UIKit;

namespace CRUDApp.ViewComponents.Root
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