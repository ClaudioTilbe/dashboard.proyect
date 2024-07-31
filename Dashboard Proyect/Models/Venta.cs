namespace Dashboard_Proyect.Models
{
    public class Venta
    {

        public DateTime Fecha { get; set; }
        public int Monto { get; set; }
        public string CategoriaProducto { get; set; }
        public string Region { get; set; }
        public string NombreProducto { get; set; }


        public Venta(DateTime fecha, int monto, string categoriaProducto, string region, string nombreProducto)
        {
            Fecha = fecha;
            Monto = monto;
            CategoriaProducto = categoriaProducto;
            Region = region;
            NombreProducto = nombreProducto;
        }




        private static readonly string[] Categorias = { "Electronica", "Ropa", "Alimentos", "Juguetes" };
        private static readonly string[] Regiones = { "Norte", "Sur", "Este", "Oeste" };
        private static readonly string[] NombresProductos = { "Producto A", "Producto B", "Producto C", "Producto D", "Producto E", "Producto F", "Producto G", "Producto H", "Producto I", "Producto J", "Producto K", "Producto L" };


        public static List<Venta> GenerarDatosDePrueba(int cantidad)
        {
            var random = new Random();
            var ventas = new List<Venta>();

            while (ventas.Count < cantidad)
            {

                for (int i = 0; i < cantidad; i++)
                {
                    //Obtengo numero entre 1 y 5
                    int x = random.Next(1, 6);

                    var fecha = new DateTime(2024, random.Next(1, 13), random.Next(1, 29));
                    var monto = random.Next(100, 9000);
                    var categoriaProducto = Categorias[random.Next(Categorias.Length)];
                    var region = Regiones[random.Next(Regiones.Length)];
                    var nombreProducto = NombresProductos[random.Next(NombresProductos.Length)];

                    nombreProducto = nombreProducto + i.ToString();

                    for (int j = 0; j < x; j++)
                    {
                        //Mientras haya menos de la cantidad solicitada seguira agregando
                        if (ventas.Count <= (cantidad - 1))
                        {
                            ventas.Add(new Venta(fecha, monto, categoriaProducto, region, nombreProducto));
                        }
                    }
                }
            }

            return ventas;
        }




    }
}
