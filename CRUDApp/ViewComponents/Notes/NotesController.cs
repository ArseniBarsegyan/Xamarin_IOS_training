using System;
using System.Threading.Tasks;
using CRUDApp.Helpers;
using CRUDApp.ViewComponents.Root;
using Foundation;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Notes
{
    public partial class NotesController : UITableViewController
    {
        public NotesDataSource NotesDataSource { get; private set; }
        private UIRefreshControl _refreshControl;
        private SideMenuManager _sideMenuManager;
        private NotesViewPresenter _presenter;

        public NotesController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _presenter = new NotesViewPresenter(this);
            
            NavigationController.SetNavigationBarHidden(false, false);
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Notes, ConstantsHelper.Notes);

            _sideMenuManager = new SideMenuManager();
            NavigationItem.SetLeftBarButtonItem(
                new UIBarButtonItem(ConstantsHelper.Menu, UIBarButtonItemStyle.Plain, (sender, e) => {
                    PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                }),
                false);
            SetupSideMenu();

            NotesDataSource = new NotesDataSource(_presenter, this);

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };

            TableView.RefreshControl = _refreshControl;
            TableView.RegisterClassForCellReuse(typeof(NoteCell), nameof(NoteCell));
            TableView.SeparatorColor = UIColor.Clear;
            TableView.Source = new NotesDataSource(_presenter, this);

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
            TableView.Source = new NotesDataSource(_presenter, this);
            TableView.ReloadData();
        }

        private void NavigateToEditNoteController(object sender, EventArgs e)
        {
            _presenter.NavigateToNoteEditViewController(true);
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}
