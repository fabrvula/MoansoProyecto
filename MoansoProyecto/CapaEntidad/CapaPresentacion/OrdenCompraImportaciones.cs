using CapaEntidad;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class OrdenCompraImportaciones : Form
    {
        // ── Lógica ───────────────────────────────────────────────────────────────
        private readonly logOrdenCompraImportaciones _log = new logOrdenCompraImportaciones();
        private entOrdenCompraImportaciones _ordenActual = null;
        private List<entDetalleOrdenCompra> _detallesTemp = new List<entDetalleOrdenCompra>();
        private bool _modoEdicion = false;

        // ── Controles ────────────────────────────────────────────────────────────
        private Button btnProveedores, btnProductos, btnProformas;
        private DataGridView dgvOrdenes;
        private Button btnNuevo, btnEditar, btnEnviar, btnAprobar, btnDevolver, btnDeshabilitar, btnSalir;
        private TextBox txtNumeroOrden, txtNombreProveedor, txtObservaciones, txtObsAprobador;
        private ComboBox cboMoneda;
        private NumericUpDown numTipoCambio;
        private DateTimePicker dtpFechaEmision, dtpFechaEntrega;
        private Label lblEstado;
        private DataGridView dgvDetalle;
        private TextBox txtProducto, txtUnidad;
        private NumericUpDown numCantidad, numPrecio;
        private Button btnAgregarProducto, btnQuitarProducto;
        private Label lblTotal;

        // ── Constructor ──────────────────────────────────────────────────────────
        public OrdenCompraImportaciones()
        {
            InitializeComponent();   // llama al .Designer.cs generado por VS (vacío)
            ConstruirUI();           // construye toda la interfaz personalizada
            CargarOrdenes();         // carga datos al abrir
        }

        private void OrdenCompraImportaciones_Load(object sender, EventArgs e)
        {
            // vacío — todo se inicializa en el constructor
        }

        // ════════════════════════════════════════════════════════════════════════
        //  DISEÑO DEL FORMULARIO
        // ════════════════════════════════════════════════════════════════════════
        private void ConstruirUI()
        {
            this.Text = "Órdenes de Compra por Importación";
            this.Size = new Size(1100, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9f);
            this.MinimumSize = new Size(1000, 680);

            // ── Menú superior ────────────────────────────────────────────────────
            var pnlMenu = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.FromArgb(30, 30, 30) };
            btnProveedores = MenuBtn("Proveedores");
            btnProductos = MenuBtn("Productos");
            btnProformas = MenuBtn("Proformas");
            btnProveedores.Location = new Point(10, 7);
            btnProductos.Location = new Point(120, 7);
            btnProformas.Location = new Point(220, 7);
            pnlMenu.Controls.AddRange(new Control[] { btnProveedores, btnProductos, btnProformas });

            // ── Panel izquierdo – lista ──────────────────────────────────────────
            var pnlLista = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(420, 610),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };

            var lblTituloLista = new Label
            {
                Text = "Órdenes de Compra",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(0, 0),
                Size = new Size(420, 22)
            };

            dgvOrdenes = new DataGridView
            {
                Location = new Point(0, 26),
                Size = new Size(420, 520),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right
            };
            dgvOrdenes.SelectionChanged += DgvOrdenes_SelectionChanged;
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Width = 40 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNumero", HeaderText = "N° Orden", Width = 110 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProveedor", HeaderText = "Proveedor" });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", Width = 80 });

            // Botones de acción
            var pnlBotones = new FlowLayoutPanel
            {
                Location = new Point(0, 555),
                Size = new Size(420, 52),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            btnNuevo = AccionBtn("NUEVO", Color.FromArgb(0, 122, 204));
            btnEditar = AccionBtn("EDITAR", Color.FromArgb(70, 130, 70));
            btnDeshabilitar = AccionBtn("ANULAR", Color.FromArgb(180, 60, 60));
            btnSalir = AccionBtn("SALIR", Color.FromArgb(100, 100, 100));
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnDeshabilitar.Click += BtnDeshabilitar_Click;
            btnSalir.Click += (s, e) => this.Close();
            pnlBotones.Controls.AddRange(new Control[] { btnNuevo, btnEditar, btnDeshabilitar, btnSalir });

            pnlLista.Controls.AddRange(new Control[] { lblTituloLista, dgvOrdenes, pnlBotones });

            // ── Panel derecho – detalle ──────────────────────────────────────────
            var pnlDetalle = new Panel
            {
                Location = new Point(445, 50),
                Size = new Size(635, 610),
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 10;
            pnlDetalle.Controls.Add(Lbl("N° Orden:", 10, y));
            txtNumeroOrden = Txt(100, y, 160, true);
            pnlDetalle.Controls.Add(txtNumeroOrden);

            lblEstado = new Label
            {
                Text = "Estado: —",
                Location = new Point(290, y + 3),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            pnlDetalle.Controls.Add(lblEstado);

            y += 35;
            pnlDetalle.Controls.Add(Lbl("Proveedor:", 10, y));
            txtNombreProveedor = Txt(100, y, 300);
            pnlDetalle.Controls.Add(txtNombreProveedor);

            y += 32;
            pnlDetalle.Controls.Add(Lbl("F. Emisión:", 10, y));
            dtpFechaEmision = new DateTimePicker { Location = new Point(100, y), Size = new Size(140, 24), Format = DateTimePickerFormat.Short };
            pnlDetalle.Controls.Add(dtpFechaEmision);
            pnlDetalle.Controls.Add(Lbl("F. Entrega:", 260, y));
            dtpFechaEntrega = new DateTimePicker { Location = new Point(350, y), Size = new Size(140, 24), Format = DateTimePickerFormat.Short };
            pnlDetalle.Controls.Add(dtpFechaEntrega);

            y += 32;
            pnlDetalle.Controls.Add(Lbl("Moneda:", 10, y));
            cboMoneda = new ComboBox { Location = new Point(100, y), Size = new Size(100, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cboMoneda.Items.AddRange(new object[] { "USD", "EUR", "PEN" });
            cboMoneda.SelectedIndex = 0;
            pnlDetalle.Controls.Add(cboMoneda);
            pnlDetalle.Controls.Add(Lbl("T. Cambio:", 220, y));
            numTipoCambio = new NumericUpDown { Location = new Point(310, y), Size = new Size(90, 24), DecimalPlaces = 2, Minimum = 0, Maximum = 999, Value = 3.75m };
            pnlDetalle.Controls.Add(numTipoCambio);

            y += 32;
            pnlDetalle.Controls.Add(Lbl("Observaciones:", 10, y));
            txtObservaciones = new TextBox { Location = new Point(110, y), Size = new Size(500, 22), MaxLength = 300 };
            pnlDetalle.Controls.Add(txtObservaciones);

            // Separador
            y += 32;
            pnlDetalle.Controls.Add(new Label { Location = new Point(10, y), Size = new Size(600, 2), BackColor = Color.LightGray });

            // Sección productos
            y += 10;
            pnlDetalle.Controls.Add(new Label { Text = "Productos de la Orden", Font = new Font("Segoe UI", 9f, FontStyle.Bold), Location = new Point(10, y), Size = new Size(250, 20) });

            y += 26;
            pnlDetalle.Controls.Add(Lbl("Producto:", 10, y));
            txtProducto = Txt(80, y, 180);
            pnlDetalle.Controls.Add(txtProducto);
            pnlDetalle.Controls.Add(Lbl("UM:", 272, y));
            txtUnidad = Txt(295, y, 70);
            pnlDetalle.Controls.Add(txtUnidad);
            pnlDetalle.Controls.Add(Lbl("Cant:", 378, y));
            numCantidad = new NumericUpDown { Location = new Point(410, y), Size = new Size(70, 24), Minimum = 1, Maximum = 99999, Value = 1 };
            pnlDetalle.Controls.Add(numCantidad);

            y += 28;
            pnlDetalle.Controls.Add(Lbl("Precio Unit.:", 10, y));
            numPrecio = new NumericUpDown { Location = new Point(90, y), Size = new Size(100, 24), DecimalPlaces = 2, Minimum = 0, Maximum = 999999, Value = 0 };
            pnlDetalle.Controls.Add(numPrecio);
            btnAgregarProducto = new Button { Text = "+ Agregar", Location = new Point(210, y - 2), Size = new Size(100, 26), BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnQuitarProducto = new Button { Text = "- Quitar", Location = new Point(320, y - 2), Size = new Size(100, 26), BackColor = Color.FromArgb(180, 60, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAgregarProducto.Click += BtnAgregarProducto_Click;
            btnQuitarProducto.Click += BtnQuitarProducto_Click;
            pnlDetalle.Controls.AddRange(new Control[] { btnAgregarProducto, btnQuitarProducto });

            // Grid detalle
            y += 34;
            dgvDetalle = new DataGridView
            {
                Location = new Point(10, y),
                Size = new Size(600, 160),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProd", HeaderText = "Producto" });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUM", HeaderText = "UM", Width = 60 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCant", HeaderText = "Cant.", Width = 70 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrecio", HeaderText = "P. Unit", Width = 80 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSub", HeaderText = "Subtotal", Width = 90 });
            pnlDetalle.Controls.Add(dgvDetalle);

            y += 168;
            lblTotal = new Label { Text = "Total: $ 0.00", Location = new Point(430, y), Size = new Size(180, 22), Font = new Font("Segoe UI", 10f, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };
            pnlDetalle.Controls.Add(lblTotal);

            y += 28;
            pnlDetalle.Controls.Add(Lbl("Obs. Aprobador:", 10, y));
            txtObsAprobador = new TextBox { Location = new Point(120, y), Size = new Size(490, 22), MaxLength = 500, BackColor = Color.LightYellow };
            pnlDetalle.Controls.Add(txtObsAprobador);

            // Botones de flujo
            y += 32;
            btnEnviar = new Button { Text = "📤 ENVIAR ORDEN", Location = new Point(10, y), Size = new Size(155, 34), BackColor = Color.FromArgb(0, 150, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            btnAprobar = new Button { Text = "✔ APROBAR", Location = new Point(175, y), Size = new Size(130, 34), BackColor = Color.FromArgb(56, 142, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            btnDevolver = new Button { Text = "↩ DEVOLVER", Location = new Point(315, y), Size = new Size(130, 34), BackColor = Color.FromArgb(230, 81, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            var btnGuardar = new Button { Text = "💾 GUARDAR", Location = new Point(455, y), Size = new Size(130, 34), BackColor = Color.FromArgb(30, 100, 180), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            btnEnviar.Click += BtnEnviar_Click;
            btnAprobar.Click += BtnAprobar_Click;
            btnDevolver.Click += BtnDevolver_Click;
            btnGuardar.Click += BtnGuardar_Click;
            pnlDetalle.Controls.AddRange(new Control[] { btnEnviar, btnAprobar, btnDevolver, btnGuardar });

            // Ensamblar
            this.Controls.AddRange(new Control[] { pnlMenu, pnlLista, pnlDetalle });

            ModoVista();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  HELPERS
        // ════════════════════════════════════════════════════════════════════════
        private Button MenuBtn(string txt) =>
            new Button { Text = txt, Size = new Size(100, 26), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.Transparent, FlatAppearance = { BorderColor = Color.Gray } };

        private Button AccionBtn(string txt, Color color) =>
            new Button { Text = txt, Size = new Size(90, 30), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(2) };

        private Label Lbl(string txt, int x, int y) =>
            new Label { Text = txt, Location = new Point(x, y + 3), AutoSize = true };

        private TextBox Txt(int x, int y, int w, bool readOnly = false) =>
            new TextBox { Location = new Point(x, y), Size = new Size(w, 22), ReadOnly = readOnly, BackColor = readOnly ? Color.WhiteSmoke : Color.White };

        // ════════════════════════════════════════════════════════════════════════
        //  CARGA Y SELECCIÓN
        // ════════════════════════════════════════════════════════════════════════
        private void CargarOrdenes()
        {
            dgvOrdenes.Rows.Clear();
            foreach (var o in _log.Listar())
            {
                dgvOrdenes.Rows.Add(o.IdOrden, o.NumeroOrden, o.NombreProveedor, o.Estado);
                dgvOrdenes.Rows[dgvOrdenes.Rows.Count - 1].DefaultCellStyle.BackColor = ColorEstado(o.Estado);
            }
        }

        private Color ColorEstado(string estado)
        {
            switch (estado)
            {
                case "Aprobada": return Color.FromArgb(220, 255, 220);
                case "Enviada": return Color.FromArgb(220, 235, 255);
                case "Devuelta": return Color.FromArgb(255, 230, 210);
                default: return Color.White;
            }
        }

        private void DgvOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenes.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvOrdenes.SelectedRows[0].Cells["colId"].Value);
            _ordenActual = _log.ObtenerPorId(id);
            MostrarOrden(_ordenActual);
            CargarDetallesGrid(id);
            ModoVista();
        }

        private void MostrarOrden(entOrdenCompraImportaciones o)
        {
            if (o == null) return;
            txtNumeroOrden.Text = o.NumeroOrden;
            txtNombreProveedor.Text = o.NombreProveedor;
            dtpFechaEmision.Value = o.FechaEmision;
            dtpFechaEntrega.Value = o.FechaEntregaEstimada;
            cboMoneda.Text = o.Moneda;
            numTipoCambio.Value = o.TipoCambio;
            txtObservaciones.Text = o.Observaciones;
            txtObsAprobador.Text = o.ObservacionesAprobador ?? "";
            lblEstado.Text = $"Estado: {o.Estado}";
            lblEstado.ForeColor = o.Estado == "Aprobada" ? Color.DarkGreen
                                    : o.Estado == "Devuelta" ? Color.OrangeRed
                                    : o.Estado == "Enviada" ? Color.DarkBlue
                                    : Color.DimGray;
        }

        private void CargarDetallesGrid(int idOrden)
        {
            dgvDetalle.Rows.Clear();
            decimal total = 0;
            foreach (var d in _log.ListarDetalles(idOrden))
            {
                dgvDetalle.Rows.Add(d.NombreProducto, d.UnidadMedida, d.CantidadSolicitada, d.PrecioUnitario.ToString("F2"), d.Subtotal.ToString("F2"));
                total += d.Subtotal;
            }
            lblTotal.Text = $"Total: {(_ordenActual?.Moneda ?? "$")} {total:N2}";
        }

        // ════════════════════════════════════════════════════════════════════════
        //  MODOS
        // ════════════════════════════════════════════════════════════════════════
        private void ModoVista()
        {
            _modoEdicion = false;
            bool hayOrden = _ordenActual != null;
            string estado = hayOrden ? _ordenActual.Estado : "";

            txtNombreProveedor.ReadOnly = true;
            txtObservaciones.ReadOnly = true;
            cboMoneda.Enabled = false;
            numTipoCambio.Enabled = false;
            dtpFechaEmision.Enabled = false;
            dtpFechaEntrega.Enabled = false;
            txtProducto.Enabled = false;
            txtUnidad.Enabled = false;
            numCantidad.Enabled = false;
            numPrecio.Enabled = false;
            btnAgregarProducto.Enabled = false;
            btnQuitarProducto.Enabled = false;
            txtObsAprobador.ReadOnly = true;

            btnEnviar.Enabled = hayOrden && estado == "Borrador";
            btnAprobar.Enabled = hayOrden && estado == "Enviada";
            btnDevolver.Enabled = hayOrden && estado == "Enviada";
            btnEditar.Enabled = hayOrden && (estado == "Borrador" || estado == "Devuelta");
            btnDeshabilitar.Enabled = hayOrden && estado != "Aprobada";
        }

        private void ModoEdicion()
        {
            _modoEdicion = true;
            txtNombreProveedor.ReadOnly = false;
            txtObservaciones.ReadOnly = false;
            cboMoneda.Enabled = true;
            numTipoCambio.Enabled = true;
            dtpFechaEmision.Enabled = true;
            dtpFechaEntrega.Enabled = true;
            txtProducto.Enabled = true;
            txtUnidad.Enabled = true;
            numCantidad.Enabled = true;
            numPrecio.Enabled = true;
            btnAgregarProducto.Enabled = true;
            btnQuitarProducto.Enabled = true;
            txtObsAprobador.ReadOnly = true;

            btnEnviar.Enabled = false;
            btnAprobar.Enabled = false;
            btnDevolver.Enabled = false;
        }

        private void LimpiarFormulario()
        {
            txtNumeroOrden.Text = "(generado al guardar)";
            txtNombreProveedor.Text = "";
            dtpFechaEmision.Value = DateTime.Today;
            dtpFechaEntrega.Value = DateTime.Today.AddDays(45);
            cboMoneda.SelectedIndex = 0;
            numTipoCambio.Value = 3.75m;
            txtObservaciones.Text = "";
            txtObsAprobador.Text = "";
            lblEstado.Text = "Estado: Borrador";
            lblEstado.ForeColor = Color.DimGray;
            dgvDetalle.Rows.Clear();
            lblTotal.Text = "Total: $ 0.00";
            _detallesTemp.Clear();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  EVENTOS – CRUD
        // ════════════════════════════════════════════════════════════════════════
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            _ordenActual = null;
            LimpiarFormulario();
            ModoEdicion();
            txtNombreProveedor.Focus();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (_ordenActual == null) { MessageBox.Show("Seleccione una orden para editar.", "Aviso"); return; }
            ModoEdicion();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion) return;
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text)) { MessageBox.Show("Ingrese el nombre del proveedor.", "Validación"); return; }

            if (_ordenActual == null)
            {
                var nueva = new entOrdenCompraImportaciones
                {
                    NombreProveedor = txtNombreProveedor.Text.Trim(),
                    FechaEmision = dtpFechaEmision.Value,
                    FechaEntregaEstimada = dtpFechaEntrega.Value,
                    Moneda = cboMoneda.Text,
                    TipoCambio = numTipoCambio.Value,
                    Observaciones = txtObservaciones.Text.Trim()
                };
                _log.Registrar(nueva);
                foreach (var d in _detallesTemp) { d.IdOrden = nueva.IdOrden; _log.AgregarDetalle(d); }
                MessageBox.Show("Orden registrada en estado Borrador.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _ordenActual.NombreProveedor = txtNombreProveedor.Text.Trim();
                _ordenActual.FechaEmision = dtpFechaEmision.Value;
                _ordenActual.FechaEntregaEstimada = dtpFechaEntrega.Value;
                _ordenActual.Moneda = cboMoneda.Text;
                _ordenActual.TipoCambio = numTipoCambio.Value;
                _ordenActual.Observaciones = txtObservaciones.Text.Trim();
                _log.Actualizar(_ordenActual);
                MessageBox.Show("Orden actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarOrdenes();
            ModoVista();
        }

        private void BtnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_ordenActual == null) return;
            if (MessageBox.Show($"¿Anular la orden {_ordenActual.NumeroOrden}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _log.Deshabilitar(_ordenActual.IdOrden);
                _ordenActual = null;
                LimpiarFormulario();
                CargarOrdenes();
                ModoVista();
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  EVENTOS – FLUJO DE APROBACIÓN
        // ════════════════════════════════════════════════════════════════════════
        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (_ordenActual == null) return;
            var (ok, msg) = _log.Enviar(_ordenActual.IdOrden);
            MessageBox.Show(msg, ok ? "Éxito" : "Error", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { _ordenActual = _log.ObtenerPorId(_ordenActual.IdOrden); MostrarOrden(_ordenActual); CargarOrdenes(); ModoVista(); }
        }

        private void BtnAprobar_Click(object sender, EventArgs e)
        {
            if (_ordenActual == null) return;
            var (ok, msg) = _log.Aprobar(_ordenActual.IdOrden);
            MessageBox.Show(msg, ok ? "Orden Aprobada ✔" : "Error", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { _ordenActual = _log.ObtenerPorId(_ordenActual.IdOrden); MostrarOrden(_ordenActual); CargarOrdenes(); ModoVista(); }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            if (_ordenActual == null) return;
            string obs = txtObsAprobador.Text.Trim();
            if (string.IsNullOrWhiteSpace(obs))
            {
                MessageBox.Show("Escriba el motivo de devolución en el campo 'Obs. Aprobador'.", "Requerido");
                txtObsAprobador.ReadOnly = false;
                txtObsAprobador.Focus();
                return;
            }
            var (ok, msg) = _log.Devolver(_ordenActual.IdOrden, obs);
            MessageBox.Show(msg, ok ? "Orden Devuelta" : "Error", MessageBoxButtons.OK, ok ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
            if (ok) { _ordenActual = _log.ObtenerPorId(_ordenActual.IdOrden); MostrarOrden(_ordenActual); CargarOrdenes(); ModoVista(); }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  EVENTOS – DETALLE DE PRODUCTOS
        // ════════════════════════════════════════════════════════════════════════
        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProducto.Text)) { MessageBox.Show("Ingrese el nombre del producto."); return; }
            if (numPrecio.Value == 0) { MessageBox.Show("Ingrese un precio mayor a 0."); return; }

            var det = new entDetalleOrdenCompra
            {
                NombreProducto = txtProducto.Text.Trim(),
                UnidadMedida = string.IsNullOrWhiteSpace(txtUnidad.Text) ? "Unidad" : txtUnidad.Text.Trim(),
                CantidadSolicitada = numCantidad.Value,
                PrecioUnitario = numPrecio.Value,
                IdOrden = _ordenActual?.IdOrden ?? 0
            };

            if (_ordenActual != null)
                _log.AgregarDetalle(det);
            else
                _detallesTemp.Add(det);

            if (_ordenActual != null)
            {
                CargarDetallesGrid(_ordenActual.IdOrden);
            }
            else
            {
                dgvDetalle.Rows.Clear();
                decimal total = 0;
                foreach (var d in _detallesTemp)
                {
                    dgvDetalle.Rows.Add(d.NombreProducto, d.UnidadMedida, d.CantidadSolicitada, d.PrecioUnitario.ToString("F2"), d.Subtotal.ToString("F2"));
                    total += d.Subtotal;
                }
                lblTotal.Text = $"Total: {cboMoneda.Text} {total:N2}";
            }

            txtProducto.Clear(); txtUnidad.Clear(); numCantidad.Value = 1; numPrecio.Value = 0;
            txtProducto.Focus();
        }

        private void BtnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.SelectedRows.Count == 0) return;
            int idx = dgvDetalle.SelectedRows[0].Index;

            if (_ordenActual != null)
            {
                var detalles = _log.ListarDetalles(_ordenActual.IdOrden);
                if (idx < detalles.Count) _log.EliminarDetalle(detalles[idx].IdDetalle);
                CargarDetallesGrid(_ordenActual.IdOrden);
            }
            else
            {
                if (idx < _detallesTemp.Count) _detallesTemp.RemoveAt(idx);
                dgvDetalle.Rows.RemoveAt(idx);
            }
        }
    }
}