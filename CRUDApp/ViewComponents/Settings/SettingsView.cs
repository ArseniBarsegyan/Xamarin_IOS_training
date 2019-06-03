using System;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Settings
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

            UsePinSwitchCell = rootView?.usePinSwitchCell;
            PinEntry = rootView?.pinEntry;
            SaveButton = rootView?.saveButton;
            SaveButton.Hidden = true;
            SaveButton.TouchUpInside += SaveButton_TouchUpInside;

            SubscribeOnEvents();
            AddSubview(rootView);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints(
                rootView.WithSameTop(this),
                rootView.WithSameBottom(this),
                rootView.WithSameLeft(this),
                rootView.WithSameRight(this));
        }

        public UISwitch UsePinSwitchCell { get; private set; }
        public UITextField PinEntry { get; private set; }
        public UIButton SaveButton { get; private set; }

        private void SubscribeOnEvents()
        {
            UsePinSwitchCell.ValueChanged += UsePinSwitchCell_ValueChanged;
        }

        private void UnsubscribeFromEvents()
        {
            UsePinSwitchCell.ValueChanged -= UsePinSwitchCell_ValueChanged;
        }

        private void UsePinSwitchCell_ValueChanged(object sender, EventArgs e)
        {
            SaveButton.Hidden = false;
        }

        private void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            NSUserDefaults.StandardUserDefaults.SetBool(UsePinSwitchCell.On, ConstantsHelper.UsePinKey);
            NSUserDefaults.StandardUserDefaults.SetString(PinEntry.Text, ConstantsHelper.UserPin);
            SaveButton.Hidden = true;
        }
    }
}