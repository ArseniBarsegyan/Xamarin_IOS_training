// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CRUDApp
{
    [Register ("SettingsView")]
    partial class SettingsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton saveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch usePinSwitchCell { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (saveButton != null) {
                saveButton.Dispose ();
                saveButton = null;
            }

            if (usePinSwitchCell != null) {
                usePinSwitchCell.Dispose ();
                usePinSwitchCell = null;
            }
        }
    }
}