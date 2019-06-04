using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class GalleryViewCell : UICollectionViewCell
    {
        [Export("initWithFrame:")]
        public GalleryViewCell(CGRect frame) : base(frame)
        {
            ImageView = new UIImageView(UIImage.FromBundle("login_background.png"));
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            ImageView.ClipsToBounds = true;
            //ImageView.Center = ContentView.Center;
            ImageView.TranslatesAutoresizingMaskIntoConstraints = false;

            ContentView.AddSubview(ImageView);
            ContentView.AddConstraints(ImageView.WithSameWidth(ContentView),
                ImageView.WithSameHeight(ContentView),
                ImageView.WithSameTop(ContentView));
        }

        public UIImageView ImageView { get; set; }
    }
}