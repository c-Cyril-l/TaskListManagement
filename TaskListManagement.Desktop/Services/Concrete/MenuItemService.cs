using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;

namespace TaskListManagement.Desktop.Services.Concrete
{
    public class MenuItemService
    {

        private readonly GeometryData _geometryData;

        public MenuItemService()
        {
            _geometryData = new GeometryData();
        }
        private string GetMenuIcon(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => _geometryData.Info,
                MenuType.Critical => _geometryData.Critical,
                MenuType.Warning => _geometryData.Warning,
                _ => _geometryData.Success
            };
        }

        public void AddItem(MenuType menuType, ICollection tasks, ObservableCollection<MenuItem> target)
        {
            var menuItem = new MenuItem
            {
                ItemCount = tasks.Count,
                Name = GetTitle(menuType),
                Type = menuType,
                IconData = GetMenuIcon(menuType),
                ForeColor = GetBrush(menuType),
            };

            target.Add(menuItem);
        }

        public void UpdateMenuCount(TaskType taskType, IEnumerable<MenuItem> target, int count)
        {
            var menuType = GetMenuTypeByTaskType(taskType);
            var menus = target.Where(mt => mt.Type == menuType);
            foreach (var menuItem in menus)
            {
                menuItem.ItemCount = count;
            }
        }

        private static MenuType GetMenuTypeByTaskType(TaskType taskType)
        {
            return taskType switch
            {
                TaskType.Info => MenuType.Info,
                TaskType.Critical => MenuType.Critical,
                TaskType.Warning => MenuType.Warning,
                _ => MenuType.Success
            };
        }

        private static Brush GetBrush(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => Brushes.DodgerBlue,
                MenuType.Critical => Brushes.Crimson,
                MenuType.Warning => Brushes.Orange,
                _ => Brushes.MediumSeaGreen,

            };
        }

        private static string GetTitle(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => @"Info Tasks",
                MenuType.Critical => @"Critical Tasks",
                MenuType.Warning => @"Warning Tasks",
                _ => @"Succeed Tasks",
            };
        }

    }
}