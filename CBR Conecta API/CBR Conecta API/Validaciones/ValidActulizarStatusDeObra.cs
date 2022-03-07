using System;
using System.Collections.Generic;
using FluentValidation;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidActulizarStatusDeObra : AbstractValidator<Models.Requests.ActualizarStatusObra>
    {
        public ValidActulizarStatusDeObra()
        {
            RuleFor(x => x.id).NotNull();
            RuleFor(x => x.Status).NotNull().Must(statusvalid);
        }

        private bool statusvalid(string status)
        {
            try
            {
                return status.Equals("Activa") || status.Equals("Finalizada");
            }
            catch
            {
                return false;
            }
        }
    }
}
