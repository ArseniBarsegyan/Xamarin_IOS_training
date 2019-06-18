using CoreGraphics;
using UIKit;

namespace CRUDApp.ViewComponents.NoteEdit.NoteGallery
{
    public class ZoomImage : UIScrollView, IUIScrollViewDelegate
    {
        private readonly UIImageView _imageView;
        private UITapGestureRecognizer _gestureRecognizer;

        public ZoomImage(CGRect frame, UIImage image)
        {
            _imageView = new UIImageView(image)
            {
                Frame = frame,
                ContentMode = UIViewContentMode.ScaleAspectFill
            };
            AddSubview(_imageView);
            ViewForZoomingInScrollView = view => _imageView;

            SetupScrollView();
            SetupGestureRecognizer();
        }

        public void SetupScrollView()
        {
            //Delegate = this;
            MinimumZoomScale = 1;
            MaximumZoomScale = 2;
        }

        public void SetupGestureRecognizer()
        {
            _gestureRecognizer = new UITapGestureRecognizer(() =>
            {
                if (ZoomScale == 1)
                {
                    ZoomToRect(ZoomRectForScale((float)MaximumZoomScale, _gestureRecognizer.LocationInView(_gestureRecognizer.View)), true);
                }
                else
                {
                    SetZoomScale(1, true);
                }
            });
            _gestureRecognizer.NumberOfTapsRequired = 2;
            AddGestureRecognizer(_gestureRecognizer);
        }

        public CGRect ZoomRectForScale(float scale, CGPoint center)
        {
            var zoomRect = CGRect.Empty;
            zoomRect.Height = _imageView.Frame.Size.Height / scale;
            zoomRect.Width = _imageView.Frame.Size.Width / scale;

            var newCenter = ConvertPointFromView(center, _imageView);
            zoomRect.X = newCenter.X - (zoomRect.Size.Width / 2);
            zoomRect.Y = newCenter.Y - (zoomRect.Size.Height / 2);
            return zoomRect;
        }
    }
}