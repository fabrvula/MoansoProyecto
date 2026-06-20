using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmPanelGerenteGeneral : Form
    {
        private DataTable dtSolicitudesPendientes;

        public FrmPanelGerenteGeneral()
        {
            InitializeComponent();
            ConfigurarBandejaGerencial();
        }

        private void ConfigurarBandejaGerencial()
        {
            // Simulamos la cola de solicitudes que llegan a tu despacho tras la actividad "[Elabora solicitud y cronograma de pagos]" del Contador
            dtSolicitudesPendientes = new DataTable();
            dtSolicitudesPendientes.Columns.Add("ID_Solicitud", typeof(string));
            dtSolicitudesPendientes.Columns.Add("Proveedor", typeof(string));
            dtSolicitudesPendientes.Columns.Add("FechaProgramada", typeof(string));
            dtSolicitudesPendientes.Columns.Add("Prioridad", typeof(string));
            dtSolicitudesPendientes.Columns.Add("MontoTotal", typeof(decimal));

            // Datos cargados listos para tu auditoría de repuestos e importaciones
            dtSolicitudesPendientes.Rows.Add("SOL-9901", "Importadora de Motores del Norte", "19/06/2026", "ALTA", 18500.00m);
            dtSolicitudesPendientes.Rows.Add("SOL-9902", "Frenos y Bujías Trujillo SAC", "22/06/2026", "MEDIA", 3400.00m);

            dgvBandejaGerente.DataSource = dtSolicitudesPendientes;
        }

        private void dgvBandejaGerente_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBandejaGerente.CurrentRow != null)
            {
                string idSol = dgvBandejaGerente.CurrentRow.Cells["ID_Solicitud"].Value.ToString();
                string prov = dgvBandejaGerente.CurrentRow.Cells["Proveedor"].Value.ToString();
                decimal monto = Convert.ToDecimal(dgvBandejaGerente.CurrentRow.Cells["MontoTotal"].Value);

                // Al hacer clic, abres el expediente completo y auditas los 4 objetos cruzados del diagrama
                AuditarObjetosDelFlujo(idSol, prov, monto);
                btnAprobarPago.Enabled = true;
                btnDevolverObservado.Enabled = true;
            }
        }

        private void AuditarObjetosDelFlujo(string idSol, string prov, decimal monto)
        {
            // 1. Objeto: OrdenDeCompra (Interno)
            txtOrdenCompra.Text = "OC-" + idSol.Replace("SOL-", "2026-");

            // 2. Objeto: FacturaDelProveedor (Externo verificado por Contador)
            txtFacturaProveedor.Text = "F001-" + new Random().Next(5000, 9999);
            txtNombreProveedor.Text = prov;

            // 3. Objeto: SolicituddePago
            txtCodigoSolicitud.Text = idSol;
            txtMontoSolicitado.Text = monto.ToString("N2");

            // 4. Objeto: CronogramadePagos
            txtDetalleCronograma.Text = $"-> Pago programado en Tesorería.\r\n" +
                                         $"-> Banco Destino: BCP Operaciones.\r\n" +
                                         $"-> Estado previo: [Verificado vs Orden de Compra por Contador (Conforme)].";
        }

        // [Actividad Gerente General]: Aprobar Pago? -> SI -> Lleva a [Autorizar pagos]
        private void btnAprobarPago_Click(object sender, EventArgs e)
        {
            if (dgvBandejaGerente.CurrentRow != null)
            {
                string idSol = dgvBandejaGerente.CurrentRow.Cells["ID_Solicitud"].Value.ToString();

                MessageBox.Show($"[Actividad: Revisar presupuesto] completada.\r\n" +
                                $"Decisión: PAGO AUTORIZADO para {idSol}.\r\n\r\n" +
                                $"El flujo avanza a:\n1. [Autorizar pagos]\n2. [Ejecutar pago al proveedor]\n3. [Generar notas de salidas de compras].",
                                "Despacho Gerencia General", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RemoverSolicitudProcesada();
            }
        }

        // [Actividad Gerente General]: Aprobar Pago? -> NO -> Lleva a [Devolver con observaciones]
        private void btnDevolverObservado_Click(object sender, EventArgs e)
        {
            if (dgvBandejaGerente.CurrentRow != null)
            {
                string idSol = dgvBandejaGerente.CurrentRow.Cells["ID_Solicitud"].Value.ToString();

                MessageBox.Show($"Decisión: PAGO RECHAZADO para {idSol}.\r\n\r\n" +
                                $"Se ejecuta la actividad [Devolver con observaciones].\r\n" +
                                $"El expediente regresa al carril del Contador para recalcular el cronograma o subsanar la línea de presupuesto.",
                                "Orden de Devolución Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                RemoverSolicitudProcesada();
            }
        }

        private void RemoverSolicitudProcesada()
        {
            dgvBandejaGerente.Rows.Remove(dgvBandejaGerente.CurrentRow);
            if (dgvBandejaGerente.Rows.Count == 0)
            {
                txtOrdenCompra.Clear();
                txtFacturaProveedor.Clear();
                txtNombreProveedor.Clear();
                txtCodigoSolicitud.Clear();
                txtMontoSolicitado.Clear();
                txtDetalleCronograma.Clear();
                btnAprobarPago.Enabled = false;
                btnDevolverObservado.Enabled = false;
            }
        }
    }
}