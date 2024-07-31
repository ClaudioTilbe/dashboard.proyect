using Dashboard_Proyect.Models;
using System.Collections.Concurrent;

namespace Dashboard_Proyect.Hubs
{
    public class DatosCompartidos
    {

        //Singleton
        private static DatosCompartidos _instancia = null;
        private DatosCompartidos() { }
        public static DatosCompartidos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosCompartidos();
            return _instancia;
        }



        //Compartir Usuario ====================================================================================
        private readonly ConcurrentDictionary<string, List<Venta>> cdVentas = new ConcurrentDictionary<string, List<Venta>>();

        public void SetearVentas(string nombreVariable, List<Venta> ventas)
        {
            cdVentas[nombreVariable] = ventas;
        }

        public List<Venta> ObtenerVentas(string nombreVariable)
        {
            return cdVentas.GetOrAdd(nombreVariable, _ => new List<Venta>());
        }


    }
}
