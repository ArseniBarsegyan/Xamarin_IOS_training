using System;
using System.IO;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.Notes
{
    public class NotesViewController : UITableViewController
    {
        public NoteRepository NoteRepository { get; private set; }
        private DataSource _dataSource;
        private UIRefreshControl _refreshControl;

        public override void ViewDidLoad()
        {
            NavigationController.SetNavigationBarHidden(false, false);
            base.ViewDidLoad();

            Title = NSBundle.MainBundle.GetLocalizedString("Master", "Master");

            NavigationItem.LeftBarButtonItem = EditButtonItem;
            NoteRepository = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName));

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };
            TableView.RefreshControl = _refreshControl;
            TableView.RegisterClassForCellReuse(typeof(NoteCell), ConstantsHelper.NoteCellReuseIdentifier);
            //TableView.Source = new DataSource(this, NoteRepository.GetAll().ToList());

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, NavigateToEditNoteController)
            {
                AccessibilityLabel = ConstantsHelper.AddNewNoteButtonAccessibilityLabel
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
            //TableView.Source = _dataSource = new DataSource(this, NoteRepository.GetAll().ToList());
        }
    }
}