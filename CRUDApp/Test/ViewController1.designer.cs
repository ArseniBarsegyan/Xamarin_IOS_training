// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CRUDApp.Test
{
    [Register ("ViewController1")]
    partial class ViewController1
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton HelloWorldButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (HelloWorldButton != null) {
                HelloWorldButton.Dispose ();
                HelloWorldButton = null;
            }
        }
    }
}