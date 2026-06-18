    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using CapaEntidad;
    using CapaLogica;


    namespace CapaPresentacion
    {
        public partial class MantenedorProforma : Form
        {
            logProforma logP = new logProforma();
            logCliente logC = new logCliente();
            logProducto logPro = new logProducto();

            entCliente clienteSeleccionado = null;
            List<entDetalleProforma> detalles = new List<entDetalleProforma>();
            int idDetalleCorrelativo = 1;

            public MantenedorProforma()
            {
                InitializeComponent();
                CargarProductos();
                CargarGridProformas();
                ModoVista();
            }

        
            void CargarProductos()
            {
                cboProducto.DataSource = null;
                cboProducto.DataSource = logPro.Listar();
                cboProducto.DisplayMember = "Descripcion";
                cboProducto.ValueMember = "IdProducto";
            }

            void CargarGridProformas()
            {
                dgvProformas.DataSource = null;
                dgvProformas.DataSource = logP.Listar();
            }

            void CargarGridDetalle()
            {
                dgvDetalle.DataSource = null;
                dgvDetalle.DataSource = new List<entDetalleProforma>(detalles);
                ActualizarTotal();
            }

            void ActualizarTotal()
            {
                decimal total = 0;
                foreach (var d in detalles)
                    total += d.Subtotal;
                lblTotal.Text = "Total: S/" + total.ToString("N2");
            }


            void ModoVista()
            {
                txtBuscarCliente.Clear();
                txtCantidad.Clear();
                txtObservaciones.Clear();
                lblNombreCliente.Text = "Cliente: —";
                lblCiudad.Text = "Ciudad: —";
                lblTotal.Text = "Total: S/0.00";
                detalles.Clear();
                CargarGridDetalle();
                clienteSeleccionado = null;

                txtBuscarCliente.Enabled = false;
                btnBuscarCliente.Enabled = false;
                cboProducto.Enabled = false;
                txtCantidad.Enabled = false;
                btnAgregarProducto.Enabled = false;
                txtObservaciones.Enabled = false;
                btnGuardar.Enabled = false;
                ///btnAnular.Enabled = false;

                btnNuevaProforma.Enabled = true;
            }

            void ModoEdicion()
            {
                txtBuscarCliente.Enabled = true;
                btnBuscarCliente.Enabled = true;
                cboProducto.Enabled = true;
                txtCantidad.Enabled = true;
                btnAgregarProducto.Enabled = true;
                txtObservaciones.Enabled = true;
                btnGuardar.Enabled = true;

                btnNuevaProforma.Enabled = false;
            }

        
            private void btnBuscarCliente_Click(object sender, EventArgs e)
            {
                string buscar = txtBuscarCliente.Text.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(buscar))
                { MessageBox.Show("Escribe el nombre del cliente."); return; }

                var clientes = logC.Listar();
                var encontrado = clientes.Find(x =>
                    x.NombreCliente.ToLower().Contains(buscar));

                if (encontrado != null)
                {
                    clienteSeleccionado = encontrado;
                    lblNombreCliente.Text = "Cliente: " + encontrado.NombreCliente;
                    lblCiudad.Text = "Ciudad: " + encontrado.Ciudad;
                }
                else
                {
                    var res = MessageBox.Show(
                        "Cliente no encontrado. ¿Desea registrarlo?",
                        "Nuevo cliente",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        MantenedorCliente frmCliente = new MantenedorCliente();
                        frmCliente.ShowDialog();
                        // busca de nuevo después de registrar
                        clientes = logC.Listar();
                        encontrado = clientes.Find(x =>
                            x.NombreCliente.ToLower().Contains(buscar));
                        if (encontrado != null)
                        {
                            clienteSeleccionado = encontrado;
                            lblNombreCliente.Text = "Cliente: " + encontrado.NombreCliente;
                            lblCiudad.Text = "Ciudad: " + encontrado.Ciudad;
                        }
                    }
                }
            }

        
            private void btnAgregarProducto_Click(object sender, EventArgs e)
            {
                if (cboProducto.SelectedItem == null)
                { MessageBox.Show("Selecciona un producto."); return; }

                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                { MessageBox.Show("Ingresa la cantidad."); return; }

                var producto = (entProducto)cboProducto.SelectedItem;
                int cantidad = Convert.ToInt32(txtCantidad.Text.Trim());

                if (cantidad <= 0)
                { MessageBox.Show("La cantidad debe ser mayor a 0."); return; }

                var detalle = new entDetalleProforma
                {
                    IdDetalle = idDetalleCorrelativo++,
                    IdProducto = producto.IdProducto,
                    CodigoProducto = producto.Codigo,
                    DescripcionProducto = producto.Descripcion,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = cantidad * producto.Precio
                };

                detalles.Add(detalle);
                CargarGridDetalle();
                txtCantidad.Clear();
            }

            // ── Guardar Proforma ─────────────────────────────────
            private void btnGuardar_Click(object sender, EventArgs e)
            {
                if (clienteSeleccionado == null)
                { MessageBox.Show("Selecciona un cliente primero."); return; }

                if (detalles.Count == 0)
                { MessageBox.Show("Agrega al menos un producto."); return; }

                decimal total = 0;
                foreach (var d in detalles) total += d.Subtotal;

                var proforma = new entProforma
                {
                    Fecha = DateTime.Now,
                    IdCliente = clienteSeleccionado.IdCliente,
                    NombreCliente = clienteSeleccionado.NombreCliente,
                    Ciudad = clienteSeleccionado.Ciudad,
                    Estado = "Pendiente",
                    Total = total,
                    Observaciones = txtObservaciones.Text.Trim(),
                    Detalles = new List<entDetalleProforma>(detalles)
                };

                if (logP.Registrar(proforma))
                {
                    MessageBox.Show("✅ Proforma " + proforma.NroProforma +
                                    " registrada correctamente.");
                    CargarGridProformas();
                    ModoVista();
                }
            }

        
            private void btnAnular_Click(object sender, EventArgs e)
            {
                if (dgvProformas.CurrentRow == null)
                { MessageBox.Show("Selecciona una proforma."); return; }

                int id = Convert.ToInt32(dgvProformas.CurrentRow.Cells["IdProforma"].Value);

                var res = MessageBox.Show("¿Anular esta proforma?", "Confirmar",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (logP.CambiarEstado(id, "Anulada"))
                    {
                        MessageBox.Show("Proforma anulada.");
                        CargarGridProformas();
                    }
                }
            }

        
            private void btnNuevaProforma_Click(object sender, EventArgs e)
            {
                ModoEdicion();
                detalles.Clear();
                CargarGridDetalle();
                txtBuscarCliente.Focus();
            }

       
            private void dgvProformas_Click(object sender, EventArgs e)
            {
                    btnAnular.Enabled = dgvProformas.CurrentRow != null;
            }

        
            private void btnSalir_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btnEliminarDetalle_Click(object sender, EventArgs e)
            {
                if (dgvDetalle.CurrentRow == null)
                { MessageBox.Show("Selecciona un producto del detalle."); return; }

                int idDetalle = Convert.ToInt32(dgvDetalle.CurrentRow.Cells["IdDetalle"].Value);

                var res = MessageBox.Show("¿Eliminar este producto de la proforma?",
                              "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    detalles.RemoveAll(x => x.IdDetalle == idDetalle);
                    CargarGridDetalle();
                    MessageBox.Show("Producto eliminado del detalle.");
                }
            }

            private void MantenedorProforma_Load(object sender, EventArgs e)
            {

            }
        }
    }