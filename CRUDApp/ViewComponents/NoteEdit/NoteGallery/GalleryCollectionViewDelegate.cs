using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class GalleryCollectionViewDelegate : UICollectionViewDelegateFlowLayout
    {
        private const float ItemsPerRow = 3;
        private readonly UIEdgeInsets _insets;

        public GalleryCollectionViewDelegate(UIEdgeInsets insets)
        {
            _insets = insets;
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