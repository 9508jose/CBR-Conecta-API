using FluentValidation;

namespace CBR_Conecta_API.Validaciones
{
    public class ValidUsuario : AbstractValidator<Models.Requests.UsuarioRequests>
    {
        public ValidUsuario()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Privilegios).NotNull().Must(PrivilegiosValidos);
            RuleFor(x => x.Usuario).NotEmpty().MaximumLength(10).MinimumLength(8);
            RuleFor(x => x.Departamento).NotEmpty();
            RuleFor(x => x.Puesto).NotEmpty();
            RuleFor(x => x.Contraseña).NotEmpty().MinimumLength(8);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
        private bool PrivilegiosValidos(int p) => p.Equals(2) || p.Equals(1); 
    }
}
