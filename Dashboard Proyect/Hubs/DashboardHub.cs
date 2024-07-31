using Dashboard_Proyect.Models;
using Microsoft.AspNetCore.SignalR;



namespace Dashboard_Proyect.Hubs
{
    public class DashboardHub : Hub
    {
        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();


        //Constructores - Propiedades
        public DashboardHub(IHubContext<DashboardHub> hub)
        {
            Hub = hub;
        }

        private IHubContext<DashboardHub> Hub
        {
            get;
            set;
        }



        //Inicia la transferencia de datos en tiempo real
        public void inicioRT()
        {
            //_timer = new Timer(ActualizoGrafico, null, _intervalo, _intervalo);

            CargoGrafico();
        }



        private void CargoGrafico()
        {
            List<Venta> listaVentas = new List<Venta>();

            if (datosCompartidos.ObtenerVentas("ListaVentas").Count < 1)
            {
                listaVentas = Venta.GenerarDatosDePrueba(20);

                datosCompartidos.SetearVentas("ListaVentas", listaVentas);
            }
            else
            {
                listaVentas = datosCompartidos.ObtenerVentas("ListaVentas");
            }


            var ventasXCategoria = listaVentas
                                     .GroupBy(v => v.CategoriaProducto)
                                     .Select(g => new
                                     {
                                         Categoria = g.Key,
                                         TotalVentas = g.Sum(v => v.Monto)
                                     })
                                     .OrderBy(x => x.Categoria)
                                     .ToList();

            Hub.Clients.All.SendAsync("cargoGrafico", ventasXCategoria);
        }



        //Agregar Venta
        public void AgregarVentas(string buttonId)
        {
            List<Venta> listaAgregar = Venta.GenerarDatosDePrueba(1);

            if (buttonId == "agregarAlimento")
            {
                listaAgregar[0].CategoriaProducto = "Alimentos";
                
            }
            else if (buttonId == "agregarRopa")
            {
                listaAgregar[0].CategoriaProducto = "Ropa";

            }
            else if (buttonId == "agregarJuguetes")
            {
                listaAgregar[0].CategoriaProducto = "Juguetes";

            }
            else if (buttonId == "agregarElectronica")
            {
                listaAgregar[0].CategoriaProducto = "Electronica";

            }

            //Primero obtengo la lista guardada en datosCompartidos para actualizar y volver a guardar
            List<Venta> listaVentas = datosCompartidos.ObtenerVentas("ListaVentas");
            listaVentas.AddRange(listaAgregar);

            datosCompartidos.SetearVentas("ListaVentas", listaVentas);

            var ventasXCategoria = listaAgregar
                                    .GroupBy(v => v.CategoriaProducto)
                                    .Select(g => new
                                    {
                                        Categoria = g.Key,
                                        TotalVentas = g.Sum(v => v.Monto)
                                    })
                                    .OrderBy(x => x.Categoria)
                                    .ToList();

            Hub.Clients.All.SendAsync("agregoVenta", ventasXCategoria);
        }


        /* Este Timer actualizaba el grafico cada un intervalo de tiempo, pero debido a que no era tan visualmente
         * agradable y que la pagina de chart.js recomienda agregar los datos sin refrescar el grafico, opte entonces
         * por quitar este segmento de codigo que bien podria utilizarse para otro tipo de graficos bajo otro contexto
         
        TimeSpan _intervalo = TimeSpan.FromMilliseconds(100000); //2000
        Timer _timer;

        private void ActualizoGrafico(object state)
        {
            EnvioActualizacion();

        }

        private void EnvioActualizacion()
        {
            List<Venta> listaVentas = datosCompartidos.ObtenerVentas("ListaVentas");

            var ventasXCategoria = listaVentas
                                     .GroupBy(v => v.CategoriaProducto)
                                     .Select(g => new
                                     {
                                         Categoria = g.Key,
                                         TotalVentas = g.Sum(v => v.Monto)
                                     })
                                     .OrderBy(x => x.Categoria)
                                     .ToList();

            Hub.Clients.All.SendAsync("actualizoGrafico", ventasXCategoria);
        }
        */

    }
}
