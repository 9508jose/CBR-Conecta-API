using System;
using System.Collections.Generic;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class TbArchivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public double? Size { get; set; }
        public string Ubicacion { get; set; }
        public int Rubro { get; set; }
        public int Usuario { get; set; }
        public int Obra { get; set; }

        public virtual TbObra ObraNavigation { get; set; }
        public virtual TbCatalogoDocumento RubroNavigation { get; set; }
        public virtual TbUsuario UsuarioNavigation { get; set; }
    }
}
