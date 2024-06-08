using GestorTickets.Models;
using Microsoft.AspNetCore.Mvc;

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
