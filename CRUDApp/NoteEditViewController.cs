using System;
using CRUDApp.Data;
using UIKit;

namespace CRUDApp
{
    public partial class NoteEditViewController : UIViewController
    {
        private Repository<Note> _repository;
        private DataSource _dataSource;

        private UITextView _noteDescriptionTextView;

        public NoteEditViewController()
        {
        }

        public NoteEditViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, AddNewItem)
            {
                AccessibilityLabel = "confirmButton"
            };
            Title = "New note";
            NavigationItem.RightBarButtonItem = addButton;
            _noteDescriptionTextView = new UITextView();
            // _noteDescriptionTextView.Frame = new CGRect(10, 10, 20, 10);
            _noteDescriptionTextView.Text = "New note";

            _noteDescriptionTextView.TranslatesAutoresizingMaskIntoConstraints = false;
            View.AddSubview(_noteDescriptionTextView);
            
            _noteDescriptionTextView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 5f).Active = true;
            _noteDescriptionTextView.TopAnchor.ConstraintEqualTo(View.TopAnchor, 50f).Active = true;
            _noteDescriptionTextView.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;
            _noteDescriptionTextView.HeightAnchor.ConstraintEqualTo(300).Active = true;
        }        

        private void AddNewItem(object sender, EventArgs args)
        {
            var note = new Note { Description = _noteDescriptionTextView.Text, CreateDate = DateTime.Now };            
            _repository.Save(note);
            _dataSource.Notes.Add(note);
            NavigationController.PopViewController(true);
        }

        public void SetRepository(Repository<Note> repository)
        {
            _repository = repository;
        }

        public void SetDataSource(DataSource dataSource)
        {
            _dataSource = dataSource;
        }
    }
}