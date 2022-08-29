using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet]
        //TRAER VENTAS//
        public List<Producto> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
        [HttpPost]
        //CARGAR VENTAS//
        public bool CargarVentas([FromBody] PostVenta venta)
        {
            try
            {
                return VentaHandler.CargarVentas(new Venta
                {
                    Comentarios = venta.Comentarios,
                    IdUsuario = venta.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpDelete]
        //ELIMINAR VENTAS//
        public bool EliminarVentas([FromBody] int id)
        {
            try
            {
                return VentaHandler.EliminarVentas(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
