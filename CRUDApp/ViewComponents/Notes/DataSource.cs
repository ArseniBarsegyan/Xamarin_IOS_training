using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Data.Entities;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Notes
{
    public class DataSource : UITableViewSource
    {
        private static readonly NSString CellIdentifier = new NSString("Cell");
        private readonly List<Note> _notes;
        private readonly NotesController _controller;

        public DataSource(NotesController controller, List<Note> notes)
        {
            _controller = controller;
            _notes = notes;
        }

        public IList<Note> Notes => _notes;

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _notes.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
            var note = _notes[indexPath.Row];
            cell.TextLabel.Text = note.Description ?? "New note";
            return cell;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                var noteToDelete = _notes.ElementAt(indexPath.Row);
                _notes.RemoveAt(indexPath.Row);
                _controller.NoteRepository.DeleteNote(noteToDelete);
                _controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            }
            else if (editingStyle == UITableViewCellEditingStyle.Insert)
            {
            }
        }
    }
}