using System;
using System.IO;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.NoteEdit;
using CRUDApp.ViewComponents.Root;
using Foundation;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Notes
{
    public partial class NotesController : UITableViewController
    {
        public NoteRepository NoteRepository { get; private set; }
        private NotesDataSource _notesDataSource;
        private UIRefreshControl _refreshControl;
        private SideMenuManager _sideMenuManager;

        public NotesController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            NavigationController.SetNavigationBarHidden(false, false);
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Notes, ConstantsHelper.Notes);

            //NavigationItem.LeftBarButtonItem = EditButtonItem;

            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(ConstantsHelper.Menu, UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();

            NoteRepository = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ConstantsHelper.DatabaseName));
            _notesDataSource = new NotesDataSource(this);

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };

            TableView.RefreshControl = _refreshControl;
            TableView.RegisterClassForCellReuse(typeof(NoteCell), nameof(NoteCell));
            TableView.SeparatorColor = UIColor.Clear;
            //TableView.Source = new NotesDataSource(this, NoteRepository.GetAll().ToList());

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, NavigateToEditNoteController)
            {
                AccessibilityLabel = ConstantsHelper.AddNewNoteButtonAccessibilityLabel
            };
            NavigationItem.RightBarButtonItem = addButton;
        }

        private void SetupSideMenu()
        {
            var sideMenuItems = MenuHelper.GetMenu();
            _sideMenuManager.FadeStatusBar = false;
            _sideMenuManager.LeftNavigationController = new UISideMenuNavigationController
                (_sideMenuManager, new SideMenuViewController(sideMenuItems), true);
            _sideMenuManager.BlurEffectStyle = null;
            _sideMenuManager.AnimationFadeStrength = 0;
            _sideMenuManager.ShadowOpacity = 1f;
            _sideMenuManager.AnimationTransformScaleFactor = 1f;
        }

        private async Task Refresh()
        {
            _refreshControl.BeginRefreshing();
            await Task.Delay(200);
            _refreshControl.EndRefreshing();
            TableView.Source = new NotesDataSource(this);
            TableView.ReloadData();
        }

        private void NavigateToEditNoteController(object sender, EventArgs e)
        {
            var noteEditViewController = new NoteEditViewController();
            noteEditViewController.SetDataSource(_notesDataSource);
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