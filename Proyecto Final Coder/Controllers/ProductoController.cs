using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpDelete("{idProducto}")]
        //ELIMINAR PRODUCTO//
        public bool EliminarProducto([FromBody] int idProducto)
        {
            return ProductoHandler.EliminarProducto(idProducto);
        }

        [HttpPost]
        //CREAR PRODUCTO//
        public bool InsertarProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.InsertarProducto(new Producto
                {
                    Descripcion = producto.Descripcion,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        
        [HttpPut]
        //MODIFICAR PRODUCTO//
        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.ModificarProducto(new Producto
                {
                    Id = producto.Id,
                    Descripcion = producto.Descripcion,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpGet("{idUsuario}")]
        //OBTENER PRODUCTO MEDIANTE EL ID DEL USUARIO//
        public List<Producto> GetProductos(int idUsuario)
        {
            return ProductoHandler.GetProductos(idUsuario);
        }
    }
}
