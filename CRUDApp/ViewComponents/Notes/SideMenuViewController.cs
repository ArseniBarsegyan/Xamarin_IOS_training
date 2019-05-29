using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp
{
    public class SideMenuViewController : UITableViewController
    {
        private readonly List<string> _items;

        public SideMenuViewController(List<string> items)
        {
            _items = items;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.RegisterClassForCellReuse(typeof(SideMenuViewCell), nameof(SideMenuViewCell));
            TableView.SeparatorColor = UIColor.Clear;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(nameof(SideMenuViewCell));
            cell.BackgroundColor = UIColor.White;
            cell.TextLabel.Text = _items.ElementAt(indexPath.Row);
            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            //tableView.DeselectRow(indexPath, true);
        }
    }

    public class SideMenuViewCell : UITableViewVibrantCell
    {
        public SideMenuViewCell(IntPtr handle) : base(handle)
        {
        }
    }
}
