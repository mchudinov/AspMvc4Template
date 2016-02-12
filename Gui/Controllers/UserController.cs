using System;
using System.Web.Mvc;
using Models;
using Repositories;
using UseCases;

namespace Gui.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IUserCase _usercase;

        public UserController(IUserRepository repo, IUserCase ucase)
        {
            _repo = repo;
            _usercase = ucase;
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
                    var newId = _usercase.CreateUser(newUser.Nickname, newUser.Email);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newUser);
                }
            }
            catch (Exception e)
            {
                return View(newUser);
            }
        }
    }
}