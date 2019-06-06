using System;
using System.Linq;
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

            _dataSource = new ToDoDataSource(_repository.GetAll().Where(x => x.Status == "Active").ToList());
            TableView.Source = _dataSource;
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Active, ConstantsHelper.Active);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }
    }
}