using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entVehiculo
    {
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }

        // Para mostrar nombre del cliente
        public string NombreCliente { get; set; }
    }
}