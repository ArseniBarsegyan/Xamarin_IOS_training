﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CRUDApp.Data.Entities;
using CRUDApp.Data.Repositories;
using CRUDApp.Helpers;
using Foundation;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.ToDoEdit
{
    public class ToDoEditViewController : UIViewController
    {
        private ToDoRepository _repository;
        private readonly int _toDoId;

        private NSDate _selectedDate;
        private string _selectedStatus;

        private UILabel _dateLabel;
        private UITextField _currentDateField;
        private UILabel _timeLabel;
        private UITextField _currentTimeField;
        private UILabel _statusLabel;
        private UITextField _statusField;
        private UILabel _descriptionLabel;
        private UITextView _descriptionEditor;

        private UITapGestureRecognizer _statusGestureRecognizer;
        private UITapGestureRecognizer _currentDateGestureRecognizer;
        private UITapGestureRecognizer _currentTimeGestureRecognizer;

        public ToDoEditViewController(IntPtr handle) : base(handle)
        {
        }

        public ToDoEditViewController(int toDoId)
        {
            _toDoId = toDoId;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _toDoId == 0 ? 
                NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.NewToDo, ConstantsHelper.NewToDo)
                : NSBundle.MainBundle.GetLocalizedString(ConstantsHelper.EditToDo, ConstantsHelper.EditToDo);
            
            View.BackgroundColor = UIColor.White;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, SaveChanges)
            {
                AccessibilityLabel = ConstantsHelper.ConfirmButtonAccessibilityLabel
            };
            NavigationItem.RightBarButtonItem = addButton;

            _selectedDate = NSDate.Now;

            _dateLabel = new UILabel();
            _dateLabel.Text = "Date:";
            _dateLabel.Font = UIFont.SystemFontOfSize(16);
            _dateLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _currentDateField = new UITextField();
            _currentDateField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _currentDateField.Layer.BorderWidth = 0.5f;
            _currentDateField.Layer.CornerRadius = 5f;
            _currentDateField.UserInteractionEnabled = true;
            _currentDateField.TextAlignment = UITextAlignment.Center;

            var dateFormatter = new NSDateFormatter { DateFormat = "dd/MM/yyyy" };
            _currentDateField.Text = dateFormatter.StringFor(_selectedDate);
            _currentDateField.Font = UIFont.SystemFontOfSize(16);
            _currentDateField.TranslatesAutoresizingMaskIntoConstraints = false;

            _timeLabel = new UILabel();
            _timeLabel.Text = "Time:";
            _timeLabel.Font = UIFont.SystemFontOfSize(16);
            _timeLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            var timeFormatter = new NSDateFormatter { DateFormat = "HH:mm" };
            _currentTimeField = new UITextField();
            _currentTimeField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _currentTimeField.Layer.BorderWidth = 0.5f;
            _currentTimeField.Layer.CornerRadius = 5f;
            _currentTimeField.UserInteractionEnabled = true;
            _currentTimeField.TextAlignment = UITextAlignment.Center;
            _currentTimeField.Text = timeFormatter.StringFor(_selectedDate);
            _currentTimeField.Font = UIFont.SystemFontOfSize(16);
            _currentTimeField.TranslatesAutoresizingMaskIntoConstraints = false;

            _statusLabel = new UILabel();
            _statusLabel.Text = "Status:";
            _statusLabel.Font = UIFont.SystemFontOfSize(16);
            _statusLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _statusField = new UITextField();
            _statusField.Layer.BorderColor = UIColor.LightGray.CGColor;
            _statusField.Layer.BorderWidth = 0.5f;
            _statusField.Layer.CornerRadius = 5f;
            _statusField.UserInteractionEnabled = true;
            _statusField.TextAlignment = UITextAlignment.Center;
            _statusField.Text = "Active";
            _statusField.TranslatesAutoresizingMaskIntoConstraints = false;

            _descriptionLabel = new UILabel();
            _descriptionLabel.Text = "Description:";
            _descriptionLabel.Font = UIFont.SystemFontOfSize(16);
            _descriptionLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            _descriptionEditor = new UITextView();
            _descriptionEditor.Text = "Test to-do";
            _descriptionEditor.Font = UIFont.SystemFontOfSize(16);
            _descriptionEditor.TranslatesAutoresizingMaskIntoConstraints = false;

            _statusGestureRecognizer = new UITapGestureRecognizer(async () => { await ShowStatusPopup(); })
                { NumberOfTapsRequired = 1};
            _currentDateGestureRecognizer = new UITapGestureRecognizer(async () => { await ShowDatePopup(); })
                { NumberOfTapsRequired = 1 };
            _currentTimeGestureRecognizer = new UITapGestureRecognizer(async () => { await ShowTimePopup(); })
                { NumberOfTapsRequired = 1 };

            View.AddSubviews(_dateLabel, 
                _currentDateField, 
                _timeLabel, 
                _currentTimeField,
                _statusLabel,
                _statusField,
                _descriptionLabel, 
                _descriptionEditor);

            View.AddConstraints(_dateLabel.AtLeftOf(View, 10f),
                _dateLabel.AtTopOf(View, 80f),
                _dateLabel.Width().EqualTo(100f),
                _dateLabel.Height().EqualTo(30f),
                _currentDateField.WithSameCenterY(_dateLabel),
                _currentDateField.ToRightOf(_dateLabel),
                _currentDateField.Width().EqualTo(200f),
                _currentDateField.Height().EqualTo(30f),
                _timeLabel.Below(_dateLabel, 10f),
                _timeLabel.WithSameLeft(_dateLabel),
                _timeLabel.Width().EqualTo(100f),
                _timeLabel.Height().EqualTo(30f),
                _currentTimeField.WithSameCenterY(_timeLabel),
                _currentTimeField.ToRightOf(_timeLabel),
                _currentTimeField.Width().EqualTo(200f),
                _currentTimeField.Height().EqualTo(30f),
                _statusLabel.Below(_timeLabel, 10f),
                _statusLabel.WithSameLeft(_timeLabel),
                _statusLabel.Width().EqualTo(100f),
                _statusLabel.Height().EqualTo(30f),
                _statusField.WithSameCenterY(_statusLabel),
                _statusField.ToRightOf(_statusLabel),
                _statusField.Width().EqualTo(200f),
                _statusField.Height().EqualTo(30f),
                _descriptionLabel.Below(_statusLabel, 10f),
                _descriptionLabel.WithSameLeft(_statusLabel),
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
            _currentDateField.AddGestureRecognizer(_currentDateGestureRecognizer);
            _currentTimeField.AddGestureRecognizer(_currentTimeGestureRecognizer);
            _statusField.AddGestureRecognizer(_statusGestureRecognizer);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _currentDateField.RemoveGestureRecognizer(_currentDateGestureRecognizer);
            _currentTimeField.RemoveGestureRecognizer(_currentTimeGestureRecognizer);
            _statusField.RemoveGestureRecognizer(_statusGestureRecognizer);
        }

        public void SetRepository(ToDoRepository repository)
        {
            _repository = repository;
        }

        private void SaveChanges(object sender, EventArgs args)
        {
            var dateFormatter = new NSDateFormatter { DateFormat = "dd/MM/yyyy HH:mm:ss" };
            var dateString = dateFormatter.StringFor(_selectedDate);
            var result = DateTime.TryParse(dateString, out var dateTime);

            var model = new ToDoModel
            {
                Id = _toDoId,
                Status = _selectedStatus,
                WhenHappens = dateTime,
                Description = _descriptionEditor.Text
            };

            _repository.Save(model);
            NavigationController.PopViewController(true);
        }

        private async Task ShowDatePopup()
        {
            var dateTimeAlertController = new UIAlertController();

            var datePicker = new UIDatePicker();
            datePicker.Date = _selectedDate;
            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.TranslatesAutoresizingMaskIntoConstraints = false;
            datePicker.MinimumDate = (NSDate)DateTime.Today.AddYears(-1);
            datePicker.MaximumDate = (NSDate)DateTime.Today.AddYears(1);

            var rootView = dateTimeAlertController.View;
            rootView.AddSubview(datePicker);
            rootView.AddConstraints(datePicker.WithSameTop(rootView), 
                datePicker.AtBottomOf(rootView, 100f), 
                datePicker.WithSameLeft(rootView), 
                datePicker.WithSameRight(rootView));

            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, action =>
            {
                _selectedDate = datePicker.Date;
                var dateFormatter = new NSDateFormatter { DateFormat = "dd/MM/yyyy" };
                _currentDateField.Text = dateFormatter.StringFor(_selectedDate);

                var timeFormatter = new NSDateFormatter { DateFormat = "HH:mm" };
                _currentTimeField.Text = timeFormatter.StringFor(_selectedDate);
            }));
            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Cancel, UIAlertActionStyle.Cancel, null));
            await PresentViewControllerAsync(dateTimeAlertController, true);
        }

        private async Task ShowTimePopup()
        {
            var dateTimeAlertController = new UIAlertController();

            var timePicker = new UIDatePicker();
            timePicker.Date = _selectedDate;
            timePicker.Mode = UIDatePickerMode.Time;
            timePicker.TranslatesAutoresizingMaskIntoConstraints = false;

            var rootView = dateTimeAlertController.View;
            rootView.AddSubview(timePicker);
            rootView.AddConstraints(timePicker.WithSameTop(rootView),
                timePicker.AtBottomOf(rootView, 100f),
                timePicker.WithSameLeft(rootView),
                timePicker.WithSameRight(rootView));

            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default, action =>
            {
                _selectedDate = timePicker.Date;
                var timeFormatter = new NSDateFormatter { DateFormat = "HH:mm" };
                _currentTimeField.Text = timeFormatter.StringFor(_selectedDate);
            }));
            dateTimeAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Cancel, UIAlertActionStyle.Cancel, null));
            await PresentViewControllerAsync(dateTimeAlertController, true);
        }

        private async Task ShowStatusPopup()
        {
            var statusAlertController = new UIAlertController();
            var data = new List<string> { "Active", "Done" };
            var rootView = statusAlertController.View;

            var picker = new UIPickerView();
            picker.Model = new StatusPickerViewModel(data);
            picker.TranslatesAutoresizingMaskIntoConstraints = false;
            rootView.AddSubview(picker);
            rootView.AddConstraints(picker.WithSameTop(rootView),
                picker.AtBottomOf(rootView, 100f),
                picker.WithSameLeft(rootView),
                picker.WithSameRight(rootView));

            statusAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Ok, UIAlertActionStyle.Default,
                action =>
                {
                    var pickerModel = picker.Model as StatusPickerViewModel;
                    _selectedStatus = pickerModel?.SelectedValue;
                    _statusField.Text = _selectedStatus;
                }));
            statusAlertController.AddAction(UIAlertAction.Create(ConstantsHelper.Cancel, UIAlertActionStyle.Cancel, null));
            await PresentViewControllerAsync(statusAlertController, true);
        }
    }
}