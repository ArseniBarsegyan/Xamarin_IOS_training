using System;
using System.IO;
using System.Linq;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.Done
{
    public class ToDoDoneViewController : UITableViewController
    {
        private readonly ToDoRepository _repository;
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
            
            _dataSource = new ToDoDataSource(_repository.GetAll().Where(x => x.Status == "Done").ToList());
            TableView.Source = _dataSource;

            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.Done, ConstantsHelper.Done);
            TableView.RegisterClassForCellReuse(typeof(ToDoCell), nameof(ToDoCell));
            TableView.SeparatorColor = UIColor.LightGray;
        }
    }
}