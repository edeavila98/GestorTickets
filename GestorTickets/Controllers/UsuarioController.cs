using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorTickets.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly gestorticketsDbContext _gestorticketsDbContext;

        public UsuarioController(gestorticketsDbContext gestorticketsDbContext)
        {
            _gestorticketsDbContext = gestorticketsDbContext;
        }

        public IActionResult Index()
        {
            //Listar los Roles
            var listaDeRoles = (from r in _gestorticketsDbContext.Roles
                                  select r).ToList();
            ViewData["listadoDeRoles"] = new SelectList(listaDeRoles, "IdRol", "NombreRol");

            //Listar los Usuarios
            var listaDeRol = (from u in _gestorticketsDbContext.Usuario
                              join r in _gestorticketsDbContext.Roles on u.IdRol equals r.IdRol
                                  select new
                                  {
                                      Nombre = u.Nombre,
                                      Apellido = u.Apellido,
                                      Correo = u.Correo,
                                      Estado = u.Estado,
                                      Telefono = u.Telefono,
                                      Rol = r.NombreRol
                                      
                                  }).ToList();

            ViewData["listadoDeRol"] = listaDeRol;

            return View();
        }

        //METODO PARA CREAR UN NUEVO USUARIO
        public IActionResult CrearUsuario(Usuario nuevoUsuario)
        {
            _gestorticketsDbContext.Add(nuevoUsuario);
            _gestorticketsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
