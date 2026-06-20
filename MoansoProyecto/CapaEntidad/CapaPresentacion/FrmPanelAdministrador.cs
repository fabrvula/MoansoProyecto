using CapaEntidad;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmPanelAdministrador : Form
    {
        private DataTable dtBandejaPendientes;

        public FrmPanelAdministrador()
        {
            InitializeComponent();
            ConfigurarBandejaTrabajo();
        }

        private void ConfigurarBandejaTrabajo()
        {
            // Simulamos la cola de entrada de archivos que el Contador te envía al presionar "Enviar Comprobante"
            dtBandejaPendientes = new DataTable();
            dtBandejaPendientes.Columns.Add("ID_Operacion", typeof(string));
            dtBandejaPendientes.Columns.Add("Contador", typeof(string));
            dtBandejaPendientes.Columns.Add("FechaEnvio", typeof(string));
            dtBandejaPendientes.Columns.Add("EstadoFlujo", typeof(string));
            dtBandejaPendientes.Columns.Add("MontoTotal", typeof(decimal));

            // Datos de prueba iniciales (Autopartes y repuestos)
            dtBandejaPendientes.Rows.Add("OP-8821", "Carlos Mendoza", "18/06/2026 08:30", "En Revision Admin", 1450.00m);
            dtBandejaPendientes.Rows.Add("OP-4105", "Carlos Mendoza", "18/06/2026 09:15", "En Revision Admin", 620.50m);

            dgvBandeja.DataSource = dtBandejaPendientes;
        }

        private void dgvBandeja_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBandeja.CurrentRow != null)
            {
                string idOp = dgvBandeja.CurrentRow.Cells["ID_Operacion"].Value.ToString();
                decimal monto = Convert.ToDecimal(dgvBandeja.CurrentRow.Cells["MontoTotal"].Value);

                // Al seleccionar una fila, el Administrador "abre" e inspecciona los objetos asociados
                CargarDetallesObjetos(idOp, monto);
                btnAprobar.Enabled = true;
                btnInconsistencia.Enabled = true;
            }
        }

        private void CargarDetallesObjetos(string idOp, decimal monto)
        {
            // 1. Mostrar detalles de la inspección del Objeto: Comprobante de Pago (Factura Proveedor)
            txtIdComprobantePago.Text = "CP-" + idOp.Replace("OP-", "");
            txtProveedor.Text = idOp == "OP-8821" ? "Distribuidora Bosch Perú" : "Importaciones Repalsa SAC";
            txtFechaEmision.Text = "16/06/2026";

            // 2. Mostrar detalles de la inspección del Objeto: Comprobante de Ingreso de Ventas (Caja)
            txtIdIngresoVentas.Text = "IV-" + idOp.Replace("OP-", "");
            txtMetodoPago.Text = idOp == "OP-8821" ? "Transferencia Bancaria" : "Efectivo Caja Chica";
            txtMontoTotal.Text = monto.ToString("N2");

            // Detalles específicos del inventario de repuestos asociados a ese objeto
            txtDetalleRepuestos.Text = idOp == "OP-8821"
                ? "-> 4 pastillas de freno cerámicas Brembo\r\n-> 2 discos de freno ventilados"
                : "-> 1 kit de embrague para Toyota Yaris\r\n-> 1 galón de aceite sintético 5W-30";
        }

        // [Actividad Administrador] Aprobar comprobante -> Fin del flujo
        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (dgvBandeja.CurrentRow != null)
            {
                string idOp = dgvBandeja.CurrentRow.Cells["ID_Operacion"].Value.ToString();

                MessageBox.Show($"Operación {idOp} evaluada como CONFORME.\r\nSe ejecutó la acción [Aprobar comprobante]. El flujo ha finalizado.",
                                "Control de Gestión - Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EliminarFilaProcesada();
            }
        }

        // [Actividad Administrador] Corrige la inconsistencia -> Regresa al flujo anterior
        private void btnInconsistencia_Click(object sender, EventArgs e)
        {
            if (dgvBandeja.CurrentRow != null)
            {
                string idOp = dgvBandeja.CurrentRow.Cells["ID_Operacion"].Value.ToString();

                MessageBox.Show($"Operación {idOp} rechazada. El flujo regresa al paso [Corrige la inconsistencia] para que el contador rectifique los montos.",
                                "Alerta de Inconsistencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                EliminarFilaProcesada();
            }
        }

        private void EliminarFilaProcesada()
        {
            dgvBandeja.Rows.Remove(dgvBandeja.CurrentRow);
            if (dgvBandeja.Rows.Count == 0)
            {
                txtIdComprobantePago.Clear();
                txtProveedor.Clear();
                txtFechaEmision.Clear();
                txtIdIngresoVentas.Clear();
                txtMetodoPago.Clear();
                txtMontoTotal.Clear();
                txtDetalleRepuestos.Clear();
                btnAprobar.Enabled = false;
                btnInconsistencia.Enabled = false;
            }
        }
    }
}