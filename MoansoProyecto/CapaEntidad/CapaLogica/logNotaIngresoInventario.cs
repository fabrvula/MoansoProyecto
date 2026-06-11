using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logNotaIngresoInventario
    {
        private readonly datNotaIngresoAlmacen _dat = new datNotaIngresoAlmacen();

        public List<entNotaIngresoInventario> Listar() => _dat.Listar();
        public entNotaIngresoInventario ObtenerPorId(int id) => _dat.ObtenerPorId(id);
        public bool Registrar(entNotaIngresoInventario n) => _dat.Registrar(n);
        public bool Actualizar(entNotaIngresoInventario n) => _dat.Actualizar(n);
        public bool Deshabilitar(int id) => _dat.Deshabilitar(id);
        public List<entOrdenCompraImportaciones> ListarOrdenesAprobadas() => _dat.ListarOrdenesAprobadas();
        public List<entDetalleNotaIngreso> ListarDetalles(int idNota) => _dat.ListarDetalles(idNota);
        public bool AgregarDetalle(entDetalleNotaIngreso d) => _dat.AgregarDetalle(d);
        public bool EliminarDetalle(int id) => _dat.EliminarDetalle(id);
        public List<entIncidencia> ListarIncidencias(int idNota) => _dat.ListarIncidencias(idNota);
        public bool RegistrarIncidencia(entIncidencia i) => _dat.RegistrarIncidencia(i);
        public bool ActualizarIncidencia(entIncidencia i) => _dat.ActualizarIncidencia(i);

        /// <summary>
        /// Confirmar ingreso: valida que todos los detalles tengan condición asignada.
        /// Si hay diferencias o daños → estado "Con Incidencia", sino "Conforme".
        /// </summary>
        public (bool ok, string msg, bool tieneIncidencia) ConfirmarIngreso(int idNota)
        {
            var nota = _dat.ObtenerPorId(idNota);
            if (nota == null) return (false, "Nota no encontrada.", false);
            if (nota.Estado != "Pendiente") return (false, "Solo se puede confirmar una nota en estado Pendiente.", false);

            var detalles = _dat.ListarDetalles(idNota);
            if (detalles.Count == 0) return (false, "Agregue al menos un producto antes de confirmar.", false);

            bool sinCondicion = detalles.Any(d => string.IsNullOrWhiteSpace(d.CondicionProducto));
            if (sinCondicion) return (false, "Asigne la condición a todos los productos.", false);

            bool hayIncidencia = detalles.Any(d => d.Diferencia != 0 || d.CondicionProducto != "Bueno");

            string nuevoEstado = hayIncidencia ? "Con Incidencia" : "Conforme";
            _dat.CambiarEstado(idNota, nuevoEstado);

            string msg = hayIncidencia
                ? "Ingreso registrado con incidencias. Se recomienda registrar el detalle de la incidencia."
                : "Ingreso confirmado correctamente. Inventario actualizado.";

            return (true, msg, hayIncidencia);
        }
    }
}
