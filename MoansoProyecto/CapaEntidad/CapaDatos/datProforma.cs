using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;


namespace CapaDatos
{
    public class datProforma
    {
        private static List<entProforma> lista = new List<entProforma>();
        private static int correlativo = 1;

        public List<entProforma> Listar()
        {
            return lista;
        }

        public bool Registrar(entProforma obj)
        {
            obj.IdProforma = correlativo;
            obj.NroProforma = "PRO-" + correlativo.ToString("D4");
            correlativo++;
            lista.Add(obj);
            return true;
        }

        public bool CambiarEstado(int id, string estado)
        {
            var item = lista.Find(x => x.IdProforma == id);
            if (item == null) return false;
            item.Estado = estado;
            return true;
        }

        public entProforma ObtenerPorId(int id)
        {
            return lista.Find(x => x.IdProforma == id);
        }
    }
}
