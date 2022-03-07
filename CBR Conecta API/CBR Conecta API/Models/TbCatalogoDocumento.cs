using System;
using System.Collections.Generic;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class TbCatalogoDocumento
    {
        public TbCatalogoDocumento()
        {
            TbArchivos = new HashSet<TbArchivo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public string Ubicacion { get; set; }
        public string Rubro { get; set; }
        public string Identificador { get; set; }

        public virtual ICollection<TbArchivo> TbArchivos { get; set; }
    }
}
