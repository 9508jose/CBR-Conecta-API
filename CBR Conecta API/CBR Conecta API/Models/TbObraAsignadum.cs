using System;
using System.Collections.Generic;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class TbObraAsignadum
    {
        public int Id { get; set; }
        public int? IdObra { get; set; }
        public int? IdUsuario { get; set; }

        public virtual TbObra IdObraNavigation { get; set; }
        public virtual TbUsuario IdUsuarioNavigation { get; set; }
    }
}
