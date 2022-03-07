﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidSubirPlantilla : AbstractValidator<Models.Requests.SubirPlatillaRequest>
    {
        public ValidSubirPlantilla()
        {
            RuleFor(x => x.archivo).NotNull().Must(ArchivoExtension);
            RuleFor(x => x.rubro).NotEmpty();
            RuleFor(x => x.id).NotEmpty();
        }

        private bool ArchivoExtension(IFormFile archivo)
        {
            try
            {
                return (Path.GetExtension(archivo.FileName)[1..].Equals("pdf") || Path.GetExtension(archivo.FileName)[1..].Equals("xlsx") || Path.GetExtension(archivo.FileName)[1..].Equals("docx"));
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
