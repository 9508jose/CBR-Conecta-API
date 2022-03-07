using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation.Results;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;

namespace CBR_Conecta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        [HttpPost("CrearProyecto")]
        public ActionResult CrearProyecto([FromForm] Models.Requests.CrearObraRequests modelo)
        {
            try
            {
                var validacion = new Validaciones.ValidCrearProyecto();
                ValidationResult result = validacion.Validate(modelo);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        var filePath = "C:\\archivosCBRConecta\\" + modelo.archivo.FileName;
                        Files.CrearFile(modelo.archivo, filePath);
                        Models.TbObra obra = new Models.TbObra();
                        obra.Nombre = modelo.nombre;
                        obra.FechaInicio = modelo.fecha;
                        obra.Destajo = modelo.NoProyecto;
                        obra.Documento = filePath;
                        obra.Estatus = modelo.Status;
                        db.TbObras.Add(obra);
                        db.SaveChanges();
                    }
                    return Ok(1);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpPut("EditarStatusProyecto")]
        public ActionResult EditarStatsuProyecto([FromBody] Models.Requests.ActualizarStatusObra model)
        {
            try
            {
                var validacion = new Validaciones.ValidActulizarStatusDeObra();
                ValidationResult result = validacion.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        Models.TbObra obra = db.TbObras.Find(model.id);
                        obra.Estatus = model.Status;
                        db.Entry(obra).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        return Ok(1);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpPost("AsignarTranbajador")]
        public ActionResult AsignarTranbajador([FromBody] Models.Requests.AsignarObraRequests asignar)
        {
            try
            {
                var validacion = new Validaciones.ValidAsignarObra(asignar.usuario);
                ValidationResult result = validacion.Validate(asignar);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        Models.TbObraAsignadum asignacion = new Models.TbObraAsignadum();
                        asignacion.IdUsuario = asignar.usuario;
                        asignacion.IdObra = asignar.obra;
                        db.TbObraAsignada.Add(asignacion);
                        db.SaveChanges();
                        return Ok(1);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("EliminarTrabajador")]
        public ActionResult EliminarTrabajador([FromBody] Models.Requests.AsignarObraRequests eliminar)
        {
            try
            {
                var validacion = new Validaciones.ValidAsignarObra(eliminar.usuario);
                ValidationResult result = validacion.Validate(eliminar);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        var query = (from d in db.TbObraAsignada where eliminar.obra == d.IdObra && eliminar.usuario == d.IdUsuario select d).Single();
                        db.TbObraAsignada.Remove(query);
                        db.SaveChanges();
                        return Ok(1);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           

        }
        [HttpGet("MostarTodasLasObras")]
        public ActionResult MostarTodasLasObras()
        {
            try
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var obras = (from d in db.TbObras select d).ToList();
                    return Ok(obras);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }            
        }
        [HttpGet("MostarObrasActivas")]
        public ActionResult MostarObrasActivas()
        {
            try
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var obras = (from d in db.TbObras where d.Estatus == "Activa" select d).ToList();
                    return Ok(obras);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }
        [HttpGet("MostrarInfoObra")]
        public ActionResult MostrarInfoObra([FromBody] Models.Requests.IdRequests Requests)
        {
            try
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var obras = (from d in db.TbObras where d.Id == Requests.id select d).ToList();
                    return Ok(obras);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("UsuariosEnObtra")]
        public ActionResult UsuariosEnObtra([FromBody] Models.Requests.ObtenerTrabajadoresdeObraRequest model)
        {
            using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
            {
                var Query = (from d in db.TbObraAsignada
                             join b in db.TbUsuarios
                             on d.IdUsuario equals b.Id
                             where d.IdObra == model.id
                             select new { b.Nombre, b.Apellido }).ToList();
                return Ok(Query);
            }
                
        }
        
        [HttpDelete("BorrarArchivos")]
        public async Task<ActionResult> BorrarArchivos([FromBody] Models.Requests.IdRequests model)
        {
            using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
            {
                var Query = (from d in db.TbCatalogoDocumentos where d.Id == model.id select d).ToList();
                foreach (var borrado in Query)
                {
                    Files.BorrarFile(borrado.Ubicacion);
                    db.TbCatalogoDocumentos.Remove(borrado);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        [HttpPost("ArchivosObra")]
        public ActionResult ArchivosObra([FromBody] Models.Requests.IdRequests model)
        {
            using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
            {
                var Query = (from d in db.TbArchivos where d.Obra == model.id select d).ToList();
                return Ok(Query);
            }
        }

    }
}
