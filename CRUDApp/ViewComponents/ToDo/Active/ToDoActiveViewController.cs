using System;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.Active
{
    public class ToDoActiveViewController : ToDoBaseViewController
    {
        public ToDoActiveViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoActiveViewController(ToDoRepository repository) : base(repository)
        {
            Repository = repository;
            DataSource = new ToDoDataSource(this);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += async (sender, args) =>
            {
                await Refresh();
            };
            TableView.RefreshControl = RefreshControl;

            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Active, ConstantsHelper.Active);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }

        private async Task Refresh()
        {
            RefreshControl.BeginRefreshing();
            await Task.Delay(200);
            RefreshControl.EndRefreshing();
            DataSource = new ToDoDataSource(this);
            TableView.Source = DataSource;
            TableView.ReloadData();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}