using System;
using System.Net;
using System.Web.Mvc;
using Models;
using NLog;
using UseCases;

namespace Gui.Controllers
{
    public class UserController : Controller
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IUserCase _case;

        public UserController(IUserCase ucase)
        {
            _case = ucase;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_case.GetUsers());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _case.CreateUser(newUser.Nickname, newUser.Email);   
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newUser);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
                return View(newUser);
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            User user = _case.GetUser(new Guid(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _case.DeleteUser(new Guid(id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _case.GetUser(new Guid(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}