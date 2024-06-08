using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using GestorTickets.Services;
using System.Linq;

namespace GestorTickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly gestorticketsDbContext _gestorticketsDbContext;
        private readonly correo _correoService;

        public TicketController(gestorticketsDbContext gestorticketsDbContext, correo correoService)
        {
            _gestorticketsDbContext = gestorticketsDbContext;
            _correoService = correoService;
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
            ViewData["listadoDeCategorias"] = new SelectList(listaDeCategorias, "IdCategoria", "NombreCategoria");

            //Listar los empleados
            var listaDeEmpleados = (from e in _gestorticketsDbContext.Usuario where e.IdRol == 1
                                    select e).ToList();
            ViewData["listadoDeEmpleados"] = new SelectList(listaDeEmpleados, "IdUsuario", "Nombre");

            //Listar los clientes
            var listaDeClientes = (from c in _gestorticketsDbContext.Usuario where c.IdRol == 2
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

        //METODO PARA CREAR UN NUEVO TICKET
        public IActionResult CrearTicket(Tickets nuevoTicket)
        {
            _gestorticketsDbContext.Add(nuevoTicket);
            _gestorticketsDbContext.SaveChanges();
            var ticketDetalles = _gestorticketsDbContext.Tickets
                               .Where(t => t.IdTicket == nuevoTicket.IdTicket)
                               .Select(t => new
                               {
                                   t.Nombre,
                                   t.Descripcion,
                                   t.Cliente.Correo,
                                   ClienteNombre = t.Cliente.Nombre,
                                   CategoriaNombre = t.Categoria.NombreCategoria,
                                   PrioridadNombre = t.Prioridad.NombrePrioridad,
                                   EstadoNombre = t.Estado.NombreEstado
                               })
                               .FirstOrDefault();

            if (ticketDetalles != null)
            {
                string destinatario = ticketDetalles.Correo;
                string asunto = "Nuevo Ticket Creado: " + ticketDetalles.Nombre;
                string cuerpo = $"Hola {ticketDetalles.ClienteNombre},\n\n" +
                                $"Se ha creado un nuevo ticket con los siguientes detalles:\n" +
                                $"Nombre: {ticketDetalles.Nombre}\n" +
                                $"Descripción: {ticketDetalles.Descripcion}\n" +
                                $"Categoría: {ticketDetalles.CategoriaNombre}\n" +
                                $"Prioridad: {ticketDetalles.PrioridadNombre}\n" +
                                $"Estado: {ticketDetalles.EstadoNombre}\n\n" +
                                $"Saludos,\n" +
                                $"Equipo de Soporte";

                _correoService.enviar(destinatario, asunto, cuerpo);
            }
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

            //Listar los tickets
            var listaDeTicket1 = (from t in _gestorticketsDbContext.Tickets
                                  select t).ToList();
            ViewData["listadoDeTicket1"] = new SelectList(listaDeTicket1, "IdTicket", "IdTicket");

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
                                      ID = t.IdTicket,
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

        //METODO PARA ACTUALIZAR UN TICKET
        public IActionResult ModificarTicket(Tickets ticketActualizado)
        {
            // Obtener el ticket existente de la base de datos usando el Id del ticketActualizado
            var ticketExistente = _gestorticketsDbContext.Tickets.Find(ticketActualizado.IdTicket);

            if (ticketExistente == null)
            {
                // Manejar el caso donde el ticket no se encuentra
                return NotFound();
            }

            // Actualizar las propiedades del ticket existente con los valores del ticket actualizado
            ticketExistente.Nombre = ticketActualizado.Nombre;
            ticketExistente.IdCategoria = ticketActualizado.IdCategoria;
            ticketExistente.Descripcion = ticketActualizado.Descripcion;
            ticketExistente.IdEmpleado = ticketActualizado.IdEmpleado;
            ticketExistente.IdCliente = ticketActualizado.IdCliente;
            ticketExistente.IdPrioridad = ticketActualizado.IdPrioridad;
            ticketExistente.IdEstado = ticketActualizado.IdEstado;

            // Guardar los cambios en la base de datos
            _gestorticketsDbContext.SaveChanges();

            return RedirectToAction("GestionarTicket");
        }



        //METODO PARA ELIMINAR UN TICKET

        public IActionResult EliminarTicket(int idTicket)
        {
            // Obtener el ticket existente de la base de datos usando el Id del ticket
            var ticketExistente = _gestorticketsDbContext.Tickets.Find(idTicket);

            if (ticketExistente == null)
            {
                // Manejar el caso donde el ticket no se encuentra
                return NotFound();
            }

            // Eliminar el ticket de la base de datos
            _gestorticketsDbContext.Tickets.Remove(ticketExistente);

            // Guardar los cambios en la base de datos
            _gestorticketsDbContext.SaveChanges();

            return RedirectToAction("GestionarTicket");
        }
    }
}
