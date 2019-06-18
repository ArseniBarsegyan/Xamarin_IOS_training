using System;
using System.Collections.Generic;
using CoreGraphics;
using CRUDApp.Data.Entities;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class GalleryCollectionViewDelegate : UICollectionViewDelegateFlowLayout
    {
        private readonly UIEdgeInsets _insets;
        private List<GalleryItemModel> _galleryItems;
        private UIViewController _controller;

        public GalleryCollectionViewDelegate(List<GalleryItemModel> galleryItems, UIViewController controller, UIEdgeInsets insets)
        {
            _galleryItems = galleryItems;
            _insets = insets;
            _controller = controller;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            collectionView.DeselectItem(indexPath, true);
            var galleryItem = _galleryItems[indexPath.Row];
            var imageViewController = new ImageViewController(galleryItem);
            _controller.NavigationController.PushViewController(imageViewController, true);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout,
            NSIndexPath indexPath)
        {
            return new CGSize(125, 125);
        }

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return _insets;
        }

        public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return _insets.Left;
        }
    }
}