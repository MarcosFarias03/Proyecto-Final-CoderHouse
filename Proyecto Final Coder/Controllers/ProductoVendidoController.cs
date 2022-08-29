using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController
    {
        [HttpGet("{idUsuario}")]
        //TRAER PRODUCTOS VENDIDOS MEDIANTE EL ID DEL USUARIO
        public List<Producto> GetProductosVendidos(int idUsuario)
        {
            return ProductoVendidoHandler.GetProductosVendidos(idUsuario);
        }
        [HttpPost]
        //CARGAR PRODUCTOS VENDIDOS Y DESCONTAR STOCK EN PRODUCTO
        public bool CargarProductosVendidos([FromBody] PostProductoVendido producto)
        {
            try
            {
                return ProductoVendidoHandler.CargarProductosVendidos(new ProductoVendido
                {
                    Stock = producto.Stock,
                    IdProducto = producto.IdProducto,
                    IdVenta = producto.IdVenta
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
