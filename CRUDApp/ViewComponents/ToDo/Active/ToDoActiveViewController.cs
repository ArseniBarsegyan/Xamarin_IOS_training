using System;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.Active
{
    public class ToDoActiveViewController : UITableViewController
    {
        private readonly ToDoRepository _repository;
        private ToDoDataSource _dataSource;
        private UIRefreshControl _refreshControl;

        public ToDoActiveViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoActiveViewController(ToDoRepository repository)
        {
            _repository = repository;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };
            TableView.RefreshControl = _refreshControl;
            
            _dataSource = new ToDoDataSource(_repository.GetAll().Where(x => x.Status == "Active").ToList());
            TableView.Source = _dataSource;
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Active, ConstantsHelper.Active);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }

        private async Task Refresh()
        {
            _refreshControl.BeginRefreshing();
            await Task.Delay(200);
            _refreshControl.EndRefreshing();
            TableView.Source = new ToDoDataSource(_repository.GetAll().Where(x => x.Status == "Active").ToList());
            TableView.ReloadData();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}