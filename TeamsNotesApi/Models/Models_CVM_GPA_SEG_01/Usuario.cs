using System;
using System.Collections.Generic;

namespace TeamsNotesApi.Models.Models_CVM_GPA_SEG_01
{
    public partial class Usuario
    {
        public string CdUsuario { get; set; } = null!;
        public int IdUsuario { get; set; }
        public string DsUsuario { get; set; } = null!;
        public string DsPassword { get; set; } = null!;
        public string DsMail { get; set; } = null!;
        public DateTime? DtPassword { get; set; }
        public bool LgAnulado { get; set; }
        public DateTime DtAlta { get; set; }
        public bool LgLock { get; set; }
        public DateTime? DtLock { get; set; }
        public int QtIntentos { get; set; }
        public DateTime? DtAcceso { get; set; }
        public bool LgCambiarPassword { get; set; }
        public int IdUsuarioAlta { get; set; }
        public string DhAlta { get; set; } = null!;
        public int IdUsuarioModifica { get; set; }
        public DateTime DtModifica { get; set; }
        public string DhModifica { get; set; } = null!;
    }
}
