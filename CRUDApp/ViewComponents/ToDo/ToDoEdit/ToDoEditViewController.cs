using System;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.ToDoEdit
{
    public class ToDoEditViewController : UIViewController
    {
        private ToDoRepository _repository;

        public ToDoEditViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoEditViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.EditToDo, ConstantsHelper.EditToDo);
            View.BackgroundColor = UIColor.Blue;
        }

        public void SetRepository(ToDoRepository repository)
        {
            _repository = repository;
        }
    }
}