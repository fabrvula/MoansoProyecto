using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datNotaIngresoAlmacen
    {
        // ── Datos de prueba ──────────────────────────────────────────────────────
        private static List<entNotaIngresoInventario> _notas = new List<entNotaIngresoInventario>
        {
            new entNotaIngresoInventario
            {
                IdNota = 1, NumeroNota = "NI-2026-001", IdOrdenCompra = 1,
                NumeroOrden = "OC-2026-001", NombreProveedor = "AutoParts USA S.A.",
                FechaIngreso = new DateTime(2026, 6, 7),
                ResponsableAlmacen = "Carlos Mendoza",
                Observaciones = "Ingreso sin novedad",
                Estado = "Conforme", Activo = true
            },
            new entNotaIngresoInventario
            {
                IdNota = 2, NumeroNota = "NI-2026-002", IdOrdenCompra = 2,
                NumeroOrden = "OC-2026-002", NombreProveedor = "CarTech China Ltd.",
                FechaIngreso = new DateTime(2026, 6, 8),
                ResponsableAlmacen = "Ana Torres",
                Observaciones = "Se encontraron unidades dañadas",
                Estado = "Con Incidencia", Activo = true
            }
        };

        private static List<entDetalleNotaIngreso> _detalles = new List<entDetalleNotaIngreso>
        {
            new entDetalleNotaIngreso { IdDetalle=1, IdNota=1, NombreProducto="Filtro de aceite", UnidadMedida="Unidad", CantidadSolicitada=100, CantidadRecibida=100, CondicionProducto="Bueno" },
            new entDetalleNotaIngreso { IdDetalle=2, IdNota=1, NombreProducto="Pastilla de freno", UnidadMedida="Par",   CantidadSolicitada=50,  CantidadRecibida=50,  CondicionProducto="Bueno" },
            new entDetalleNotaIngreso { IdDetalle=3, IdNota=2, NombreProducto="Bujía NGK",         UnidadMedida="Unidad",CantidadSolicitada=200, CantidadRecibida=180, CondicionProducto="Incompleto" },
            new entDetalleNotaIngreso { IdDetalle=4, IdNota=2, NombreProducto="Faro LED",           UnidadMedida="Unidad",CantidadSolicitada=20,  CantidadRecibida=18,  CondicionProducto="Dañado" },
        };

        private static List<entIncidencia> _incidencias = new List<entIncidencia>
        {
            new entIncidencia { IdIncidencia=1, IdNota=2, NumeroNota="NI-2026-002", FechaIncidencia=new DateTime(2026,6,8), TipoIncidencia="Ambos", Descripcion="20 bujías faltantes y 2 faros con rotura de cristal", AccionTomada="Contactar proveedor para reclamo", Estado="En Proceso" }
        };

        private static int _nextNota = 3;
        private static int _nextDet = 5;
        private static int _nextInc = 2;

        // ── Órdenes aprobadas disponibles (simulado) ─────────────────────────────
        public List<entOrdenCompraImportaciones> ListarOrdenesAprobadas()
        {
            return new List<entOrdenCompraImportaciones>
            {
                new entOrdenCompraImportaciones { IdOrden=1, NumeroOrden="OC-2026-001", NombreProveedor="AutoParts USA S.A.", Estado="Aprobada" },
                new entOrdenCompraImportaciones { IdOrden=3, NumeroOrden="OC-2026-003", NombreProveedor="AutoParts USA S.A.", Estado="Aprobada" },
            };
        }

        // ── CRUD Notas ────────────────────────────────────────────────────────────
        public List<entNotaIngresoInventario> Listar() =>
            _notas.FindAll(n => n.Activo);

        public entNotaIngresoInventario ObtenerPorId(int id) =>
            _notas.Find(n => n.IdNota == id);

        public bool Registrar(entNotaIngresoInventario nota)
        {
            nota.IdNota = _nextNota++;
            nota.NumeroNota = $"NI-{DateTime.Now.Year}-{_nextNota:D3}";
            nota.Estado = "Pendiente";
            nota.Activo = true;
            _notas.Add(nota);
            return true;
        }

        public bool Actualizar(entNotaIngresoInventario nota)
        {
            var n = _notas.Find(x => x.IdNota == nota.IdNota);
            if (n == null) return false;
            n.ResponsableAlmacen = nota.ResponsableAlmacen;
            n.FechaIngreso = nota.FechaIngreso;
            n.Observaciones = nota.Observaciones;
            return true;
        }

        public bool CambiarEstado(int idNota, string estado)
        {
            var n = _notas.Find(x => x.IdNota == idNota);
            if (n == null) return false;
            n.Estado = estado;
            return true;
        }

        public bool Deshabilitar(int idNota)
        {
            var n = _notas.Find(x => x.IdNota == idNota);
            if (n == null) return false;
            n.Activo = false;
            return true;
        }

        // ── CRUD Detalles ─────────────────────────────────────────────────────────
        public List<entDetalleNotaIngreso> ListarDetalles(int idNota) =>
            _detalles.FindAll(d => d.IdNota == idNota);

        public bool AgregarDetalle(entDetalleNotaIngreso d)
        {
            d.IdDetalle = _nextDet++;
            _detalles.Add(d);
            return true;
        }

        public bool EliminarDetalle(int idDetalle)
        {
            var d = _detalles.Find(x => x.IdDetalle == idDetalle);
            if (d == null) return false;
            _detalles.Remove(d);
            return true;
        }

        // ── CRUD Incidencias ──────────────────────────────────────────────────────
        public List<entIncidencia> ListarIncidencias(int idNota) =>
            _incidencias.FindAll(i => i.IdNota == idNota);

        public bool RegistrarIncidencia(entIncidencia inc)
        {
            inc.IdIncidencia = _nextInc++;
            _incidencias.Add(inc);
            return true;
        }

        public bool ActualizarIncidencia(entIncidencia inc)
        {
            var i = _incidencias.Find(x => x.IdIncidencia == inc.IdIncidencia);
            if (i == null) return false;
            i.AccionTomada = inc.AccionTomada;
            i.Estado = inc.Estado;
            return true;
        }
    }

}
