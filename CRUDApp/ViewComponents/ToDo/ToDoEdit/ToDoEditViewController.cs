using System;
using System.Threading.Tasks;
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

        private UILabel _dateLabel;
        private UITextField _currentDateLabel;
        private UILabel _timeLabel;
        private UILabel _descriptionLabel;
        private UITextView _descriptionEditor;

        private UITapGestureRecognizer _currentDateGestureRecognizer;

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

            _dateLabel = new UILabel();
            _dateLabel.Text = "Date:";
            _dateLabel.Font = UIFont.SystemFontOfSize(16);
            _dateLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _currentDateLabel = new UITextField();
            _currentDateLabel.Layer.BorderColor = UIColor.LightGray.CGColor;
            _currentDateLabel.Layer.BorderWidth = 0.5f;
            _currentDateLabel.Layer.CornerRadius = 5f;
            _currentDateLabel.UserInteractionEnabled = true;
            _currentDateLabel.TextAlignment = UITextAlignment.Center;

            var formatter = new NSDateFormatter { DateFormat = "dd/MM/yyyy" };
            _currentDateLabel.Text = formatter.StringFor(NSDate.Now);
            _currentDateLabel.Font = UIFont.SystemFontOfSize(16);
            _currentDateLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _timeLabel = new UILabel();
            _timeLabel.Text = "Time:";
            _timeLabel.Font = UIFont.SystemFontOfSize(16);
            _timeLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _descriptionLabel = new UILabel();
            _descriptionLabel.Text = "Description:";
            _descriptionLabel.Font = UIFont.SystemFontOfSize(16);
            _descriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _descriptionEditor = new UITextView();
            _descriptionEditor.Text = "Test to-do";
            _descriptionEditor.Font = UIFont.SystemFontOfSize(16);
            _descriptionEditor.TranslatesAutoresizingMaskIntoConstraints = false;

            _currentDateGestureRecognizer = new UITapGestureRecognizer(async () => { await ShowDateTimePopup(); })
                { NumberOfTapsRequired = 1 };

            View.AddSubviews(_dateLabel, 
                _currentDateLabel, 
                _timeLabel, 
                _descriptionLabel, 
                _descriptionEditor);

            View.AddConstraints(_dateLabel.AtLeftOf(View, 10f),
                _dateLabel.AtTopOf(View, 80f),
                _dateLabel.Width().EqualTo(100f),
                _dateLabel.Height().EqualTo(30f),
                _currentDateLabel.WithSameCenterY(_dateLabel),
                _currentDateLabel.ToRightOf(_dateLabel),
                _currentDateLabel.Width().EqualTo(200f),
                _currentDateLabel.Height().EqualTo(30f),
                _timeLabel.Below(_dateLabel, 10f),
                _timeLabel.WithSameLeft(_dateLabel),
                _timeLabel.Width().EqualTo(100f),
                _timeLabel.Height().EqualTo(30f),
                _descriptionLabel.Below(_timeLabel, 10f),
                _descriptionLabel.WithSameLeft(_timeLabel),
                _descriptionLabel.Width().EqualTo(100f),
                _descriptionLabel.Height().EqualTo(30f),
                _descriptionEditor.Below(_descriptionLabel, 10f),
                _descriptionEditor.WithSameLeft(_descriptionLabel),
                _descriptionEditor.Width().EqualTo(View.Bounds.Width - 20f),
                _descriptionEditor.Height().EqualTo(500f));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _currentDateLabel.AddGestureRecognizer(_currentDateGestureRecognizer);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _currentDateLabel.RemoveGestureRecognizer(_currentDateGestureRecognizer);
        }

        public void SetRepository(ToDoRepository repository)
        {
            _repository = repository;
        }

        private async Task ShowDateTimePopup()
        {
            var dateTimeAlertController = new UIAlertController();

            var datePicker = new UIDatePicker();
            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.TranslatesAutoresizingMaskIntoConstraints = false;
            datePicker.MinimumDate = (NSDate)DateTime.Today.AddYears(-1);
            datePicker.MaximumDate = (NSDate)DateTime.Today.AddYears(1);

            var rootView = dateTimeAlertController.View;
            rootView.AddConstraints(datePicker.WithSameTop(rootView), 
                datePicker.AtBottomOf(rootView, 100f), 
                datePicker.WithSameLeft(rootView), 
                datePicker.WithSameRight(rootView));
            rootView.AddSubview(datePicker);

            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, action =>
                {
                    SetDate(datePicker.Date);
                }));
            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Cancel, UIAlertActionStyle.Cancel, null));
            PresentViewController(dateTimeAlertController, true, null);
        }

        private void SetDate(NSDate dateTime)
        {
            var formatter = new NSDateFormatter { DateFormat = "dd/MM/yyyy" };
            _currentDateLabel.Text = formatter.StringFor(dateTime);
        }
    }
}