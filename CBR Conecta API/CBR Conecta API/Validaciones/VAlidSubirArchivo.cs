using System;
using System.IO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CBR_Conecta_API.Validaciones
{
    public class VAlidSubirArchivo : AbstractValidator<Models.Requests.SubirArchivoRequests>
    {
        public VAlidSubirArchivo()
        {
            RuleFor(x => x.archivo).NotNull().Must(ArchivoExtension);
            RuleFor(x => x.rubro).NotEmpty();
            RuleFor(x => x.obra).NotNull();
            RuleFor(x => x.usauario).NotNull();

        }

        private bool ArchivoExtension(IFormFile archivo)
        {
            try
            {
                return (Path.GetExtension(archivo.FileName)[1..].Equals("pdf") || Path.GetExtension(archivo.FileName)[1..].Equals("xlsx") || Path.GetExtension(archivo.FileName)[1..].Equals("docx"));
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
