using System;
using System.Collections.Generic;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class TbObra
    {
        public TbObra()
        {
            TbArchivos = new HashSet<TbArchivo>();
            TbObraAsignada = new HashSet<TbObraAsignadum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string Destajo { get; set; }
        public string Documento { get; set; }
        public string Estatus { get; set; }

        public virtual ICollection<TbArchivo> TbArchivos { get; set; }
        public virtual ICollection<TbObraAsignadum> TbObraAsignada { get; set; }
    }
}
