using System;
using System.Collections.Generic;
using CoreGraphics;
using CRUDApp.Data.Entities;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteGallery
{
    public partial class NoteGalleryViewController : UICollectionViewController
    {
        public NoteGalleryViewController(UICollectionViewLayout layout) : base(layout)
        {
        }

        public NoteGalleryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            View.BackgroundColor = UIColor.White;
            Title = ConstantsHelper.Gallery;

            CollectionView.RegisterClassForCell(typeof(GalleryViewCell), nameof(GalleryViewCell));
            CollectionView.DataSource = new GalleryCollectionViewDataSource(new List<GalleryItemModel>
            {
                new GalleryItemModel
                {
                    Id = 0,
                    ImagePath = "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
                new GalleryItemModel
                {
                    Id = 1,
                    ImagePath = "https://www.w3schools.com/w3css/img_lights.jpg"
                },
                new GalleryItemModel
                {
                    Id = 2,
                    ImagePath = "https://www.w3schools.com/w3css/img_lights.jpg"
                },
                new GalleryItemModel
                {
                    Id = 3,
                    ImagePath = "https://www.w3schools.com/w3css/img_lights.jpg"
                }
            });
            CollectionView.CollectionViewLayout = new GalleryCollectionViewLayout();
            BeginInvokeOnMainThread(() =>
            {
                CollectionView.ReloadData();
            });
        }
    }

    public class GalleryCollectionViewLayout : UICollectionViewFlowLayout
    {
        public GalleryCollectionViewLayout()
        {
            //SectionInset = new UIEdgeInsets(0, 0, 0, 0);
            //MinimumInteritemSpacing = 5;
            //MinimumLineSpacing = 5;
            //ItemSize = new CGSize(150, 150);
        }

        //public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
        //{
        //    return true;
        //}
    }    

    public class GalleryCollectionViewDataSource : UICollectionViewDataSource
    {
        private List<GalleryItemModel> _galleryItems;
        private static NSString GalleryCellId = new NSString(nameof(GalleryViewCell));

        public GalleryCollectionViewDataSource(List<GalleryItemModel> galleryItems)
        {
            _galleryItems = galleryItems;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (GalleryViewCell)collectionView.DequeueReusableCell(GalleryCellId, indexPath);
            var galleryItem = _galleryItems[indexPath.Row];
            BeginInvokeOnMainThread(() =>
            {
                cell.Image = new UIImage(NSData.FromUrl(NSUrl.FromString(galleryItem.ImagePath)));
                //cell.Image = UIImage.FromBundle("login_background.png");
            });
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _galleryItems.Count;
        }
    }

    public class GalleryViewCell : UICollectionViewCell
    {
        private UIImageView _imageView;

        [Export("initWithFrame:")]
        public GalleryViewCell(CGRect frame) : base(frame)
        {
            BackgroundView = new UIView { BackgroundColor = UIColor.White };
            SelectedBackgroundView = new UIView { BackgroundColor = UIColor.LightGray };

            ContentView.BackgroundColor = UIColor.White;
            _imageView = new UIImageView();
            //_imageView.Image = UIImage.LoadFromData(NSData.FromUrl(NSUrl.FromString("https://www.w3schools.com/w3css/img_lights.jpg")));
            //_imageView.Image = UIImage.FromBundle("login_background.png");
            _imageView.Center = ContentView.Center;
            _imageView.Transform = CGAffineTransform.MakeScale(0.9f, 0.9f);

            ContentView.AddSubview(_imageView);
        }

        public UIImage Image
        {
            set => _imageView.Image = value;
        }
    }
}