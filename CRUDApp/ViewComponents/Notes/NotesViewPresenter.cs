using System;
using System.IO;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit;

namespace CRUDApp.ViewComponents.Notes
{
    public class NotesViewPresenter
    {
        private readonly NotesController _controller;
        
        public NotesViewPresenter(NotesController controller)
        {
            _controller = controller;
            Repository = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName));
        }

        public NoteRepository Repository { get; }

        public void NavigateToNoteEditViewController(bool animated, Note detail = null)
        {
            var noteEditViewController = new NoteEditViewController();
            noteEditViewController.SetDataSource(_controller.NotesDataSource);
            noteEditViewController.SetRepository(Repository);
            noteEditViewController.SetDetailItem(detail);
            _controller.NavigationController.PushViewController(noteEditViewController, animated);
        }
    }
}
