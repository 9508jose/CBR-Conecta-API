using FluentValidation;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidLogin : AbstractValidator<Models.Requests.LoginRequests>
    {
        public ValidLogin()
        {
            RuleFor(x => x.usuario).NotNull();
            RuleFor(x => x.Contraseña).NotNull();
        }
    }
}
