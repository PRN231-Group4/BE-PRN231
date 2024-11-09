using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Configuration;
using System.Net.Http.Headers;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve roleId from session
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Set the layout based on role
            if (roleId == 2) // Admin
            {
                ViewData["Layout"] = "~/Views/Shared/Layout/_LayoutAdmin.cshtml";
            }
            else if (roleId == 1) // Staff
            {
                ViewData["Layout"] = "~/Views/Shared/Layout/_LayoutStaff.cshtml";
            }
            else // 
            {
                ViewData["Layout"] = "~/Views/Shared/Layout/_LayoutManager.cshtml";
            }

            base.OnActionExecuting(context);
        }
    }

}
