using Microsoft.AspNetCore.Http;

namespace CBR_Conecta_API.Models.Requests
{
    public class SubirPlatillaRequest
    {
        public IFormFile archivo { get; set; }
        public string rubro { get; set; }
        public string id { get; set; }
    }
}
