using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class GalleryCollectionViewLayout : UICollectionViewFlowLayout
    {
        public GalleryCollectionViewLayout()
        {
            ScrollDirection = UICollectionViewScrollDirection.Vertical;
        }
    }
}