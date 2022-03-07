using FluentValidation;
using System.Linq;
namespace CBR_Conecta_API.Validaciones
{
    public class ValidAsignarObra : AbstractValidator<Models.Requests.AsignarObraRequests>
    {
        private int idUsuario;
        public ValidAsignarObra(int _usuario)
        {
            idUsuario = _usuario;
            RuleFor(x => x.obra).NotNull().Must(UsuarioEnObra);
            RuleFor(x => x.usuario).NotNull();
        }

        public bool UsuarioEnObra(int id)
        {
            using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
            {
                var resultQuery = (from d in db.TbObraAsignada where d.IdObra == id select d).ToList();
                return resultQuery.Exists(x=> x.IdUsuario == idUsuario) ?  true : false;
            }
               
        }
    }
}
