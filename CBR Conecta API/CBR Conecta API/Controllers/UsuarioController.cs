using FluentValidation.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBR_Conecta_API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("permitir")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("Log")]
        public async Task<ActionResult> Log([FromBody] Models.Requests.LoginRequests user)
        {
            var validacion = new Validaciones.ValidLogin();
            ValidationResult result = validacion.Validate(user);
            if (!result.IsValid)
            {
                return BadRequest(Errores.errors(result));
            }
            else
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var Usuario =  (from d in db.TbUsuarios where user.usuario == d.Usuario && Encrypt.ComputeSha256Hash(user.Contraseña) == d.Contraseña select d).ToList();
                    return (Usuario.Count() > 0) ? Ok(Usuario) : NotFound();
                }
            }
               
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult> PostUser([FromBody] Models.Requests.UsuarioRequests user)
        {
            var validacion = new Validaciones.ValidUsuario();
            ValidationResult result = validacion.Validate(user);

            if(!result.IsValid)
            {
                return BadRequest(Errores.errors(result));
            }
            else
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    Models.TbUsuario usuario = new Models.TbUsuario();
                    usuario.Nombre = user.Nombre;
                    usuario.Apellido = user.Apellido;
                    usuario.Privilegios = user.Privilegios;
                    usuario.Usuario = user.Usuario;
                    usuario.Contraseña = Encrypt.ComputeSha256Hash(user.Contraseña);
                    usuario.Puesto = user.Puesto;
                    usuario.Departamento = user.Departamento;
                    usuario.Email = user.Email;
                    db.TbUsuarios.Add(usuario);
                    db.SaveChanges();
                    return Ok(1);
                }
            }
           
        }
        
        [HttpPut("ActualizarPassword")]
        public ActionResult ActualizarPassword()
        {
            //TO DO
            return Ok();
        }


        [HttpPost("ObrasUsuario")]
        public ActionResult ObrasUsuario([FromBody] Models.Requests.ObtenerArchivoRequests modelo)
        {
            var validacion = new Validaciones.ValidObtenerArchvios();
            ValidationResult result = validacion.Validate(modelo);
            try
            {
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        Models.TbObraAsignadum obraAsignada = new Models.TbObraAsignadum();
                        var obrasUsuario = (from d in db.TbObraAsignada
                                           join b in db.TbObras
                                           on d.IdObra equals b.Id
                                           where d.IdUsuario == modelo.id
                                           select new { b.Id, b.Nombre}).ToList();
                        return Ok(obrasUsuario);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("ObtenerEmpleados")]
        public ActionResult ObtenerEmpleados()
        {
            try
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var usuarios = (from d in db.TbUsuarios where d.Privilegios == 2 select d).ToList();
                    if (usuarios.Count() > 0)
                    {
                        return Ok(usuarios);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
