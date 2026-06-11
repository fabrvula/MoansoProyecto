using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entDetalleOrdenCompra
    {
        public int IdDetalle { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string UnidadMedida { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => CantidadSolicitada * PrecioUnitario;
    }
}
