using System;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using Foundation;
using UIKit;

namespace CRUDApp.Controllers
{
    public partial class NoteDetailViewController : UIViewController
    {        
        private NoteRepository _repository;
        private Note NoteEditModel;

        public NoteDetailViewController(IntPtr handle) : base(handle)
        {
        }        

        public void SetRepository(NoteRepository repository)
        {
            _repository = repository;
        }

        public void SetDetailItem(Note note)
        {
            if (NoteEditModel != note)
            {
                NoteEditModel = note;
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
            if (IsViewLoaded && NoteEditModel != null)
            {
                NoteDescriptionEditor.Text = NoteEditModel.Description;
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ConfigureView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}


