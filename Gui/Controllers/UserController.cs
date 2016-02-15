using System;
using System.Net;
using System.Web.Mvc;
using Common;
using Models;
using NLog;
using Repositories;
using UseCases;

namespace Gui.Controllers
{
    public class UserController : Controller
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _repo;
        private readonly IUserCase _case;

        public UserController(IUserRepository repo, IUserCase ucase)
        {
            _repo = repo;
            _case = ucase;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_repo.GetAllUsers());
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
                log.Error(e.Message);
                return View(newUser);
            }
        }

        [HttpDelete]
        public ActionResult Delete(IFormattable id)
        {
            _case.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _repo.GetUser(new Guid(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}