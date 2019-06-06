using System;
using Cirrious.FluentLayouts.Touch;
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
            View.BackgroundColor = UIColor.White;

            var dateLabel = new UILabel();
            dateLabel.Text = "Date:";
            dateLabel.Font = UIFont.SystemFontOfSize(16);
            dateLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            var currentDateLabel = new UILabel();
            currentDateLabel.Text = DateTime.Now.ToString("d");
            currentDateLabel.Font = UIFont.SystemFontOfSize(16);
            currentDateLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            //var datePicker = new UIDatePicker();
            //datePicker.TranslatesAutoresizingMaskIntoConstraints = false;
            //datePicker.MinimumDate = (NSDate) DateTime.Today.AddYears(-1);
            //datePicker.MaximumDate = (NSDate)DateTime.Today.AddYears(1);

            var timeLabel = new UILabel();
            timeLabel.Text = "Time:";
            timeLabel.Font = UIFont.SystemFontOfSize(16);
            timeLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            var descriptionLabel = new UILabel();
            descriptionLabel.Text = "Description:";
            descriptionLabel.Font = UIFont.SystemFontOfSize(16);
            descriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            var descriptionEditor = new UITextView();
            descriptionEditor.Text = "Test to-do";
            descriptionEditor.Font = UIFont.SystemFontOfSize(16);
            descriptionEditor.TranslatesAutoresizingMaskIntoConstraints = false;

            View.AddSubviews(dateLabel, currentDateLabel, timeLabel, descriptionLabel, descriptionEditor);

            View.AddConstraints(dateLabel.AtLeftOf(View, 10f),
                dateLabel.AtTopOf(View, 80f),
                dateLabel.Width().EqualTo(100f),
                dateLabel.Height().EqualTo(30f),
                currentDateLabel.WithSameCenterY(dateLabel),
                currentDateLabel.ToRightOf(dateLabel, 10f),
                currentDateLabel.Width().EqualTo(View.Bounds.Width),
                currentDateLabel.Height().EqualTo(30f),
                timeLabel.Below(dateLabel, 10f),
                timeLabel.WithSameLeft(dateLabel),
                timeLabel.Width().EqualTo(100f),
                timeLabel.Height().EqualTo(30f),
                descriptionLabel.Below(timeLabel, 10f),
                descriptionLabel.WithSameLeft(timeLabel),
                descriptionLabel.Width().EqualTo(100f),
                descriptionLabel.Height().EqualTo(30f),
                descriptionEditor.Below(descriptionLabel, 10f),
                descriptionEditor.WithSameLeft(descriptionLabel),
                descriptionEditor.Width().EqualTo(View.Frame.Width),
                descriptionEditor.Height().EqualTo(500f));
        }

        public void SetRepository(ToDoRepository repository)
        {
            _repository = repository;
        }
    }
}