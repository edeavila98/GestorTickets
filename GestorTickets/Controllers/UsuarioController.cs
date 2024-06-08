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
                                      ID = u.IdUsuario,
                                      Nombre = u.Nombre,
                                      Apellido = u.Apellido,
                                      Correo = u.Correo,
                                      Estado = u.Estado,
                                      Telefono = u.Telefono,
                                      Rol = r.NombreRol
                                      
                                  }).ToList();

            ViewData["listadoDeRol"] = listaDeRol;


            //Listar usuarios para eliminar
            var listaDeUser = (from us in _gestorticketsDbContext.Usuario
                                select us).ToList();
            ViewData["listadoDeUser"] = new SelectList(listaDeUser, "IdUsuario", "IdUsuario");

            return View();
        }

        //METODO PARA CREAR UN NUEVO USUARIO
        public IActionResult CrearUsuario(Usuario nuevoUsuario)
        {
            _gestorticketsDbContext.Add(nuevoUsuario);
            _gestorticketsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }



        //METODO PARA ELIMINAR UN USUARIO
        public IActionResult EliminarUsuario(int idUsuario)
        {
            // Obtener el usuario existente de la base de datos usando el Id del usuario
            var UsuarioExistente = _gestorticketsDbContext.Usuario.Find(idUsuario);

            if (UsuarioExistente == null)
            {
                // Manejar el caso donde el usuario no se encuentra
                return NotFound();
            }

            // Eliminar el usuario de la base de datos
            _gestorticketsDbContext.Usuario.Remove(UsuarioExistente);

            // Guardar los cambios en la base de datos
            _gestorticketsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        //METODO PARA ACTUALIZAR UN USUARIO
        public IActionResult ModificarUsuario(Usuario usuarioActualizado)
        {
            // Obtener el usuario existente de la base de datos usando el Id del usuarioActualizado
            var usuarioExistente = _gestorticketsDbContext.Usuario.Find(usuarioActualizado.IdUsuario);

            if (usuarioExistente == null)
            {
                // Manejar el caso donde el usuario no se encuentra
                return NotFound();
            }

            // Actualizar las propiedades del usuario existente con los valores del usuario actualizado
            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Apellido = usuarioActualizado.Apellido;
            usuarioExistente.Correo = usuarioActualizado.Correo;
            usuarioExistente.Clave = usuarioActualizado.Clave;
            usuarioExistente.Estado = usuarioActualizado.Estado;
            usuarioExistente.Telefono = usuarioActualizado.Telefono;
            usuarioExistente.IdRol = usuarioActualizado.IdRol;

            // Guardar los cambios en la base de datos
            _gestorticketsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
