using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.IO;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace CBR_Conecta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("permitir")]
    public class ArchivosController : ControllerBase
    {
        [HttpPost("subirPlantilla")]
        public ActionResult subirPlantilla([FromForm] Models.Requests.SubirPlatillaRequest model)
        {
            try
            {
                var validacion = new Validaciones.ValidSubirPlantilla();
                ValidationResult result = validacion.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
               
                        var filePath = "C:\\archivosCBRConecta\\" + model.archivo.FileName;
                        Files.CrearFile(model.archivo, filePath);
                        Models.TbCatalogoDocumento document = new Models.TbCatalogoDocumento();
                        document.Nombre = model.archivo.FileName;
                        document.Extension = Path.GetExtension(model.archivo.FileName).Substring(1);
                        document.Size = Math.Round((double.Parse(model.archivo.Length.ToString()) / 1000000), 2);
                        document.Ubicacion = filePath;
                        document.Rubro = model.rubro;
                        document.Identificador = model.id;
                        db.TbCatalogoDocumentos.Add(document);
                        db.SaveChanges();
                    }
                    return Ok(1);
                }
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("SubirArchivo")]
        public ActionResult SubirArchivo([FromForm] Models.Requests.SubirArchivoRequests model)
        {
            try
            {
                var validacion = new Validaciones.VAlidSubirArchivo();
                ValidationResult result = validacion.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        var filePath = "C:\\archivosCBRConecta\\" + model.archivo.FileName;
                        Files.CrearFile(model.archivo, filePath);
                        Models.TbArchivo document = new Models.TbArchivo();
                        document.Nombre = model.archivo.FileName;
                        document.Extension = Path.GetExtension(model.archivo.FileName).Substring(1);
                        document.Size = Math.Round((double.Parse(model.archivo.Length.ToString()) / 1000000), 2);
                        document.Ubicacion = filePath;
                        document.Rubro = model.rubro;
                        document.Usuario = model.usauario;
                        document.Obra = model.obra;
                        db.TbArchivos.Add(document);
                        db.SaveChanges();
                    }
                }
                return Ok(1);
        }
        catch (Exception e)
        {
        return BadRequest(e);
    }

}

        [HttpPost("ObtenerArchivo")]
        public ActionResult ObtenerArchivo([FromBody] Models.Requests.ObtenerArchivoRequests modelo)
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
                        Models.TbCatalogoDocumento archivo = new Models.TbCatalogoDocumento();
                        var Archivo = (from d in db.TbCatalogoDocumentos where modelo.id == d.Id select d).ToList();
                        if (Archivo.Count() > 0)
                        {
                            string path = Archivo[0].Ubicacion;
                            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                var reader = new BinaryReader(stream);
                                return Ok(reader.ReadBytes((int)stream.Length));
                            }

                        }
                        else
                        {
                            return Ok(null);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("ObtenerArchivoObra")]
        public ActionResult ObtenerArchivoObra([FromBody] Models.Requests.ObtenerArchivoRequests modelo)
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
                        Models.TbArchivo archivo = new Models.TbArchivo();
                        var Archivo = (from d in db.TbArchivos where modelo.id == d.Id select d).ToList();
                        if (Archivo.Count() > 0)
                        {
                            string path = Archivo[0].Ubicacion;
                            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                var reader = new BinaryReader(stream);
                                return Ok(reader.ReadBytes((int)stream.Length));
                            }
                        }
                        else
                        {
                            return Ok(null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpGet("ObtenerCatalogo")]
        public ActionResult ObtenerCatalogo()
        {
            try
            {
                using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                {
                    var catalogo = (from d in db.TbCatalogoDocumentos select d).ToList();
                    if (catalogo.Count() > 0)
                    {
                        return Ok(catalogo);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        
        [HttpPut("ActualizarArchivo")]
        public ActionResult ActualizarArchivo([FromForm] Models.Requests.ActualizarArchivosRequests model)
        {
            try
            {
                var validacion = new Validaciones.ValidActualizarArchivo();
                ValidationResult result = validacion.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(Errores.errors(result));
                }
                else
                {
                    using (Models.CBR_ConectaContext db = new Models.CBR_ConectaContext())
                    {
                        Models.TbCatalogoDocumento oCatalogo = db.TbCatalogoDocumentos.Find(model.idArchivo);
                        var filePath = "C:\\archivosCBRConecta\\" + model.archivo.FileName;
                        Files.CrearFile(model.archivo, filePath); ;
                        oCatalogo.Nombre = model.archivo.FileName;
                        oCatalogo.Extension = Path.GetExtension(model.archivo.FileName).Substring(1);
                        oCatalogo.Size = Math.Round((double.Parse(model.archivo.Length.ToString()) / 1000000), 2);
                        oCatalogo.Ubicacion = filePath;
                        oCatalogo.Rubro = model.rubro;
                        oCatalogo.Identificador = model.id;
                        db.Entry(oCatalogo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();
                        return Ok(1);
                    }
                }                   
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
