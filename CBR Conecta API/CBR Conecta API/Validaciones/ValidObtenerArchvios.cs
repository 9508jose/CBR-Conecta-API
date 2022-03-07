using FluentValidation;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidObtenerArchvios : AbstractValidator<Models.Requests.ObtenerArchivoRequests>
    {
        public ValidObtenerArchvios()
        {
            RuleFor(x => x.id).NotNull();
        }
    }
}
