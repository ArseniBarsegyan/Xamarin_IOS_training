using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using Foundation;
using UIKit;

namespace CRUDApp.Controllers
{
    public class NotesViewController : UITableViewController
    {
        public NoteRepository NoteRepository { get; private set; }
        private DataSource _dataSource;
        private UIRefreshControl _refreshControl;

        public override void ViewDidLoad()
        {
            NavigationController.SetNavigationBarHidden(false, false);
            Title = NSBundle.MainBundle.GetLocalizedString("Master", "Master");
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.GetLocalizedString("Master", "Master");

            NavigationItem.LeftBarButtonItem = EditButtonItem;
            NoteRepository = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "rm.db3"));

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };
            TableView.RefreshControl = _refreshControl;
            TableView.RegisterClassForCellReuse(typeof(NoteCell), "Cell");
            TableView.Source = new DataSource(this, NoteRepository.GetAll().ToList());

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, NavigateToEditNoteController)
            {
                AccessibilityLabel = "addNewNoteButton"
            };
            NavigationItem.RightBarButtonItem = addButton;
        }

        private void NavigateToEditNoteController(object sender, EventArgs e)
        {
            var noteEditViewController = new NoteEditViewController();
            noteEditViewController.SetDataSource(_dataSource);
            noteEditViewController.SetRepository(NoteRepository);
            NavigationController.PushViewController(noteEditViewController, true);
        }

        private async Task Refresh()
        {
            _refreshControl.BeginRefreshing();
            await Task.Delay(200);
            _refreshControl.EndRefreshing();
            TableView.ReloadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.Source = _dataSource = new DataSource(this, NoteRepository.GetAll().ToList());
        }
    }

    public class NoteCell : UITableViewCell
    {
        public NoteCell(IntPtr handle) : base(handle)
        {
        }
    }

    public class DataSource : UITableViewSource
    {
        private static readonly NSString CellIdentifier = new NSString("Cell");
        private readonly List<Note> _notes;
        private readonly NotesViewController _controller;

        public DataSource(NotesViewController controller, List<Note> notes)
        {
            _controller = controller;
            _notes = notes;
        }

        public IList<Note> Notes => _notes;

        // Customize the number of sections in the table view.
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _notes.Count;
        }

        // Customize the appearance of table view cells.
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
            var note = _notes[indexPath.Row];
            cell.TextLabel.Text = note.Description ?? "New note";
            return cell;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            // Return false if you do not want the specified item to be editable.
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
                // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
            }
        }
    }
}