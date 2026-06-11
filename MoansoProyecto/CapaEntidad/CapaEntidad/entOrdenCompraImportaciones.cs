using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entOrdenCompraImportaciones
    {
        public int IdOrden { get; set; }
        public string NumeroOrden { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int IdCotizacion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public string Observaciones { get; set; }

        // Estado: "Borrador", "Enviada", "Aprobada", "Devuelta"
        public string Estado { get; set; }

        public string ObservacionesAprobador { get; set; }
        public bool Activo { get; set; }
    }
}
