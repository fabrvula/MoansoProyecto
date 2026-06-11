using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entNotaIngresoInventario
    {
        public int IdNota { get; set; }
        public string NumeroNota { get; set; }
        public int IdOrdenCompra { get; set; }
        public string NumeroOrden { get; set; }
        public string NombreProveedor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string ResponsableAlmacen { get; set; }
        public string Observaciones { get; set; }
        // "Pendiente", "Conforme", "Con Incidencia"
        public string Estado { get; set; }
        public bool Activo { get; set; }
    }
}
