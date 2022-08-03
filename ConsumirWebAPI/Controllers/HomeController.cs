using ConsumirWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ConsumirWebAPI.Servicios;

namespace ConsumirWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicio_API;

        public HomeController(IServicio_API servicio_API)
        {
            _servicio_API = servicio_API;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _servicio_API.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Producto(int idProducto)
        {
            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if(idProducto != 0)
            {
                modelo_producto = await _servicio_API.Obtener(idProducto);
                ViewBag.Accion = "Nuevo Producto";
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult>GuardarCambios(Producto producto)
        {
            bool respuesta;

            if(producto.idProducto == 0)
                respuesta = await _servicio_API.Guardar(producto);
            else
                respuesta = await _servicio_API.Editar(producto);

            if(respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            var respuesta = await _servicio_API.Eliminar(idProducto);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}