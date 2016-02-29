using System;
using System.Web.Mvc;
using Models;
using NLog;
using UseCases;

namespace Gui.Controllers
{
    public class WidgetController : Controller
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IWidgetCase _case;

        public WidgetController(IWidgetCase ucase)
        {
            _case = ucase;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Widget newWidget)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _case.CreateWidget(newWidget.Name, newWidget.Price, newWidget.User.Id);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newWidget);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
                return View(newWidget);
            }
        }
    }
}
