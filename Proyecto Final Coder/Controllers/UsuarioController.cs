using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Models;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contraseña}")]
        //INICIO DE SESION//
        public Usuario GetUsuarioByContraseña(string nombreUsuario, string contraseña)
        {
            return UsuarioHandler.GetUsuarioByContraseña(nombreUsuario, contraseña);
        }

        [HttpPost]
        //CREAR USUARIO//
        public bool InsertarUsuario([FromBody]PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.InsertarUsuario(new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        
        [HttpPut]
        //MODIFICAR USUARIO//
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            try
            {
                return UsuarioHandler.ModificarUsuario(new Usuario
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpGet("{NombreUsuario}")]
        //TRAER USUARIO MEDIANTE SU NOMBRE DE USUARIO//
        public Usuario GetUsuario(string NombreUsuario)
        {
            return UsuarioHandler.GetUsuario(NombreUsuario);
        }

        [HttpDelete]
        //ELIMINAR USUARIO 
        public bool DeleteUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.DeleteUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
