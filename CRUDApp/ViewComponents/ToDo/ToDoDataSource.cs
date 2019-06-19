using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Data.Entities;
using CRUDApp.ViewComponents.ToDo.Active;
using CRUDApp.ViewComponents.ToDo.Done;
using CRUDApp.ViewComponents.ToDo.ToDoEdit;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo
{
    public class ToDoDataSource : UITableViewSource
    {
        private readonly ToDoBaseViewController _controller;
        
        public ToDoDataSource(ToDoBaseViewController controller)
        {
            _controller = controller;
            if (controller is ToDoActiveViewController activeViewController)
            {
                ToDoModels = activeViewController.Repository.GetAll().Where(x => x.Status == "Active").ToList();
            }
            else if (controller is ToDoDoneViewController doneViewController)
            {
                ToDoModels = doneViewController.Repository.GetAll().Where(x => x.Status == "Done").ToList();
            }
        }

        public List<ToDoModel> ToDoModels { get; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(ToDoCell), indexPath);
            var toDo = ToDoModels.ElementAt(indexPath.Row);
            cell.TextLabel.Text = toDo.Description;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var toDo = ToDoModels.ElementAt(indexPath.Row);
            var toDoEditViewController = new ToDoEditViewController(toDo.Id);
            toDoEditViewController.SetRepository(_controller.Repository);
            _controller.NavigationController.PushViewController(toDoEditViewController, true);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ToDoModels.Count;
        }
    }
}