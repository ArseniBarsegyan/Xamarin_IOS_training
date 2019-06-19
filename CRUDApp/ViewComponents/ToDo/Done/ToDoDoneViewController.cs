using System;
using System.Threading.Tasks;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.Done
{
    public class ToDoDoneViewController : ToDoBaseViewController
    {
        public ToDoDoneViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoDoneViewController(ToDoRepository repository) : base(repository)
        {
            Repository = repository;
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

            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Done, ConstantsHelper.Done);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }

        private async Task Refresh()
        {
            RefreshControl.BeginRefreshing();
            await Task.Delay(200);
            RefreshControl.EndRefreshing();
            TableView.Source = new ToDoDataSource(this);
            TableView.ReloadData();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            await Refresh();
        }
    }
}