using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorTickets.Controllers
{
    public class TareaController : Controller
    {
        private readonly gestorticketsDbContext _gestorticketsDbContext;

        public TareaController(gestorticketsDbContext gestorticketsDbContext)
        {
            _gestorticketsDbContext = gestorticketsDbContext;
        }
        public IActionResult Index()
        {
            //Listar los tickets
            var listaDeTicket = (from t in _gestorticketsDbContext.Tickets
                                    select t).ToList();
            ViewData["listadoDeTickt"] = new SelectList(listaDeTicket, "IdTicket", "Nombre");

            //Listar los empleados
            var listaDeEmpleados = (from e in _gestorticketsDbContext.Usuario
                                    where e.IdRol == 1
                                    select e).ToList();
            ViewData["listadoDeEmpleados"] = new SelectList(listaDeEmpleados, "IdUsuario", "Nombre");

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

        //METODO PARA ASIGNAR UNA TAREA
        public IActionResult AsignarTarea(Tickets ticketActualizado)
        {
            // Obtener el ticket existente de la base de datos usando el Id del ticketActualizado
            var ticketExistente = _gestorticketsDbContext.Tickets.Find(ticketActualizado.IdTicket);

            if (ticketExistente == null)
            {
                // Manejar el caso donde el ticket no se encuentra
                return NotFound();
            }

            // Actualizar las propiedades del ticket existente con los valores del ticket actualizado
            ticketExistente.IdEmpleado = ticketActualizado.IdEmpleado;

            // Guardar los cambios en la base de datos
            _gestorticketsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
