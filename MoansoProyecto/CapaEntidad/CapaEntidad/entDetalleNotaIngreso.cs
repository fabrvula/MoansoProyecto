using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entDetalleNotaIngreso
    {
        public int IdDetalle { get; set; }
        public int IdNota { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string UnidadMedida { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal Diferencia => CantidadRecibida - CantidadSolicitada;
        // "Bueno", "Dañado", "Incompleto"
        public string CondicionProducto { get; set; }
    }
}
