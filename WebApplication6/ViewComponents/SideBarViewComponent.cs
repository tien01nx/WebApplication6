using MesWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MesWeb.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var categories = new List<CategoryViewModel>
            {
                new CategoryViewModel { Id = 1, CategoryName = "Dashboard", Level = 0, ParentId = 0, Class = "collapsed" },
                new CategoryViewModel { Id = 2, CategoryName = "Management", Level = 0, ParentId = 0, Class = "collapsed" },
                new CategoryViewModel { Id = 3, CategoryName = "Users", Level = 1, ParentId = 2 },
                new CategoryViewModel { Id = 4, CategoryName = "Settings", Level = 1, ParentId = 2 },
                new CategoryViewModel { Id = 5, CategoryName = "User List", Level = 2, ParentId = 3 },
                new CategoryViewModel { Id = 6, CategoryName = "Add User", Level = 2, ParentId = 3 },
                new CategoryViewModel { Id = 7, CategoryName = "General", Level = 2, ParentId = 4 },
                new CategoryViewModel { Id = 8, CategoryName = "Security", Level = 2, ParentId = 4 },
                new CategoryViewModel { Id = 9, CategoryName = "User Details", Level = 3, ParentId = 5, Link = "user/details" },
                new CategoryViewModel { Id = 10, CategoryName = "User Settings", Level = 3, ParentId = 5, Link = "user/settings" },
                new CategoryViewModel { Id = 11, CategoryName = "General Settings", Level = 3, ParentId = 7, Link = "settings/general" },
                new CategoryViewModel { Id = 12, CategoryName = "Security Settings", Level = 3, ParentId = 8, Link = "settings/security" }
            };
            return View(categories);
        }
    }
}
