// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;
using Foundation;

namespace CRUDApp.Controllers
{
    [Register ("DetailViewController")]
    partial class NoteDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ConfirmButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView NoteDescriptionEditor { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ConfirmButton != null) {
                ConfirmButton.Dispose ();
                ConfirmButton = null;
            }

            if (NoteDescriptionEditor != null) {
                NoteDescriptionEditor.Dispose ();
                NoteDescriptionEditor = null;
            }
        }
    }
}