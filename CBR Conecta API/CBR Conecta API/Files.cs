using Microsoft.AspNetCore.Http;
using System.IO;

namespace CBR_Conecta_API
{
    public static class Files
    {
        public static async void CrearFile(IFormFile model, string filePath)
        {
            if (!Directory.Exists("C:\\archivosCBRConecta\\"))
            {
                Directory.CreateDirectory("C:\\archivosCBRConecta\\");
            }
            using (var stream = File.Create(filePath))
            {
                await model.CopyToAsync(stream);
            }
        }
        public static void BorrarFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
