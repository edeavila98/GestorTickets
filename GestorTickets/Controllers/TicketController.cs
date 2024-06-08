using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace GestorTickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly gestorticketsDbContext _gestorticketsDbContext;

        public TicketController(gestorticketsDbContext gestorticketsDbContext)
        {
            _gestorticketsDbContext = gestorticketsDbContext;
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var datosUsuario = JsonSerializer.Deserialize<Usuario>(HttpContext.Session.GetString("user"));
                ViewBag.IdUsuario = datosUsuario.IdUsuario;
                ViewBag.NombreUsuario = datosUsuario.Nombre;
                ViewBag.IdRol = datosUsuario.IdRol;
            }

            //Listar las categorias
            var listaDeCategorias = (from c in _gestorticketsDbContext.Categoria
                                  select c).ToList();
            ViewData["listadoDeCategorias"] = new SelectList(listaDeCategorias, "IdCategoria","NombreCategoria");

            //Listar los empleados
            var listaDeEmpleados = (from e in _gestorticketsDbContext.Usuario where e.IdRol == 1
                                     select e).ToList();
            ViewData["listadoDeEmpleados"] = new SelectList(listaDeEmpleados, "IdUsuario", "Nombre");

            //Listar los clientes
            var listaDeClientes = (from c in _gestorticketsDbContext.Usuario where c.IdUsuario == 2
                                    select c).ToList();
            ViewData["listadoDeClientes"] = new SelectList(listaDeClientes, "IdUsuario", "Nombre");

            //Listar las prioridades
            var listaDePrioridad = (from p in _gestorticketsDbContext.Prioridad
                                   select p).ToList();
            ViewData["listadoDePrioridad"] = new SelectList(listaDePrioridad, "IdPrioridad", "NombrePrioridad");

            //Listar los estados
            var listaDeEstados = (from e in _gestorticketsDbContext.Estado
                                   select e).ToList();
            ViewData["listadoDeEstados"] = new SelectList(listaDeEstados, "IdEstado", "NombreEstado");

            //Listar los tickets
            var listaDeTickets = (from t in  _gestorticketsDbContext.Tickets
                                  join c in _gestorticketsDbContext.Categoria on t.IdCategoria equals c.IdCategoria
                                  join e in _gestorticketsDbContext.Usuario on t.IdEmpleado equals e.IdUsuario
                                  join cl in _gestorticketsDbContext.Usuario on t.IdCliente equals cl.IdUsuario
                                  join es in _gestorticketsDbContext.Estado on t.IdEstado equals es.IdEstado
                                  join p in _gestorticketsDbContext.Prioridad on t.IdPrioridad equals p.IdPrioridad
                                  select new
                                  {
                                      Nombre = t.Nombre,
                                      Descripcion = t.Descripcion,
                                      CategoriaNombre = c.NombreCategoria,
                                      EmpleadoNombre = e.Nombre,
                                      ClienteNombre = cl.Nombre,
                                      EstadoNombre = es.NombreEstado,
                                      PrioridadNombre = p.NombrePrioridad,
                                      IdUsuario = cl.IdUsuario
                                  }).ToList();

            ViewData["listadoDeTickets"] = listaDeTickets;

            

            return View();
        }

        //METODO PARA CREAR UN NUEVO TICKET
        public IActionResult CrearTicket(Tickets nuevoTicket)
        {
            _gestorticketsDbContext.Add(nuevoTicket);
            _gestorticketsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult GestionarTicket()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var datosUsuario = JsonSerializer.Deserialize<Usuario>(HttpContext.Session.GetString("user"));
                ViewBag.IdUsuario = datosUsuario.IdUsuario;
                ViewBag.NombreUsuario = datosUsuario.Nombre;
                ViewBag.IdRol = datosUsuario.IdRol;
            }

            //Listar las categorias
            var listaDeCategorias = (from c in _gestorticketsDbContext.Categoria
                                     select c).ToList();
            ViewData["listadoDeCategorias"] = new SelectList(listaDeCategorias, "IdCategoria", "NombreCategoria");

            //Listar los empleados
            var listaDeEmpleados = (from e in _gestorticketsDbContext.Usuario
                                    where e.IdRol == 1
                                    select e).ToList();
            ViewData["listadoDeEmpleados"] = new SelectList(listaDeEmpleados, "IdUsuario", "Nombre");

            //Listar los clientes
            var listaDeClientes = (from c in _gestorticketsDbContext.Usuario
                                   where c.IdUsuario == 2
                                   select c).ToList();
            ViewData["listadoDeClientes"] = new SelectList(listaDeClientes, "IdUsuario", "Nombre");

            //Listar las prioridades
            var listaDePrioridad = (from p in _gestorticketsDbContext.Prioridad
                                    select p).ToList();
            ViewData["listadoDePrioridad"] = new SelectList(listaDePrioridad, "IdPrioridad", "NombrePrioridad");

            //Listar los estados
            var listaDeEstados = (from e in _gestorticketsDbContext.Estado
                                  select e).ToList();
            ViewData["listadoDeEstados"] = new SelectList(listaDeEstados, "IdEstado", "NombreEstado");

            //Listar los tickets
            var listaDeTickets = (from t in _gestorticketsDbContext.Tickets
                                  join c in _gestorticketsDbContext.Categoria on t.IdCategoria equals c.IdCategoria
                                  join e in _gestorticketsDbContext.Usuario on t.IdEmpleado equals e.IdUsuario
                                  join cl in _gestorticketsDbContext.Usuario on t.IdCliente equals cl.IdUsuario
                                  join es in _gestorticketsDbContext.Estado on t.IdEstado equals es.IdEstado
                                  join p in _gestorticketsDbContext.Prioridad on t.IdPrioridad equals p.IdPrioridad
                                  select new
                                  {
                                      Nombre = t.Nombre,
                                      Descripcion = t.Descripcion,
                                      CategoriaNombre = c.NombreCategoria,
                                      EmpleadoNombre = e.Nombre,
                                      ClienteNombre = cl.Nombre,
                                      EstadoNombre = es.NombreEstado,
                                      PrioridadNombre = p.NombrePrioridad,
                                      IdUsuario = cl.IdUsuario
                                  }).ToList();

            ViewData["listadoDeTickets"] = listaDeTickets;

            return View();
        }
    }
}
