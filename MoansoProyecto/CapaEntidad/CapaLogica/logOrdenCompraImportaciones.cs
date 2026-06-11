using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
namespace CapaLogica
{
    public class logOrdenCompraImportaciones
    {
        private readonly datOrdenCompraImportaciones _dat = new datOrdenCompraImportaciones();

        public List<entOrdenCompraImportaciones> Listar() => _dat.Listar();
        public entOrdenCompraImportaciones ObtenerPorId(int id) => _dat.ObtenerPorId(id);
        public bool Registrar(entOrdenCompraImportaciones o) => _dat.Registrar(o);
        public bool Actualizar(entOrdenCompraImportaciones o) => _dat.Actualizar(o);
        public bool Deshabilitar(int id) => _dat.Deshabilitar(id);
        public List<entDetalleOrdenCompra> ListarDetalles(int idOrden) => _dat.ListarDetalles(idOrden);
        public bool AgregarDetalle(entDetalleOrdenCompra d) => _dat.AgregarDetalle(d);
        public bool EliminarDetalle(int idDetalle) => _dat.EliminarDetalle(idDetalle);

        /// <summary>
        /// Enviar: sólo si está en Borrador
        /// </summary>
        public (bool ok, string msg) Enviar(int idOrden)
        {
            var o = _dat.ObtenerPorId(idOrden);
            if (o == null) return (false, "Orden no encontrada.");
            if (o.Estado != "Borrador") return (false, "Solo se puede enviar una orden en estado Borrador.");
            if (_dat.ListarDetalles(idOrden).Count == 0)
                return (false, "La orden no tiene productos. Agregue al menos uno.");
            _dat.CambiarEstado(idOrden, "Enviada");
            return (true, "Orden enviada correctamente.");
        }

        /// <summary>
        /// Aprobar: sólo si está Enviada
        /// </summary>
        public (bool ok, string msg) Aprobar(int idOrden)
        {
            var o = _dat.ObtenerPorId(idOrden);
            if (o == null) return (false, "Orden no encontrada.");
            if (o.Estado != "Enviada") return (false, "Solo se puede aprobar una orden en estado Enviada.");
            _dat.CambiarEstado(idOrden, "Aprobada");
            return (true, "Orden aprobada y firmada.");
        }

        /// <summary>
        /// Devolver con observaciones: sólo si está Enviada
        /// </summary>
        public (bool ok, string msg) Devolver(int idOrden, string observaciones)
        {
            var o = _dat.ObtenerPorId(idOrden);
            if (o == null) return (false, "Orden no encontrada.");
            if (o.Estado != "Enviada") return (false, "Solo se puede devolver una orden en estado Enviada.");
            if (string.IsNullOrWhiteSpace(observaciones))
                return (false, "Debe indicar el motivo de la devolución.");
            _dat.CambiarEstado(idOrden, "Devuelta", observaciones);
            return (true, "Orden devuelta con observaciones.");
        }
    }
}
