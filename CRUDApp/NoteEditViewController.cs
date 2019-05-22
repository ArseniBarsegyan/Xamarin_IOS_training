using System;
using CoreGraphics;
using CRUDApp.Data;
using Foundation;
using UIKit;

namespace CRUDApp
{
    public partial class NoteEditViewController : UIViewController
    {
        private UITextView _noteDescriptionTextView;

        public NoteEditViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, AddNewItem)
            {
                AccessibilityLabel = "addButton"
            };
            NavigationItem.RightBarButtonItem = addButton;
            _noteDescriptionTextView = new UITextView();
            // _noteDescriptionTextView.Frame = new CGRect(10, 10, 20, 10);
            _noteDescriptionTextView.Text = "Lorem ipsum dolor sit er elit llit unt mooen legum odioque civiuda.";

            var margins = View.LayoutMarginsGuide;

            _noteDescriptionTextView.Frame = new CGRect(10, 10, 300, 300);
            View.AddSubview(_noteDescriptionTextView);
            
            _noteDescriptionTextView.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor, 50f).Active = true;
            _noteDescriptionTextView.TopAnchor.ConstraintEqualTo(margins.BottomAnchor, 100f).Active = true;
        }
        
        private void AddNewItem(object sender, EventArgs args)
        {
            var note = new Note();
            //dataSource.Notes.Add(note);

            //using (var indexPath = NSIndexPath.FromRowSection(0, 0))
            //{
            //    TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
            //}
        }
    }
}