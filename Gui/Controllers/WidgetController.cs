using System;
using System.Web.Mvc;
using Models;
using NLog;
using Repositories;
using UseCases;

namespace Gui.Controllers
{
    public class WidgetController : Controller
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private readonly IWidgetRepository _repo;
        private readonly IWidgetCase _case;

        public WidgetController(IWidgetRepository repo, IWidgetCase ucase)
        {
            _repo = repo;
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
                    _case.CreateWidget(newWidget.Name, newWidget.Price, 123);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newWidget);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return View(newWidget);
            }
        }
    }
}
