using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.ViewComponents.NoteEdit;
using Foundation;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Notes
{
    public partial class NotesController : UITableViewController
    {
        public NoteRepository NoteRepository { get; private set; }
        private DataSource _dataSource;
        private UIRefreshControl _refreshControl;
        private SideMenuManager _sideMenuManager;

        public NotesController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            NavigationController.SetNavigationBarHidden(false, false);
            Title = NSBundle.MainBundle.GetLocalizedString("Notes", "Notes");

            //NavigationItem.LeftBarButtonItem = EditButtonItem;

            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem("Menu", UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();


            NoteRepository = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "rm.db3"));
            _dataSource = new DataSource(this, NoteRepository.GetAll().ToList());

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };

            TableView.RefreshControl = _refreshControl;
            TableView.RegisterClassForCellReuse(typeof(NoteCell), "Cell");
            TableView.SeparatorColor = UIColor.Clear;
            //TableView.Source = new DataSource(this, NoteRepository.GetAll().ToList());

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, NavigateToEditNoteController)
            {
                AccessibilityLabel = "addNewNoteButton"
            };
            NavigationItem.RightBarButtonItem = addButton;
        }

        void SetupSideMenu()
        {
            var items = new List<string>
            {
                "Notes",
                "To-Do",
                "Achievements",
                "Settings",
                "Logout"
            };
            _sideMenuManager.FadeStatusBar = false;
            _sideMenuManager.LeftNavigationController = new UISideMenuNavigationController
                (_sideMenuManager, new SideMenuViewController(items), true);
            _sideMenuManager.BlurEffectStyle = null;
            _sideMenuManager.AnimationFadeStrength = 0;
            _sideMenuManager.ShadowOpacity = 1f;
            _sideMenuManager.AnimationTransformScaleFactor = 1f;
            //_sideMenuManager.AnimationBackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("stars.png"));
        }

        private async Task Refresh()
        {
            _refreshControl.BeginRefreshing();
            await Task.Delay(200);
            _refreshControl.EndRefreshing();
            TableView.Source = new DataSource(this, NoteRepository.GetAll().ToList());
            TableView.ReloadData();
        }

        private void NavigateToEditNoteController(object sender, EventArgs e)
        {
            var noteEditViewController = new NoteEditViewController();
            noteEditViewController.SetDataSource(_dataSource);
            noteEditViewController.SetRepository(NoteRepository);
            NavigationController.PushViewController(noteEditViewController, true);
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}