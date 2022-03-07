using Microsoft.AspNetCore.Http;
using System;

namespace CBR_Conecta_API.Models.Requests
{
    public class CrearObraRequests
    {
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public string NoProyecto  { get; set; }
        public IFormFile archivo { get; set; }
        public string Status { get; set; }
    }
}
