using System;
using System.Collections.Generic;
using System.Linq;
using CRUDApp.Helpers;
using UIKit;
using Xamarin.SideMenu;

namespace CRUDApp.ViewComponents.Root
{
    public class SideMenuViewController : UITableViewController
    {
        private readonly List<MasterPageItem> _items;
        private readonly SideMenuViewPresenter _presenter;

        public SideMenuViewController(List<MasterPageItem> items)
        {
            _items = items;
            _presenter = new SideMenuViewPresenter(this);
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
            cell.ImageView.Image = UIImage.FromBundle(_items.ElementAt(indexPath.Row).IconSource);
            cell.TextLabel.Text = _items.ElementAt(indexPath.Row).Title;
            return cell;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var item = _items.ElementAt(indexPath.Row);
            switch (item.Index)
            {
                case MenuViewIndex.NotesView:
                    _presenter.NavigateToNotesSection();
                    break;
                case MenuViewIndex.ToDoView:
                    _presenter.NavigateToToDoSection();
                    break;
                case MenuViewIndex.MapsView:
                    _presenter.NavigateToMapsSection();
                    break;
                case MenuViewIndex.SettingsView:
                    _presenter.NavigateToSettingsSection();
                    break;
                case MenuViewIndex.Logout:
                    _presenter.Logout();
                    break;
            }
        }
    }

    public class SideMenuViewCell : UITableViewVibrantCell
    {
        public SideMenuViewCell(IntPtr handle) : base(handle)
        {
        }
    }
}
