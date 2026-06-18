using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class datProducto
    {
        private static List<entProducto> lista = new List<entProducto>()
        {
            new entProducto { IdProducto=1, Codigo="PROD-001",
                              Descripcion="Filtro de aceite Toyota",
                              Categoria="Filtros", Precio=25.50m,
                              Stock=10, Activo=true, FechaRegistro=DateTime.Now },
            new entProducto { IdProducto=2, Codigo="PROD-002",
                              Descripcion="Pastillas de freno Nissan",
                              Categoria="Frenos", Precio=45.00m,
                              Stock=8, Activo=true, FechaRegistro=DateTime.Now }
        };
        private static int correlativo = 3;

        public List<entProducto> Listar()
        {
            return lista;
        }

        // ── Generar el siguiente código automático ───────────
        public string GenerarCodigo()
        {
            return "PROD-" + correlativo.ToString("D3");
        }

        public bool Registrar(entProducto obj)
        {
            obj.IdProducto = correlativo;
            obj.Codigo = "PROD-" + correlativo.ToString("D3");
            correlativo++;
            lista.Add(obj);
            return true;
        }

        public bool Modificar(entProducto obj)
        {
            var item = lista.Find(x => x.IdProducto == obj.IdProducto);
            if (item == null) return false;
            item.Descripcion = obj.Descripcion;
            item.Categoria = obj.Categoria;
            item.Precio = obj.Precio;
            item.Stock = obj.Stock;
            item.Activo = obj.Activo;
            return true;
        }

        public bool Deshabilitar(int id)
        {
            var item = lista.Find(x => x.IdProducto == id);
            if (item == null) return false;
            item.Activo = false;
            return true;
        }
    }
}