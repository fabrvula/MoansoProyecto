using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datVehiculo
    {
        private static List<entVehiculo> lista = new List<entVehiculo>()
        {
            new entVehiculo { IdVehiculo=1, IdCliente=1, Marca="Toyota",
                              Modelo="Corolla", Anio=2020, Placa="ABC-123",
                              Color="Blanco", NombreCliente="Juan",
                              Activo=true, FechaRegistro=DateTime.Now },
            new entVehiculo { IdVehiculo=2, IdCliente=2, Marca="Nissan",
                              Modelo="Sentra", Anio=2019, Placa="XYZ-456",
                              Color="Negro", NombreCliente="María",
                              Activo=true, FechaRegistro=DateTime.Now }
        };
        private static int correlativo = 3;

        public List<entVehiculo> Listar()
        {
            return lista;
        }

        public bool Registrar(entVehiculo obj)
        {
            obj.IdVehiculo = correlativo++;
            lista.Add(obj);
            return true;
        }

        public bool Modificar(entVehiculo obj)
        {
            var item = lista.Find(x => x.IdVehiculo == obj.IdVehiculo);
            if (item == null) return false;
            item.IdCliente = obj.IdCliente;
            item.NombreCliente = obj.NombreCliente;
            item.Marca = obj.Marca;
            item.Modelo = obj.Modelo;
            item.Anio = obj.Anio;
            item.Placa = obj.Placa;
            item.Color = obj.Color;
            item.Activo = obj.Activo;
            return true;
        }

        public bool Deshabilitar(int id)
        {
            var item = lista.Find(x => x.IdVehiculo == id);
            if (item == null) return false;
            item.Activo = false;
            return true;
        }
    }
}
