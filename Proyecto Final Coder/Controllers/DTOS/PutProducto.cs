namespace ProyectoFinal.Controllers.DTOS
{
    public class PutProducto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Costo { get; set; }
        public int PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

    }
}
