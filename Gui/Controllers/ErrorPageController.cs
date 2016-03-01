using System;
using System.Web;
using System.Web.Mvc;

namespace Gui.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            var error = new Models.Error
            {
                StatusCode = statusCode.ToString() + " error",
                StatusDescription = HttpWorkerRequest.GetStatusDescription(statusCode),
                Message = exception.Message
            };
            return View(error);
        }
    }
}