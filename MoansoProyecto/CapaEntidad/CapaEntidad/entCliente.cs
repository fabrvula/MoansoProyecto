using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entCliente
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }    
        public string RazonSocial { get; set; }
        public string TipoCliente { get; set; }      
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}