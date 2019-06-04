using System;
using System.Collections.Generic;
using CRUDApp.Data.Entities;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class GalleryCollectionViewSource : UICollectionViewSource
    {
        private List<GalleryItemModel> _galleryItems;
        private static NSString GalleryCellId = new NSString(nameof(GalleryViewCell));

        public GalleryCollectionViewSource(List<GalleryItemModel> galleryItems)
        {
            _galleryItems = galleryItems;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (GalleryViewCell)collectionView.DequeueReusableCell(GalleryCellId, indexPath);
            var galleryItem = _galleryItems[indexPath.Row];
            cell.ImageView.Image = new UIImage(NSData.FromUrl(NSUrl.FromString(galleryItem.ImagePath)));
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _galleryItems.Count;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }
    }
}