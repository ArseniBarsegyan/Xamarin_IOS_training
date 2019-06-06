using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Data.Entities;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo
{
    public class ToDoDataSource : UITableViewSource
    {
        private readonly List<ToDoModel> _toDoModels;

        public ToDoDataSource(List<ToDoModel> toDoModels)
        {
            _toDoModels = toDoModels;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(ToDoCell), indexPath);
            var toDo = _toDoModels.ElementAt(indexPath.Row);
            cell.TextLabel.Text = toDo.Description;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _toDoModels.Count;
        }
    }
}