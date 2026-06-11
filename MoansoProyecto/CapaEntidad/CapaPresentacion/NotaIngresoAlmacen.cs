using CapaEntidad;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class NotaIngresoAlmacen : Form
    {
       

        // ── Lógica ───────────────────────────────────────────────────────────────
        private readonly logNotaIngresoInventario _log = new logNotaIngresoInventario();
        private entNotaIngresoInventario _notaActual = null;
        private bool _modoEdicion = false;

        // ── Controles ─────────────────────────────────────────────────────────────
        private DataGridView dgvNotas;
        private Button btnNuevo, btnEditar, btnConfirmar, btnDeshabilitar, btnSalir;

        // Cabecera
        private TextBox txtNumeroNota, txtNombreProveedor, txtResponsable, txtObservaciones;
        private ComboBox cboOrden;
        private DateTimePicker dtpFechaIngreso;
        private Label lblEstado;

        // Detalle productos
        private DataGridView dgvDetalle;
        private TextBox txtProducto, txtUnidad;
        private NumericUpDown numSolicitado, numRecibido;
        private ComboBox cboCondicion;
        private Button btnAgregarDet, btnQuitarDet;

        // Panel incidencias
        private DataGridView dgvIncidencias;
        private Button btnVerIncidencia;

        // ── Constructor ──────────────────────────────────────────────────────────
        public NotaIngresoAlmacen()
        {
            InitializeComponent();
            ConstruirUI();
            CargarNotas();
        }

        private void NotaIngresoAlmacen_Load(object sender, EventArgs e) { }

        // ════════════════════════════════════════════════════════════════════════
        //  DISEÑO
        // ════════════════════════════════════════════════════════════════════════
        private void ConstruirUI()
        {
            this.Text = "Nota de Ingreso al Inventario";
            this.Size = new Size(1150, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9f);
            this.MinimumSize = new Size(1050, 700);

            // ── Menú superior ─────────────────────────────────────────────────────
            var pnlMenu = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.FromArgb(30, 30, 30) };
            foreach (var (txt, x) in new[] { ("Proveedores", 10), ("Productos", 120), ("Órdenes Compra", 220) })
            {
                var b = new Button { Text = txt, Location = new Point(x, 7), Size = new Size(110, 26), FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.Transparent };
                b.FlatAppearance.BorderColor = Color.Gray;
                pnlMenu.Controls.Add(b);
            }

            // ── Panel izquierdo – lista ───────────────────────────────────────────
            var pnlLista = new Panel { Location = new Point(10, 50), Size = new Size(380, 660), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom };

            pnlLista.Controls.Add(new Label { Text = "Notas de Ingreso", Font = new Font("Segoe UI", 10f, FontStyle.Bold), Location = new Point(0, 0), Size = new Size(380, 22) });

            dgvNotas = new DataGridView
            {
                Location = new Point(0, 26),
                Size = new Size(380, 520),
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
            dgvNotas.SelectionChanged += DgvNotas_SelectionChanged;
            dgvNotas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", Width = 35 });
            dgvNotas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNum", HeaderText = "N° Nota", Width = 100 });
            dgvNotas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOC", HeaderText = "N° OC", Width = 100 });
            dgvNotas.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", Width = 100 });

            var pnlBotones = new FlowLayoutPanel { Location = new Point(0, 555), Size = new Size(380, 50), FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            btnNuevo = ABtn("NUEVO", Color.FromArgb(0, 122, 204));
            btnEditar = ABtn("EDITAR", Color.FromArgb(70, 130, 70));
            btnConfirmar = ABtn("CONFIRMAR", Color.FromArgb(0, 150, 136));
            btnDeshabilitar = ABtn("ANULAR", Color.FromArgb(180, 60, 60));
            btnSalir = ABtn("SALIR", Color.FromArgb(100, 100, 100));
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnDeshabilitar.Click += BtnDeshabilitar_Click;
            btnSalir.Click += (s, e) => this.Close();
            pnlBotones.Controls.AddRange(new Control[] { btnNuevo, btnEditar, btnConfirmar, btnDeshabilitar, btnSalir });

            pnlLista.Controls.AddRange(new Control[] { dgvNotas, pnlBotones });

            // ── Panel derecho – detalle ───────────────────────────────────────────
            var pnlDetalle = new Panel
            {
                Location = new Point(400, 50),
                Size = new Size(730, 660),
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 10;

            // N° Nota + Estado
            pnlDetalle.Controls.Add(L("N° Nota:", 10, y));
            txtNumeroNota = T(90, y, 140, true); pnlDetalle.Controls.Add(txtNumeroNota);
            lblEstado = new Label { Text = "Estado: —", Location = new Point(250, y + 3), Size = new Size(220, 20), Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            pnlDetalle.Controls.Add(lblEstado);

            // Orden de compra origen
            y += 34;
            pnlDetalle.Controls.Add(L("Orden de Compra:", 10, y));
            cboOrden = new ComboBox { Location = new Point(130, y), Size = new Size(260, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            pnlDetalle.Controls.Add(cboOrden);

            // Proveedor (readonly, se llena al elegir OC)
            y += 32;
            pnlDetalle.Controls.Add(L("Proveedor:", 10, y));
            txtNombreProveedor = T(90, y, 280, true); pnlDetalle.Controls.Add(txtNombreProveedor);

            // Fecha + Responsable
            y += 32;
            pnlDetalle.Controls.Add(L("F. Ingreso:", 10, y));
            dtpFechaIngreso = new DateTimePicker { Location = new Point(90, y), Size = new Size(140, 24), Format = DateTimePickerFormat.Short };
            pnlDetalle.Controls.Add(dtpFechaIngreso);
            pnlDetalle.Controls.Add(L("Responsable:", 250, y));
            txtResponsable = T(340, y, 200); pnlDetalle.Controls.Add(txtResponsable);

            // Observaciones
            y += 32;
            pnlDetalle.Controls.Add(L("Observaciones:", 10, y));
            txtObservaciones = new TextBox { Location = new Point(110, y), Size = new Size(590, 22), MaxLength = 300 };
            pnlDetalle.Controls.Add(txtObservaciones);

            // ── Separador ─────────────────────────────────────────────────────────
            y += 32;
            pnlDetalle.Controls.Add(new Label { Location = new Point(10, y), Size = new Size(700, 2), BackColor = Color.LightGray });
            y += 8;
            pnlDetalle.Controls.Add(new Label { Text = "Verificación de Productos Recibidos", Font = new Font("Segoe UI", 9f, FontStyle.Bold), Location = new Point(10, y), Size = new Size(350, 20) });

            // Fila de ingreso de producto
            y += 28;
            pnlDetalle.Controls.Add(L("Producto:", 10, y));
            txtProducto = T(80, y, 160); pnlDetalle.Controls.Add(txtProducto);
            pnlDetalle.Controls.Add(L("UM:", 252, y));
            txtUnidad = T(272, y, 60); pnlDetalle.Controls.Add(txtUnidad);
            pnlDetalle.Controls.Add(L("Solic.:", 344, y));
            numSolicitado = new NumericUpDown { Location = new Point(382, y), Size = new Size(70, 24), Minimum = 0, Maximum = 99999, Value = 0 };
            pnlDetalle.Controls.Add(numSolicitado);
            pnlDetalle.Controls.Add(L("Recib.:", 462, y));
            numRecibido = new NumericUpDown { Location = new Point(500, y), Size = new Size(70, 24), Minimum = 0, Maximum = 99999, Value = 0 };
            pnlDetalle.Controls.Add(numRecibido);

            y += 28;
            pnlDetalle.Controls.Add(L("Condición:", 10, y));
            cboCondicion = new ComboBox { Location = new Point(80, y), Size = new Size(120, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cboCondicion.Items.AddRange(new object[] { "Bueno", "Dañado", "Incompleto" });
            cboCondicion.SelectedIndex = 0;
            pnlDetalle.Controls.Add(cboCondicion);

            btnAgregarDet = new Button { Text = "+ Agregar", Location = new Point(215, y - 2), Size = new Size(100, 26), BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnQuitarDet = new Button { Text = "- Quitar", Location = new Point(325, y - 2), Size = new Size(100, 26), BackColor = Color.FromArgb(180, 60, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAgregarDet.Click += BtnAgregarDet_Click;
            btnQuitarDet.Click += BtnQuitarDet_Click;
            pnlDetalle.Controls.AddRange(new Control[] { btnAgregarDet, btnQuitarDet });

            // Grid detalle
            y += 34;
            dgvDetalle = new DataGridView
            {
                Location = new Point(10, y),
                Size = new Size(700, 155),
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
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSolic", HeaderText = "Solicitado", Width = 80 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRecib", HeaderText = "Recibido", Width = 80 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDif", HeaderText = "Diferencia", Width = 80 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCondicion", HeaderText = "Condición", Width = 90 });
            pnlDetalle.Controls.Add(dgvDetalle);

            // ── Sección Incidencias ───────────────────────────────────────────────
            y += 165;
            pnlDetalle.Controls.Add(new Label { Location = new Point(10, y), Size = new Size(700, 2), BackColor = Color.LightGray });
            y += 8;
            pnlDetalle.Controls.Add(new Label { Text = "Incidencias Registradas", Font = new Font("Segoe UI", 9f, FontStyle.Bold), Location = new Point(10, y), Size = new Size(250, 20) });

            btnVerIncidencia = new Button { Text = "+ Registrar Incidencia", Location = new Point(430, y - 2), Size = new Size(180, 26), BackColor = Color.FromArgb(200, 80, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnVerIncidencia.Click += BtnVerIncidencia_Click;
            pnlDetalle.Controls.Add(btnVerIncidencia);

            y += 28;
            dgvIncidencias = new DataGridView
            {
                Location = new Point(10, y),
                Size = new Size(700, 80),
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
            dgvIncidencias.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTipo", HeaderText = "Tipo", Width = 160 });
            dgvIncidencias.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDesc", HeaderText = "Descripción" });
            dgvIncidencias.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAccion", HeaderText = "Acción", Width = 180 });
            dgvIncidencias.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEst", HeaderText = "Estado", Width = 90 });
            pnlDetalle.Controls.Add(dgvIncidencias);

            // Ensamblar
            this.Controls.AddRange(new Control[] { pnlMenu, pnlLista, pnlDetalle });

            // Evento al cambiar orden
            cboOrden.SelectedIndexChanged += (s, e) => {
                if (cboOrden.SelectedItem is entOrdenCompraImportaciones oc)
                    txtNombreProveedor.Text = oc.NombreProveedor;
            };

            ModoVista();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  HELPERS
        // ════════════════════════════════════════════════════════════════════════
        private Button ABtn(string txt, Color c) =>
            new Button { Text = txt, Size = new Size(75, 28), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(2) };
        private Label L(string txt, int x, int y) =>
            new Label { Text = txt, Location = new Point(x, y + 3), AutoSize = true };
        private TextBox T(int x, int y, int w, bool ro = false) =>
            new TextBox { Location = new Point(x, y), Size = new Size(w, 22), ReadOnly = ro, BackColor = ro ? Color.WhiteSmoke : Color.White };

        // ════════════════════════════════════════════════════════════════════════
        //  CARGA
        // ════════════════════════════════════════════════════════════════════════
        private void CargarNotas()
        {
            dgvNotas.Rows.Clear();
            foreach (var n in _log.Listar())
            {
                dgvNotas.Rows.Add(n.IdNota, n.NumeroNota, n.NumeroOrden, n.Estado);
                dgvNotas.Rows[dgvNotas.Rows.Count - 1].DefaultCellStyle.BackColor = ColorEstado(n.Estado);
            }
        }

        private void CargarOrdenesCombo()
        {
            cboOrden.Items.Clear();
            foreach (var oc in _log.ListarOrdenesAprobadas())
                cboOrden.Items.Add(oc);
            cboOrden.DisplayMember = "NumeroOrden";
        }

        private Color ColorEstado(string e)
        {
            switch (e)
            {
                case "Conforme": return Color.FromArgb(220, 255, 220);
                case "Con Incidencia": return Color.FromArgb(255, 230, 200);
                case "Pendiente": return Color.FromArgb(255, 255, 210);
                default: return Color.White;
            }
        }

        private void DgvNotas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNotas.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvNotas.SelectedRows[0].Cells["colId"].Value);
            _notaActual = _log.ObtenerPorId(id);
            MostrarNota(_notaActual);
            CargarDetallesGrid(id);
            CargarIncidenciasGrid(id);
            ModoVista();
        }

        private void MostrarNota(entNotaIngresoInventario n)
        {
            if (n == null) return;
            txtNumeroNota.Text = n.NumeroNota;
            txtNombreProveedor.Text = n.NombreProveedor;
            dtpFechaIngreso.Value = n.FechaIngreso;
            txtResponsable.Text = n.ResponsableAlmacen;
            txtObservaciones.Text = n.Observaciones;
            lblEstado.Text = $"Estado: {n.Estado}";
            lblEstado.ForeColor = n.Estado == "Conforme" ? Color.DarkGreen
                                    : n.Estado == "Con Incidencia" ? Color.OrangeRed
                                    : Color.DimGray;
        }

        private void CargarDetallesGrid(int idNota)
        {
            dgvDetalle.Rows.Clear();
            foreach (var d in _log.ListarDetalles(idNota))
            {
                int rowIdx = dgvDetalle.Rows.Add(d.NombreProducto, d.UnidadMedida, d.CantidadSolicitada, d.CantidadRecibida, d.Diferencia, d.CondicionProducto);
                // Color por condición
                var row = dgvDetalle.Rows[rowIdx];
                if (d.CondicionProducto == "Dañado")
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (d.CondicionProducto == "Incompleto" || d.Diferencia != 0)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
            }
        }

        private void CargarIncidenciasGrid(int idNota)
        {
            dgvIncidencias.Rows.Clear();
            foreach (var i in _log.ListarIncidencias(idNota))
                dgvIncidencias.Rows.Add(i.TipoIncidencia, i.Descripcion, i.AccionTomada, i.Estado);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  MODOS
        // ════════════════════════════════════════════════════════════════════════
        private void ModoVista()
        {
            _modoEdicion = false;
            bool hay = _notaActual != null;
            string est = hay ? _notaActual.Estado : "";

            cboOrden.Enabled = false;
            txtResponsable.ReadOnly = true;
            txtObservaciones.ReadOnly = true;
            dtpFechaIngreso.Enabled = false;
            txtProducto.Enabled = false;
            txtUnidad.Enabled = false;
            numSolicitado.Enabled = false;
            numRecibido.Enabled = false;
            cboCondicion.Enabled = false;
            btnAgregarDet.Enabled = false;
            btnQuitarDet.Enabled = false;

            btnEditar.Enabled = hay && est == "Pendiente";
            btnConfirmar.Enabled = hay && est == "Pendiente";
            btnDeshabilitar.Enabled = hay && est == "Pendiente";
            btnVerIncidencia.Enabled = hay && (est == "Con Incidencia" || est == "Pendiente");
        }

        private void ModoEdicion()
        {
            _modoEdicion = true;
            cboOrden.Enabled = _notaActual == null; // solo en nuevo
            txtResponsable.ReadOnly = false;
            txtObservaciones.ReadOnly = false;
            dtpFechaIngreso.Enabled = true;
            txtProducto.Enabled = true;
            txtUnidad.Enabled = true;
            numSolicitado.Enabled = true;
            numRecibido.Enabled = true;
            cboCondicion.Enabled = true;
            btnAgregarDet.Enabled = true;
            btnQuitarDet.Enabled = true;
            btnConfirmar.Enabled = false;
        }

        private void LimpiarFormulario()
        {
            txtNumeroNota.Text = "(generado al guardar)";
            txtNombreProveedor.Text = "";
            dtpFechaIngreso.Value = DateTime.Today;
            txtResponsable.Text = "";
            txtObservaciones.Text = "";
            lblEstado.Text = "Estado: Pendiente";
            lblEstado.ForeColor = Color.DimGray;
            dgvDetalle.Rows.Clear();
            dgvIncidencias.Rows.Clear();
            txtProducto.Text = "";
            txtUnidad.Text = "";
            numSolicitado.Value = 0;
            numRecibido.Value = 0;
            cboCondicion.SelectedIndex = 0;
        }

        // ════════════════════════════════════════════════════════════════════════
        //  EVENTOS – CRUD
        // ════════════════════════════════════════════════════════════════════════
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            _notaActual = null;
            LimpiarFormulario();
            CargarOrdenesCombo();
            ModoEdicion();
            cboOrden.Focus();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (_notaActual == null) { MessageBox.Show("Seleccione una nota para editar.", "Aviso"); return; }
            ModoEdicion();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion) return;
            if (string.IsNullOrWhiteSpace(txtResponsable.Text)) { MessageBox.Show("Ingrese el responsable de almacén.", "Validación"); return; }

            if (_notaActual == null)
            {
                if (cboOrden.SelectedItem == null) { MessageBox.Show("Seleccione la Orden de Compra.", "Validación"); return; }
                var oc = (entOrdenCompraImportaciones)cboOrden.SelectedItem;
                var nueva = new entNotaIngresoInventario
                {
                    IdOrdenCompra = oc.IdOrden,
                    NumeroOrden = oc.NumeroOrden,
                    NombreProveedor = oc.NombreProveedor,
                    FechaIngreso = dtpFechaIngreso.Value,
                    ResponsableAlmacen = txtResponsable.Text.Trim(),
                    Observaciones = txtObservaciones.Text.Trim()
                };
                _log.Registrar(nueva);
                MessageBox.Show("Nota de ingreso creada en estado Pendiente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _notaActual.ResponsableAlmacen = txtResponsable.Text.Trim();
                _notaActual.FechaIngreso = dtpFechaIngreso.Value;
                _notaActual.Observaciones = txtObservaciones.Text.Trim();
                _log.Actualizar(_notaActual);
                MessageBox.Show("Nota actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarNotas();
            ModoVista();
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (_notaActual == null) return;

            // Guardar primero si estaba editando
            if (_modoEdicion) BtnGuardar_Click(sender, e);

            var (ok, msg, tieneInc) = _log.ConfirmarIngreso(_notaActual.IdNota);

            var icon = tieneInc ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
            MessageBox.Show(msg, ok ? "Ingreso Confirmado" : "Error", MessageBoxButtons.OK, icon);

            if (ok)
            {
                _notaActual = _log.ObtenerPorId(_notaActual.IdNota);
                MostrarNota(_notaActual);
                CargarNotas();

                // Si hay incidencia, ofrece registrarla ahora
                if (tieneInc)
                {
                    if (MessageBox.Show("¿Desea registrar la incidencia ahora?", "Incidencia detectada", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        AbrirFormIncidencia();
                }

                ModoVista();
            }
        }

        private void BtnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_notaActual == null) return;
            if (MessageBox.Show($"¿Anular la nota {_notaActual.NumeroNota}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _log.Deshabilitar(_notaActual.IdNota);
                _notaActual = null;
                LimpiarFormulario();
                CargarNotas();
                ModoVista();
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  EVENTOS – DETALLE
        // ════════════════════════════════════════════════════════════════════════
        private void BtnAgregarDet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProducto.Text)) { MessageBox.Show("Ingrese el nombre del producto."); return; }

            var det = new entDetalleNotaIngreso
            {
                NombreProducto = txtProducto.Text.Trim(),
                UnidadMedida = string.IsNullOrWhiteSpace(txtUnidad.Text) ? "Unidad" : txtUnidad.Text.Trim(),
                CantidadSolicitada = numSolicitado.Value,
                CantidadRecibida = numRecibido.Value,
                CondicionProducto = cboCondicion.Text,
                IdNota = _notaActual?.IdNota ?? 0
            };

            if (_notaActual != null)
            {
                _log.AgregarDetalle(det);
                CargarDetallesGrid(_notaActual.IdNota);
            }
            else
            {
                // Modo nuevo: agregar a grilla temporalmente
                decimal dif = det.CantidadRecibida - det.CantidadSolicitada;
                int row = dgvDetalle.Rows.Add(det.NombreProducto, det.UnidadMedida, det.CantidadSolicitada, det.CantidadRecibida, dif, det.CondicionProducto);
                if (det.CondicionProducto == "Dañado")
                    dgvDetalle.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (det.CondicionProducto == "Incompleto" || dif != 0)
                    dgvDetalle.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
            }

            txtProducto.Clear(); txtUnidad.Clear();
            numSolicitado.Value = 0; numRecibido.Value = 0;
            cboCondicion.SelectedIndex = 0;
            txtProducto.Focus();
        }

        private void BtnQuitarDet_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.SelectedRows.Count == 0) return;
            int idx = dgvDetalle.SelectedRows[0].Index;
            if (_notaActual != null)
            {
                var lista = _log.ListarDetalles(_notaActual.IdNota);
                if (idx < lista.Count) _log.EliminarDetalle(lista[idx].IdDetalle);
                CargarDetallesGrid(_notaActual.IdNota);
            }
            else
            {
                dgvDetalle.Rows.RemoveAt(idx);
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  INCIDENCIA
        // ════════════════════════════════════════════════════════════════════════
        private void BtnVerIncidencia_Click(object sender, EventArgs e)
        {
            if (_notaActual == null) return;
            AbrirFormIncidencia();
        }

        private void AbrirFormIncidencia()
        {
            var frmInc = new RegistrarIncidencia(_notaActual, _log);
            frmInc.ShowDialog(this);
            // Refrescar incidencias al cerrar
            CargarIncidenciasGrid(_notaActual.IdNota);
        }
    }

    // ══════════════════════════════════════════════════════════════════════════
    //  FORMULARIO SECUNDARIO – REGISTRAR / VER INCIDENCIA
    // ══════════════════════════════════════════════════════════════════════════
    public class RegistrarIncidencia : Form
    {
        private readonly entNotaIngresoInventario _nota;
        private readonly logNotaIngresoInventario _log;

        private ComboBox cboTipo, cboEstado;
        private TextBox txtDescripcion, txtAccion;

        public RegistrarIncidencia(entNotaIngresoInventario nota, logNotaIngresoInventario log)
        {
            _nota = nota;
            _log = log;
            ConstruirUI();
        }

        private void ConstruirUI()
        {
            this.Text = $"Registrar Incidencia — {_nota.NumeroNota}";
            this.Size = new Size(500, 340);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9f);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int y = 20;
            Controls.Add(Lbl("Nota:", 20, y));
            Controls.Add(new TextBox { Location = new Point(120, y), Size = new Size(340, 22), Text = _nota.NumeroNota, ReadOnly = true, BackColor = Color.WhiteSmoke });

            y += 34;
            Controls.Add(Lbl("Tipo de Incidencia:", 20, y));
            cboTipo = new ComboBox { Location = new Point(160, y), Size = new Size(200, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cboTipo.Items.AddRange(new object[] { "Diferencia de Cantidad", "Producto Dañado", "Ambos" });
            cboTipo.SelectedIndex = 0;
            Controls.Add(cboTipo);

            y += 34;
            Controls.Add(Lbl("Descripción:", 20, y));
            txtDescripcion = new TextBox { Location = new Point(120, y), Size = new Size(340, 60), Multiline = true, MaxLength = 500 };
            Controls.Add(txtDescripcion);

            y += 70;
            Controls.Add(Lbl("Acción a tomar:", 20, y));
            txtAccion = new TextBox { Location = new Point(120, y), Size = new Size(340, 22), MaxLength = 300 };
            Controls.Add(txtAccion);

            y += 34;
            Controls.Add(Lbl("Estado:", 20, y));
            cboEstado = new ComboBox { Location = new Point(120, y), Size = new Size(160, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cboEstado.Items.AddRange(new object[] { "Pendiente", "En Proceso", "Resuelta" });
            cboEstado.SelectedIndex = 0;
            Controls.Add(cboEstado);

            y += 44;
            var btnGuardar = new Button { Text = "💾 Guardar Incidencia", Location = new Point(120, y), Size = new Size(180, 34), BackColor = Color.FromArgb(200, 80, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            var btnCerrar = new Button { Text = "Cerrar", Location = new Point(315, y), Size = new Size(100, 34), BackColor = Color.FromArgb(100, 100, 100), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnGuardar.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text)) { MessageBox.Show("Ingrese la descripción de la incidencia."); return; }
                var inc = new entIncidencia
                {
                    IdNota = _nota.IdNota,
                    NumeroNota = _nota.NumeroNota,
                    FechaIncidencia = DateTime.Today,
                    TipoIncidencia = cboTipo.Text,
                    Descripcion = txtDescripcion.Text.Trim(),
                    AccionTomada = txtAccion.Text.Trim(),
                    Estado = cboEstado.Text
                };
                _log.RegistrarIncidencia(inc);
                MessageBox.Show("Incidencia registrada. El proveedor será contactado para el reclamo.", "Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
            btnCerrar.Click += (s, e) => this.Close();
            Controls.AddRange(new Control[] { btnGuardar, btnCerrar });
        }

        private Label Lbl(string txt, int x, int y) =>
            new Label { Text = txt, Location = new Point(x, y + 3), AutoSize = true };
    }



}
