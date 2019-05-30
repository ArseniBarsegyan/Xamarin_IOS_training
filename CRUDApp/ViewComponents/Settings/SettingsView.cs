using Cirrious.FluentLayouts.Touch;
using Foundation;
using System;
using UIKit;

namespace CRUDApp
{
    public partial class SettingsView : UIView
    {
        public SettingsView (IntPtr handle) : base (handle)
        {
        }

        public SettingsView()
        {
            InitView();
        }

        private void InitView()
        {
            var arr = NSBundle.MainBundle.LoadNib(nameof(SettingsView), this, null);
            var rootView = ObjCRuntime.Runtime.GetNSObject(arr.ValueAt(0)) as SettingsView;
            AddSubview(rootView);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints(
                rootView.WithSameTop(this),
                rootView.WithSameBottom(this),
                rootView.WithSameLeft(this),
                rootView.WithSameRight(this));
        }
    }
}