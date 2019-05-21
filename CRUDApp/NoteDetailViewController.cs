using System;
using CRUDApp.Data;
using Foundation;
using UIKit;

namespace CRUDApp
{
    public partial class NoteDetailViewController : UIViewController
    {
        public Note NoteEditModel { get; set; }
        private Repository<Note> _repository;

        public NoteDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public void SetRepository(Repository<Note> repository)
        {
            _repository = repository;
        }

        public void SetDetailItem(Note note)
        {
            if (NoteEditModel != note)
            {
                NoteEditModel = note;

                // Update the view
                ConfigureView();
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showNotes")
            {
                NoteEditModel.Description = NoteDescriptionEditor.Text;
                _repository.Save(NoteEditModel);
            }
        }

        void ConfigureView()
        {
            // Update the user interface for the detail item
            if (IsViewLoaded && NoteEditModel != null)
            {
                NoteDescriptionEditor.Text = NoteEditModel.Description;
                // detailDescriptionLabel.Text = NoteEditModel.Description;
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


