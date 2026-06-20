namespace CapaPresentacion
{
    partial class FrmPanelGerenteGeneral
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvBandejaGerente = new System.Windows.Forms.DataGridView();
            this.txtOrdenCompra = new System.Windows.Forms.TextBox();
            this.txtFacturaProveedor = new System.Windows.Forms.TextBox();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.txtCodigoSolicitud = new System.Windows.Forms.TextBox();
            this.txtMontoSolicitado = new System.Windows.Forms.TextBox();
            this.txtDetalleCronograma = new System.Windows.Forms.TextBox();
            this.btnAprobarPago = new System.Windows.Forms.Button();
            this.btnDevolverObservado = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpBandeja = new System.Windows.Forms.GroupBox();
            this.grpExpediente = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMontoG = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejaGerente)).BeginInit();
            this.grpBandeja.SuspendLayout();
            this.grpExpediente.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBandejaGerente
            // 
            this.dgvBandejaGerente.AllowUserToAddRows = false;
            this.dgvBandejaGerente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBandejaGerente.Location = new System.Drawing.Point(15, 25);
            this.dgvBandejaGerente.Name = "dgvBandejaGerente";
            this.dgvBandejaGerente.Size = new System.Drawing.Size(400, 435);
            this.dgvBandejaGerente.TabIndex = 0;
            this.dgvBandejaGerente.SelectionChanged += new System.EventHandler(this.dgvBandejaGerente_SelectionChanged);
            // 
            // txtOrdenCompra
            // 
            this.txtOrdenCompra.Location = new System.Drawing.Point(165, 30);
            this.txtOrdenCompra.Name = "txtOrdenCompra";
            this.txtOrdenCompra.ReadOnly = true;
            this.txtOrdenCompra.Size = new System.Drawing.Size(200, 20);
            this.txtOrdenCompra.TabIndex = 1;
            // 
            // txtFacturaProveedor
            // 
            this.txtFacturaProveedor.Location = new System.Drawing.Point(165, 65);
            this.txtFacturaProveedor.Name = "txtFacturaProveedor";
            this.txtFacturaProveedor.ReadOnly = true;
            this.txtFacturaProveedor.Size = new System.Drawing.Size(200, 20);
            this.txtFacturaProveedor.TabIndex = 3;
            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.Location = new System.Drawing.Point(165, 100);
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.ReadOnly = true;
            this.txtNombreProveedor.Size = new System.Drawing.Size(200, 20);
            this.txtNombreProveedor.TabIndex = 5;
            // 
            // txtCodigoSolicitud
            // 
            this.txtCodigoSolicitud.Location = new System.Drawing.Point(165, 175);
            this.txtCodigoSolicitud.Name = "txtCodigoSolicitud";
            this.txtCodigoSolicitud.ReadOnly = true;
            this.txtCodigoSolicitud.Size = new System.Drawing.Size(200, 20);
            this.txtCodigoSolicitud.TabIndex = 8;
            // 
            // txtMontoSolicitado
            // 
            this.txtMontoSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtMontoSolicitado.Location = new System.Drawing.Point(215, 345);
            this.txtMontoSolicitado.Name = "txtMontoSolicitado";
            this.txtMontoSolicitado.ReadOnly = true;
            this.txtMontoSolicitado.Size = new System.Drawing.Size(150, 22);
            this.txtMontoSolicitado.TabIndex = 12;
            this.txtMontoSolicitado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDetalleCronograma
            // 
            this.txtDetalleCronograma.Location = new System.Drawing.Point(15, 240);
            this.txtDetalleCronograma.Multiline = true;
            this.txtDetalleCronograma.Name = "txtDetalleCronograma";
            this.txtDetalleCronograma.ReadOnly = true;
            this.txtDetalleCronograma.Size = new System.Drawing.Size(350, 80);
            this.txtDetalleCronograma.TabIndex = 10;
            // 
            // btnAprobarPago
            // 
            this.btnAprobarPago.BackColor = System.Drawing.Color.Teal;
            this.btnAprobarPago.Enabled = false;
            this.btnAprobarPago.ForeColor = System.Drawing.Color.White;
            this.btnAprobarPago.Location = new System.Drawing.Point(15, 405);
            this.btnAprobarPago.Name = "btnAprobarPago";
            this.btnAprobarPago.Size = new System.Drawing.Size(170, 45);
            this.btnAprobarPago.TabIndex = 13;
            this.btnAprobarPago.Text = "✔ AUTORIZAR PAGO\r\n(Aprobar)";
            this.btnAprobarPago.UseVisualStyleBackColor = false;
            this.btnAprobarPago.Click += new System.EventHandler(this.btnAprobarPago_Click);
            // 
            // btnDevolverObservado
            // 
            this.btnDevolverObservado.BackColor = System.Drawing.Color.Chocolate;
            this.btnDevolverObservado.Enabled = false;
            this.btnDevolverObservado.ForeColor = System.Drawing.Color.White;
            this.btnDevolverObservado.Location = new System.Drawing.Point(200, 405);
            this.btnDevolverObservado.Name = "btnDevolverObservado";
            this.btnDevolverObservado.Size = new System.Drawing.Size(170, 45);
            this.btnDevolverObservado.TabIndex = 14;
            this.btnDevolverObservado.Text = "⚠ DEVOLVER\r\n(Con Observaciones)";
            this.btnDevolverObservado.UseVisualStyleBackColor = false;
            this.btnDevolverObservado.Click += new System.EventHandler(this.btnDevolverObservado_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblHeader.Location = new System.Drawing.Point(20, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(700, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Comprobante de pago a proveedor";
            // 
            // grpBandeja
            // 
            this.grpBandeja.Controls.Add(this.dgvBandejaGerente);
            this.grpBandeja.Location = new System.Drawing.Point(25, 60);
            this.grpBandeja.Name = "grpBandeja";
            this.grpBandeja.Size = new System.Drawing.Size(430, 480);
            this.grpBandeja.TabIndex = 1;
            this.grpBandeja.TabStop = false;
            this.grpBandeja.Text = "Solicitudes por Revisar (Listadas por el Contador)";
            // 
            // grpExpediente
            // 
            this.grpExpediente.Controls.Add(this.label1);
            this.grpExpediente.Controls.Add(this.txtOrdenCompra);
            this.grpExpediente.Controls.Add(this.label2);
            this.grpExpediente.Controls.Add(this.txtFacturaProveedor);
            this.grpExpediente.Controls.Add(this.label3);
            this.grpExpediente.Controls.Add(this.txtNombreProveedor);
            this.grpExpediente.Controls.Add(this.label4);
            this.grpExpediente.Controls.Add(this.label5);
            this.grpExpediente.Controls.Add(this.txtCodigoSolicitud);
            this.grpExpediente.Controls.Add(this.label6);
            this.grpExpediente.Controls.Add(this.txtDetalleCronograma);
            this.grpExpediente.Controls.Add(this.lblMontoG);
            this.grpExpediente.Controls.Add(this.txtMontoSolicitado);
            this.grpExpediente.Controls.Add(this.btnAprobarPago);
            this.grpExpediente.Controls.Add(this.btnDevolverObservado);
            this.grpExpediente.Location = new System.Drawing.Point(475, 60);
            this.grpExpediente.Name = "grpExpediente";
            this.grpExpediente.Size = new System.Drawing.Size(390, 480);
            this.grpExpediente.TabIndex = 2;
            this.grpExpediente.TabStop = false;
            this.grpExpediente.Text = "Análisis e Inspección de Objetos Cruzados";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Orden de Compra:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = " Factura Proveedor:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Razón Social:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(15, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Solicitud de Pago:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "Código Solicitud:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(15, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "Cronograma / Trazabilidad:";
            // 
            // lblMontoG
            // 
            this.lblMontoG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMontoG.Location = new System.Drawing.Point(15, 348);
            this.lblMontoG.Name = "lblMontoG";
            this.lblMontoG.Size = new System.Drawing.Size(200, 23);
            this.lblMontoG.TabIndex = 11;
            this.lblMontoG.Text = "Monto Total de Desembolso (S/.):";
            // 
            // FrmPanelGerenteGeneral
            // 
            this.ClientSize = new System.Drawing.Size(890, 565);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.grpBandeja);
            this.Controls.Add(this.grpExpediente);
            this.Name = "FrmPanelGerenteGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Control Presupuestal - Gerente General";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejaGerente)).EndInit();
            this.grpBandeja.ResumeLayout(false);
            this.grpExpediente.ResumeLayout(false);
            this.grpExpediente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBandejaGerente;
        private System.Windows.Forms.TextBox txtOrdenCompra;
        private System.Windows.Forms.TextBox txtFacturaProveedor;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.TextBox txtCodigoSolicitud;
        private System.Windows.Forms.TextBox txtMontoSolicitado;
        private System.Windows.Forms.TextBox txtDetalleCronograma;
        private System.Windows.Forms.Button btnAprobarPago;
        private System.Windows.Forms.Button btnDevolverObservado;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpBandeja;
        private System.Windows.Forms.GroupBox grpExpediente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMontoG;
    }
}