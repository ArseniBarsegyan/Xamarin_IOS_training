using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit
{
    public class CameraDelegate : UIImagePickerControllerDelegate
    {
        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            picker.DismissModalViewController(true);
            var image = info.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
        }
    }
}