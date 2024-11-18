using Microsoft.AspNetCore.Mvc;

namespace MesWeb.ViewComponents
{
    public class PermissionButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string formName)
        {
            if (formName == null)
            {
                return View(new List<string>());
            }
            // Lấy JSON từ Session
            //var permissionsJson = HttpContext.Session.GetString("UserPermissions");

            //// Chuyển JSON thành danh sách quyền
            //var permissions = permissionsJson != null
            //    ? JsonConvert.DeserializeObject<List<dynamic>>(permissionsJson)
            //    : new List<dynamic>();

            //// Lọc danh sách quyền theo formName và lấy danh sách actions
            //var actions = permissions
            //    .Where(p => p.Form == formName)
            //    .Select(p => (string)p.Action) // Chuyển thành danh sách string
            //    .ToList();

            return View(formName);
        }
    }
}
