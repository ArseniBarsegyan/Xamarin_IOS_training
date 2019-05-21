using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CRUDApp.Data;
using System.Linq;

namespace CRUDApp
{
    public partial class MasterViewController : UITableViewController
    {
        public Repository<Note> NoteRepository { get; private set; }
        private DataSource dataSource;

        protected MasterViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.GetLocalizedString("Master", "Master");
            SplitViewController.PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;

            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.LeftBarButtonItem = EditButtonItem;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem)
            {
                AccessibilityLabel = "addButton"
            };
            NavigationItem.RightBarButtonItem = addButton;

            NoteRepository = new Repository<Note>();
            TableView.Source = dataSource = new DataSource(this, NoteRepository.GetAll());
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void AddNewItem(object sender, EventArgs args)
        {
            var note = new Note();
            NoteRepository.Save(note);
            dataSource.Notes.Add(note);

            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
            {
                TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var controller = (NoteDetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Notes[indexPath.Row];

                controller.SetDetailItem(item);
                controller.SetRepository(NoteRepository);
                controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
                controller.NavigationItem.LeftItemsSupplementBackButton = true;
            }
        }

        private class DataSource : UITableViewSource
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
                    // Delete the row from the data source.
                    var noteToDelete = _notes.ElementAt(indexPath.Row);
                    _notes.RemoveAt(indexPath.Row);
                    _controller.NoteRepository.Delete(noteToDelete);
                    // _controller.NoteRepository.Delete(indexPath.Row);
                    _controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
        }
    }
}
