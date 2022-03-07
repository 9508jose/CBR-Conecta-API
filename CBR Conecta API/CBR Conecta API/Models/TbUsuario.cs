using System;
using System.Collections.Generic;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbArchivos = new HashSet<TbArchivo>();
            TbObraAsignada = new HashSet<TbObraAsignadum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Privilegios { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Puesto { get; set; }
        public string Departamento { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TbArchivo> TbArchivos { get; set; }
        public virtual ICollection<TbObraAsignadum> TbObraAsignada { get; set; }
    }
}
