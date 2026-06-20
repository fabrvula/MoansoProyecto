namespace CapaPresentacion
{
    partial class FrmPanelAdministrador
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
            this.dgvBandeja = new System.Windows.Forms.DataGridView();
            this.txtIdComprobantePago = new System.Windows.Forms.TextBox();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.txtFechaEmision = new System.Windows.Forms.TextBox();
            this.txtIdIngresoVentas = new System.Windows.Forms.TextBox();
            this.txtMetodoPago = new System.Windows.Forms.TextBox();
            this.txtMontoTotal = new System.Windows.Forms.TextBox();
            this.txtDetalleRepuestos = new System.Windows.Forms.TextBox();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.btnInconsistencia = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpBandeja = new System.Windows.Forms.GroupBox();
            this.grpInspector = new System.Windows.Forms.GroupBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            this.lblM = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandeja)).BeginInit();
            this.grpBandeja.SuspendLayout();
            this.grpInspector.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBandeja
            // 
            this.dgvBandeja.AllowUserToAddRows = false;
            this.dgvBandeja.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBandeja.Location = new System.Drawing.Point(15, 25);
            this.dgvBandeja.Name = "dgvBandeja";
            this.dgvBandeja.Size = new System.Drawing.Size(400, 435);
            this.dgvBandeja.TabIndex = 0;
            this.dgvBandeja.SelectionChanged += new System.EventHandler(this.dgvBandeja_SelectionChanged);
            // 
            // txtIdComprobantePago
            // 
            this.txtIdComprobantePago.Location = new System.Drawing.Point(110, 50);
            this.txtIdComprobantePago.Name = "txtIdComprobantePago";
            this.txtIdComprobantePago.ReadOnly = true;
            this.txtIdComprobantePago.Size = new System.Drawing.Size(250, 20);
            this.txtIdComprobantePago.TabIndex = 2;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(110, 80);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(250, 20);
            this.txtProveedor.TabIndex = 4;
            // 
            // txtFechaEmision
            // 
            this.txtFechaEmision.Location = new System.Drawing.Point(110, 110);
            this.txtFechaEmision.Name = "txtFechaEmision";
            this.txtFechaEmision.ReadOnly = true;
            this.txtFechaEmision.Size = new System.Drawing.Size(250, 20);
            this.txtFechaEmision.TabIndex = 6;
            // 
            // txtIdIngresoVentas
            // 
            this.txtIdIngresoVentas.Location = new System.Drawing.Point(110, 180);
            this.txtIdIngresoVentas.Name = "txtIdIngresoVentas";
            this.txtIdIngresoVentas.ReadOnly = true;
            this.txtIdIngresoVentas.Size = new System.Drawing.Size(250, 20);
            this.txtIdIngresoVentas.TabIndex = 9;
            // 
            // txtMetodoPago
            // 
            this.txtMetodoPago.Location = new System.Drawing.Point(110, 210);
            this.txtMetodoPago.Name = "txtMetodoPago";
            this.txtMetodoPago.ReadOnly = true;
            this.txtMetodoPago.Size = new System.Drawing.Size(250, 20);
            this.txtMetodoPago.TabIndex = 11;
            // 
            // txtMontoTotal
            // 
            this.txtMontoTotal.Location = new System.Drawing.Point(145, 360);
            this.txtMontoTotal.Name = "txtMontoTotal";
            this.txtMontoTotal.ReadOnly = true;
            this.txtMontoTotal.Size = new System.Drawing.Size(215, 20);
            this.txtMontoTotal.TabIndex = 15;
            this.txtMontoTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDetalleRepuestos
            // 
            this.txtDetalleRepuestos.Location = new System.Drawing.Point(15, 270);
            this.txtDetalleRepuestos.Multiline = true;
            this.txtDetalleRepuestos.Name = "txtDetalleRepuestos";
            this.txtDetalleRepuestos.ReadOnly = true;
            this.txtDetalleRepuestos.Size = new System.Drawing.Size(345, 75);
            this.txtDetalleRepuestos.TabIndex = 13;
            // 
            // btnAprobar
            // 
            this.btnAprobar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAprobar.Enabled = false;
            this.btnAprobar.ForeColor = System.Drawing.Color.White;
            this.btnAprobar.Location = new System.Drawing.Point(15, 415);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(165, 40);
            this.btnAprobar.TabIndex = 16;
            this.btnAprobar.Text = "✔ APROBAR\r\n(Conforme)";
            this.btnAprobar.UseVisualStyleBackColor = false;
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // btnInconsistencia
            // 
            this.btnInconsistencia.BackColor = System.Drawing.Color.Firebrick;
            this.btnInconsistencia.Enabled = false;
            this.btnInconsistencia.ForeColor = System.Drawing.Color.White;
            this.btnInconsistencia.Location = new System.Drawing.Point(195, 415);
            this.btnInconsistencia.Name = "btnInconsistencia";
            this.btnInconsistencia.Size = new System.Drawing.Size(165, 40);
            this.btnInconsistencia.TabIndex = 17;
            this.btnInconsistencia.Text = "❌ RECHAZAR\r\n(Inconsistencia)";
            this.btnInconsistencia.UseVisualStyleBackColor = false;
            this.btnInconsistencia.Click += new System.EventHandler(this.btnInconsistencia_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(600, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Comprobante de Ingresos por Ventas";
            // 
            // grpBandeja
            // 
            this.grpBandeja.Controls.Add(this.dgvBandeja);
            this.grpBandeja.Location = new System.Drawing.Point(25, 60);
            this.grpBandeja.Name = "grpBandeja";
            this.grpBandeja.Size = new System.Drawing.Size(430, 480);
            this.grpBandeja.TabIndex = 1;
            this.grpBandeja.TabStop = false;
            this.grpBandeja.Text = "Bandeja de Pendientes (Enviados por el Contador)";
            // 
            // grpInspector
            // 
            this.grpInspector.Controls.Add(this.lbl1);
            this.grpInspector.Controls.Add(this.lbl2);
            this.grpInspector.Controls.Add(this.txtIdComprobantePago);
            this.grpInspector.Controls.Add(this.lbl3);
            this.grpInspector.Controls.Add(this.txtProveedor);
            this.grpInspector.Controls.Add(this.lbl4);
            this.grpInspector.Controls.Add(this.txtFechaEmision);
            this.grpInspector.Controls.Add(this.lbl5);
            this.grpInspector.Controls.Add(this.lbl6);
            this.grpInspector.Controls.Add(this.txtIdIngresoVentas);
            this.grpInspector.Controls.Add(this.lbl7);
            this.grpInspector.Controls.Add(this.txtMetodoPago);
            this.grpInspector.Controls.Add(this.lblItems);
            this.grpInspector.Controls.Add(this.txtDetalleRepuestos);
            this.grpInspector.Controls.Add(this.lblM);
            this.grpInspector.Controls.Add(this.txtMontoTotal);
            this.grpInspector.Controls.Add(this.btnAprobar);
            this.grpInspector.Controls.Add(this.btnInconsistencia);
            this.grpInspector.Location = new System.Drawing.Point(475, 60);
            this.grpInspector.Name = "grpInspector";
            this.grpInspector.Size = new System.Drawing.Size(380, 480);
            this.grpInspector.TabIndex = 2;
            this.grpInspector.TabStop = false;
            this.grpInspector.Text = "Auditoría Interna de Objetos";
            // 
            // lbl1
            // 
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lbl1.Location = new System.Drawing.Point(15, 25);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(200, 23);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Comprobante de Pago";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(15, 53);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(80, 23);
            this.lbl2.TabIndex = 1;
            this.lbl2.Text = "ID Registro:";
            // 
            // lbl3
            // 
            this.lbl3.Location = new System.Drawing.Point(15, 83);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(80, 23);
            this.lbl3.TabIndex = 3;
            this.lbl3.Text = "Proveedor:";
            // 
            // lbl4
            // 
            this.lbl4.Location = new System.Drawing.Point(15, 113);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(80, 23);
            this.lbl4.TabIndex = 5;
            this.lbl4.Text = "F. Emisión:";
            // 
            // lbl5
            // 
            this.lbl5.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lbl5.Location = new System.Drawing.Point(15, 155);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(250, 23);
            this.lbl5.TabIndex = 7;
            this.lbl5.Text = " Comprobante Ingreso Ventas";
            // 
            // lbl6
            // 
            this.lbl6.Location = new System.Drawing.Point(15, 183);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(80, 23);
            this.lbl6.TabIndex = 8;
            this.lbl6.Text = "ID Ingreso:";
            // 
            // lbl7
            // 
            this.lbl7.Location = new System.Drawing.Point(15, 213);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(80, 23);
            this.lbl7.TabIndex = 10;
            this.lbl7.Text = "Método Pago:";
            // 
            // lblItems
            // 
            this.lblItems.Location = new System.Drawing.Point(15, 250);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(200, 23);
            this.lblItems.TabIndex = 12;
            this.lblItems.Text = "Items e Importaciones a Validar:";
            // 
            // lblM
            // 
            this.lblM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblM.Location = new System.Drawing.Point(15, 363);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(120, 23);
            this.lblM.TabIndex = 14;
            this.lblM.Text = "Monto Auditado (S/.):";
            // 
            // FrmPanelAdministrador
            // 
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.grpBandeja);
            this.Controls.Add(this.grpInspector);
            this.Name = "FrmPanelAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Control - Administrador (Autopartes)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandeja)).EndInit();
            this.grpBandeja.ResumeLayout(false);
            this.grpInspector.ResumeLayout(false);
            this.grpInspector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBandeja;
        private System.Windows.Forms.TextBox txtIdComprobantePago;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.TextBox txtFechaEmision;
        private System.Windows.Forms.TextBox txtIdIngresoVentas;
        private System.Windows.Forms.TextBox txtMetodoPago;
        private System.Windows.Forms.TextBox txtMontoTotal;
        private System.Windows.Forms.TextBox txtDetalleRepuestos;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.Button btnInconsistencia;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpBandeja;
        private System.Windows.Forms.GroupBox grpInspector;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Label lblM;
    }
}