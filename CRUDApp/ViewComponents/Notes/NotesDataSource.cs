using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Notes
{
    public class NotesDataSource : UITableViewSource
    {
        private static readonly NSString CellIdentifier = new NSString(nameof(NoteCell));
        private readonly NoteRepository _repository;
        private readonly NotesController _controller;

        public NotesDataSource(NotesController controller)
        {
            _controller = controller;
            _repository = controller.NoteRepository;
        }

        public IList<Note> Notes => _repository.GetAll().ToList();

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Notes.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
            var note = Notes[indexPath.Row];
            cell.TextLabel.Text = note.Description ?? ConstantsHelper.NewNote;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var model = Notes.ElementAt(indexPath.Row);

            if (model != null)
            {
                var noteEditViewController = new NoteEditViewController();
                noteEditViewController.SetDataSource(this);
                noteEditViewController.SetRepository(_repository);
                noteEditViewController.SetDetailItem(model);
                _controller.NavigationController.PushViewController(noteEditViewController, true);
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                var noteToDelete = Notes.ElementAt(indexPath.Row);
                Notes.RemoveAt(indexPath.Row);
                _controller.NoteRepository.DeleteNote(noteToDelete);
                _controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            }
            else if (editingStyle == UITableViewCellEditingStyle.Insert)
            {
            }
        }
    }
}