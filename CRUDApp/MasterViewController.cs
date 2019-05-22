using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CRUDApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApp
{
    public partial class MasterViewController : UITableViewController
    {
        public Repository<Note> NoteRepository { get; private set; }
        private DataSource _dataSource;
        private UIRefreshControl _refreshControl;

        protected MasterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.GetLocalizedString("Master", "Master");
            SplitViewController.PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;

            NavigationItem.LeftBarButtonItem = EditButtonItem;
            NoteRepository = new Repository<Note>();

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async(sender, args) =>
            {
                await Refresh();
            };
            TableView.RefreshControl = _refreshControl;

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
            TableView.Source = _dataSource = new DataSource(this, NoteRepository.GetAll());
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var controller = (NoteDetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = _dataSource.Notes[indexPath.Row];

                controller.SetDetailItem(item);
                controller.SetRepository(NoteRepository);
                controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
                controller.NavigationItem.LeftItemsSupplementBackButton = true;
            }
            else if (segue.Identifier == "noteEditSeague")
            {
                if (segue.DestinationViewController is NoteEditViewController controller)
                {
                    controller.SetRepository(NoteRepository);
                    controller.SetDataSource(_dataSource);
                    controller.NavigationItem.LeftItemsSupplementBackButton = true;
                }                
            }
            
        }        
    }

    public class DataSource : UITableViewSource
    {
        private static readonly NSString CellIdentifier = new NSString("Cell");
        private readonly List<Note> _notes = new List<Note>();
        private readonly MasterViewController _controller;

        public DataSource(MasterViewController controller, List<Note> notes)
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
            if (note.Description == null)
            {
                cell.TextLabel.Text = "New note";
            }
            else
            {
                cell.TextLabel.Text = note.Description;
            }
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
                _controller.NoteRepository.Delete(noteToDelete);
                _controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            }
            else if (editingStyle == UITableViewCellEditingStyle.Insert)
            {
                // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
            }
        }
    }
}
