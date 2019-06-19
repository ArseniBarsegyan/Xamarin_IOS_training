using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace CRUDApp.ViewComponents.ToDo.ToDoEdit
{
    public class StatusPickerViewModel : UIPickerViewModel
    {
        private readonly List<string> _data;
        public EventHandler ValueChanged;
        public string SelectedValue;

        public StatusPickerViewModel(List<string> data)
        {
            _data = data;
            SelectedValue = _data.ElementAt(0);
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _data.Count;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _data.ElementAt((int) row);
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            SelectedValue = _data.ElementAt((int)row);
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}