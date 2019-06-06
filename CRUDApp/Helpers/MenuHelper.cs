using System.Collections.Generic;

namespace CRUDApp.Helpers
{
    /// <summary>
    /// Helper class. Provide list of MasterPageItem for MenuView.
    /// </summary>
    public static class MenuHelper
    {
        public static List<MasterPageItem> GetMenu()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = ConstantsHelper.Notes,
                    IconSource = ConstantsHelper.NotesListIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.NotesView
                },
                new MasterPageItem
                {
                    Title = ConstantsHelper.ToDo,
                    IconSource = ConstantsHelper.ToDoListIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.ToDoView
                },
                new MasterPageItem
                {
                    Title = ConstantsHelper.Achievements,
                    IconSource = ConstantsHelper.AchievementsIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.AchievementsView
                },
                new MasterPageItem
                {
                    Title = ConstantsHelper.Maps,
                    IconSource = ConstantsHelper.MapsIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.MapsView
                },
                new MasterPageItem
                {
                    Title = ConstantsHelper.Settings,
                    IconSource = ConstantsHelper.SettingsIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.SettingsView
                },
                new MasterPageItem
                {
                    Title = ConstantsHelper.Logout,
                    IconSource = ConstantsHelper.LogoutIcon,
                    IsDisplayed = true,
                    Index = MenuViewIndex.Logout
                }
            };
            return masterPageItems;
        }
    }

    public class MasterPageItem
    {
        /// <summary>
        /// Title that will be displayed in side menu.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Icon source that will be displayed in side menu.
        /// </summary>
        public string IconSource { get; set; }
        /// <summary>
        /// Show this item in side menu or not.
        /// </summary>
        public bool IsDisplayed { get; set; }
        /// <summary>
        /// Display item index.
        /// </summary>
        public MenuViewIndex Index { get; set; }
    }

    public enum MenuViewIndex
    {
        NotesView,
        ToDoView,
        AchievementsView,
        MapsView,
        SettingsView,
        Logout
    }
}
