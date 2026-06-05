using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class datProveedor
    {
        private static List<entProveedor> lista = new List<entProveedor>()
        {
            new entProveedor { IdProveedor=1, NombreProveedor="Toyota Parts",
                       RazonSocial="Toyota Parts S.A.",
                       Ruc="20123456789",              
                       Contacto="Carlos Ruiz",
                       Telefono="999111333", Correo="toyota@mail.com",
                       Pais="Japón", Activo=true, FechaRegistro=DateTime.Now },
            new entProveedor { IdProveedor=2, NombreProveedor="AutoParts USA",
                       RazonSocial="AutoParts USA LLC",
                       Ruc="20987654321",              
                       Contacto="John Smith",
                       Telefono="999444555", Correo="autoparts@mail.com",
                       Pais="Estados Unidos", Activo=true, FechaRegistro=DateTime.Now }
        };
        private static int correlativo = 3;

        public List<entProveedor> Listar()
        {
            return lista;
        }

        public bool Registrar(entProveedor obj)
        {
            obj.IdProveedor = correlativo++;
            lista.Add(obj);
            return true;
        }

        public bool Modificar(entProveedor obj)
        {
            var item = lista.Find(x => x.IdProveedor == obj.IdProveedor);
            if (item == null) return false;
            item.NombreProveedor = obj.NombreProveedor;
            item.RazonSocial = obj.RazonSocial;
            item.Ruc = obj.Ruc;
            item.Contacto = obj.Contacto;
            item.Telefono = obj.Telefono;
            item.Correo = obj.Correo;
            item.Pais = obj.Pais;
            item.Activo = obj.Activo;
            return true;
        }

        public bool Deshabilitar(int id)
        {
            var item = lista.Find(x => x.IdProveedor == id);
            if (item == null) return false;
            item.Activo = false;
            return true;
        }
    }
}
