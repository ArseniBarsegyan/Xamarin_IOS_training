using System;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.Done
{
    public class ToDoDoneViewController : UITableViewController
    {
        private readonly ToDoRepository _repository;
        private UIRefreshControl _refreshControl;
        private ToDoDataSource _dataSource;

        public ToDoDoneViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoDoneViewController(ToDoRepository repository)
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

            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Done, ConstantsHelper.Done);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }

        private async Task Refresh()
        {
            _refreshControl.BeginRefreshing();
            await Task.Delay(200);
            _refreshControl.EndRefreshing();
            TableView.Source = new ToDoDataSource(_repository.GetAll().Where(x => x.Status == "Done").ToList());
            TableView.ReloadData();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}