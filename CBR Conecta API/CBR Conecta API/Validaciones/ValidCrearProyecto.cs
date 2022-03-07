using System;
using System.IO;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidCrearProyecto : AbstractValidator<Models.Requests.CrearObraRequests>
    {
        public ValidCrearProyecto()
        {
            RuleFor(x => x.archivo).NotNull().Must(ArchivoExtension);
            RuleFor(x => x.nombre).NotEmpty();
            RuleFor(x => x.fecha).NotNull();
            RuleFor(x => x.NoProyecto).NotNull();
            RuleFor(x => x.Status).NotNull().Must(StatusVald);

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
        private bool StatusVald(string status) => (status.Equals("Activa") || status.Equals("Finalizada"));
    }
}
