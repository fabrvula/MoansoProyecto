using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapaDatos.datOrdenCompraImportaciones;
using CapaEntidad;

namespace CapaDatos
{
    public class datOrdenCompraImportaciones
    {
      
            // ─── DATOS DE PRUEBA (reemplazar por SqlConnection luego) ───────────────

            private static List<entOrdenCompraImportaciones> _ordenes = new List<entOrdenCompraImportaciones>
        {
            new entOrdenCompraImportaciones
            {
                IdOrden = 1,
                NumeroOrden = "OC-2026-001",
                IdProveedor = 1,
                NombreProveedor = "AutoParts USA S.A.",
                IdCotizacion = 1,
                FechaEmision = new DateTime(2026, 6, 1),
                FechaEntregaEstimada = new DateTime(2026, 7, 15),
                Moneda = "USD",
                TipoCambio = 3.75m,
                Observaciones = "Importación trimestral",
                Estado = "Aprobada",
                Activo = true
            },
            new entOrdenCompraImportaciones
            {
                IdOrden = 2,
                NumeroOrden = "OC-2026-002",
                IdProveedor = 2,
                NombreProveedor = "CarTech China Ltd.",
                IdCotizacion = 2,
                FechaEmision = new DateTime(2026, 6, 5),
                FechaEntregaEstimada = new DateTime(2026, 8, 1),
                Moneda = "USD",
                TipoCambio = 3.75m,
                Observaciones = "Repuestos motor",
                Estado = "Enviada",
                Activo = true
            },
            new entOrdenCompraImportaciones
            {
                IdOrden = 3,
                NumeroOrden = "OC-2026-003",
                IdProveedor = 1,
                NombreProveedor = "AutoParts USA S.A.",
                IdCotizacion = 3,
                FechaEmision = new DateTime(2026, 6, 8),
                FechaEntregaEstimada = new DateTime(2026, 9, 1),
                Moneda = "EUR",
                TipoCambio = 4.10m,
                Observaciones = "Accesorios interior",
                Estado = "Borrador",
                Activo = true
            }
        };

            private static List<entDetalleOrdenCompra> _detalles = new List<entDetalleOrdenCompra>
        {
            new entDetalleOrdenCompra { IdDetalle=1, IdOrden=1, IdProducto=1, NombreProducto="Filtro de aceite", UnidadMedida="Unidad", CantidadSolicitada=100, PrecioUnitario=5.50m },
            new entDetalleOrdenCompra { IdDetalle=2, IdOrden=1, IdProducto=2, NombreProducto="Pastilla de freno", UnidadMedida="Par",    CantidadSolicitada=50,  PrecioUnitario=18.00m },
            new entDetalleOrdenCompra { IdDetalle=3, IdOrden=2, IdProducto=3, NombreProducto="Bujía NGK",         UnidadMedida="Unidad", CantidadSolicitada=200, PrecioUnitario=3.20m },
            new entDetalleOrdenCompra { IdDetalle=4, IdOrden=3, IdProducto=4, NombreProducto="Faro delantero LED",UnidadMedida="Unidad",              CantidadSolicitada=20,  PrecioUnitario=45.00m }
        };

            private static int _nextId = 4;

            // ─── CRUD ÓRDENES ────────────────────────────────────────────────────────

            public List<entOrdenCompraImportaciones> Listar()
            {
                return _ordenes.FindAll(o => o.Activo);
            }

            public entOrdenCompraImportaciones ObtenerPorId(int idOrden)
            {
                return _ordenes.Find(o => o.IdOrden == idOrden);
            }

            public bool Registrar(entOrdenCompraImportaciones orden)
            {
                orden.IdOrden = _nextId++;
                orden.NumeroOrden = $"OC-{DateTime.Now.Year}-{_nextId:D3}";
                orden.Estado = "Borrador";
                orden.Activo = true;
                _ordenes.Add(orden);
                return true;
            }

            public bool Actualizar(entOrdenCompraImportaciones orden)
            {
                var existe = _ordenes.Find(o => o.IdOrden == orden.IdOrden);
                if (existe == null) return false;
                existe.IdProveedor = orden.IdProveedor;
                existe.NombreProveedor = orden.NombreProveedor;
                existe.FechaEntregaEstimada = orden.FechaEntregaEstimada;
                existe.Moneda = orden.Moneda;
                existe.TipoCambio = orden.TipoCambio;
                existe.Observaciones = orden.Observaciones;
                return true;
            }

            public bool CambiarEstado(int idOrden, string nuevoEstado, string obsAprobador = "")
            {
                var orden = _ordenes.Find(o => o.IdOrden == idOrden);
                if (orden == null) return false;
                orden.Estado = nuevoEstado;
                if (!string.IsNullOrEmpty(obsAprobador))
                    orden.ObservacionesAprobador = obsAprobador;
                return true;
            }

            public bool Deshabilitar(int idOrden)
            {
                var orden = _ordenes.Find(o => o.IdOrden == idOrden);
                if (orden == null) return false;
                orden.Activo = false;
                return true;
            }

            // ─── CRUD DETALLES ───────────────────────────────────────────────────────

            public List<entDetalleOrdenCompra> ListarDetalles(int idOrden)
            {
                return _detalles.FindAll(d => d.IdOrden == idOrden);
            }

            public bool AgregarDetalle(entDetalleOrdenCompra detalle)
            {
                detalle.IdDetalle = _detalles.Count + 1;
                _detalles.Add(detalle);
                return true;
            }

            public bool EliminarDetalle(int idDetalle)
            {
                var d = _detalles.Find(x => x.IdDetalle == idDetalle);
                if (d == null) return false;
                _detalles.Remove(d);
                return true;
            }
        }
    
}
