using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GestorTickets.Controllers
{
    public class InicioController : Controller
    {
        private readonly gestorticketsDbContext _context;

        public InicioController(gestorticketsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidarUsuario(Credenciales Credenciales)
        {
            Usuario? Usuario = (from u in _context.Usuario
                                where u.Correo == Credenciales.Correo
                                && u.Clave == Credenciales.Clave
                                select u).FirstOrDefault();

            if (Usuario == null)
            {
                ViewBag.Mensaje = "Credenciales incorrectas!!";
                return View("Index");
            }

            string datosUsuario = JsonSerializer.Serialize(Usuario);

            HttpContext.Session.SetString("user", datosUsuario);

            return RedirectToAction("Index", "Ticket");

            
        }
    }
}
