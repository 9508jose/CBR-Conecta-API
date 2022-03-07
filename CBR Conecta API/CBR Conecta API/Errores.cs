using FluentValidation.Results;
using System.Collections.Generic;


namespace CBR_Conecta_API
{
    public static class Errores
    {
        public static List<Models.ErrorModel> errors(ValidationResult result)
        {
            List<Models.ErrorModel> error = new List<Models.ErrorModel>();
            foreach (var errores in result.Errors)
            {
                Models.ErrorModel errorModel = new Models.ErrorModel();
                errorModel.Error = errores.PropertyName;
                errorModel.Mensaje = errores.ErrorMessage;
                error.Add(errorModel);
            }

            return error;
        }
    }
}
