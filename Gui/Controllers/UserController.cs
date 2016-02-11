using System.Web.Mvc;
using Repositories;

namespace Gui.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: All Users
        public ActionResult Index()
        {
            return View(_repo.GetAllUsers());
        }
    }
}