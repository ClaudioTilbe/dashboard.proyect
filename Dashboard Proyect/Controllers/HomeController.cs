using Dashboard_Proyect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard_Proyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        

        public IActionResult DashboardVentas()
        {
            List<Venta> listaVentas = new List<Venta>();
            listaVentas = Venta.GenerarDatosDePrueba(100);


            return View(listaVentas);
        }

        public IActionResult DashboardVentasRT()
        {
            return View();
        }

        public IActionResult DashboardVentasDinamico()
        {
            List<Venta> listaVentas = new List<Venta>();
            listaVentas = Venta.GenerarDatosDePrueba(100);


            return View(listaVentas);
        }







    }
}
