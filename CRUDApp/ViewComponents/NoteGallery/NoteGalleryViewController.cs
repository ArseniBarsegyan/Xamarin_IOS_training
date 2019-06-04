using System;
using System.Collections.Generic;
using CRUDApp.Data.Entities;
using CRUDApp.Helpers;
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
            Title = ConstantsHelper.Gallery;
            CollectionView.BackgroundColor = UIColor.White;

            var items = new List<GalleryItemModel>
            {
                new GalleryItemModel
                {
                    Id = 0,
                    ImagePath =
                        "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
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
                    ImagePath =
                        "https://ak5.picdn.net/shutterstock/videos/3775625/thumb/1.jpg?i10c=img.resize(height:160)"
                },
            };

            CollectionView.RegisterClassForCell(typeof(GalleryViewCell), nameof(GalleryViewCell));
            CollectionView.Source = new GalleryCollectionViewSource(items);

            CollectionView.Delegate = new GalleryCollectionViewDelegate(new UIEdgeInsets(5, 5, 5, 5));
            CollectionView.ReloadData();
        }
    }
}