using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entIncidencia
    {
        public int IdIncidencia { get; set; }
        public int IdNota { get; set; }
        public string NumeroNota { get; set; }
        public DateTime FechaIncidencia { get; set; }
        // "Diferencia de Cantidad", "Producto Dañado", "Ambos"
        public string TipoIncidencia { get; set; }
        public string Descripcion { get; set; }
        public string AccionTomada { get; set; }
        // "Pendiente", "En Proceso", "Resuelta"
        public string Estado { get; set; }
    }
}
