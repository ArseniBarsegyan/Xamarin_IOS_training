using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using CRUDApp.Data.Entities;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class ImageViewController : UIViewController
    {
        private readonly GalleryItemModel _model;

        public ImageViewController(GalleryItemModel model)
        {
            _model = model;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Gallery, ConstantsHelper.Gallery);

            var zoomImage = new ZoomImage(
                new CGRect(0,0, 400, 400),
                new UIImage(NSData.FromUrl(NSUrl.FromString(_model.ImagePath))));
            zoomImage.TranslatesAutoresizingMaskIntoConstraints = false;
            View.AddSubview(zoomImage);
            View.AddConstraints(zoomImage.WithSameTop(View),
                zoomImage.WithSameBottom(View),
                zoomImage.WithSameLeft(View),
                zoomImage.WithSameRight(View));
        }
    }
}