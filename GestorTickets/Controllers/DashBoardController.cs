using Microsoft.AspNetCore.Mvc;

namespace GestorTickets.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
