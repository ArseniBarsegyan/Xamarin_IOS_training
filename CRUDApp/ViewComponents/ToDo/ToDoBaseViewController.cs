using System;
using CRUDApp.Data.Repositories;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo
{
    public abstract class ToDoBaseViewController : UITableViewController
    {
        protected ToDoDataSource DataSource;

        protected ToDoBaseViewController(IntPtr handle) : base(handle)
        {
        }

        protected ToDoBaseViewController(ToDoRepository repository)
        {
            Repository = repository;
        }

        public ToDoRepository Repository { get; protected set; }
    }
}