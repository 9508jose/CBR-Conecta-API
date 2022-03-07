using Microsoft.AspNetCore.Http;

namespace CBR_Conecta_API.Models.Requests
{
    public class SubirArchivoRequests
    {
        public IFormFile archivo { get; set; }
        public int rubro { get; set; }
        public int usauario { get; set; }
        public int obra { get; set; }
    }
}
