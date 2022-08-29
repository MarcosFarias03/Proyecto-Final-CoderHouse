using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NombreController
    {
        [HttpGet]
        //Traer nombre de la API
        public string GetNombre()
        {
            return "Proyecto Final";
        }
    }
}
