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
    public partial class MantenedorCliente : Form
    {
        logCliente logC = new logCliente();
        int idSeleccionado = 0;

        public MantenedorCliente()
        {
            InitializeComponent();
            CargarGrid();
            ModoVista();
        }

        void CargarGrid()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = logC.Listar();
        }

        void ModoVista()
        {
            txtCliente.Clear();
            txtRazonSocial.Clear();
            txtTipoCliente.Clear();
            txtCiudad.Clear();
            chkEstado.Checked = false;
            dtpFecha.Value = DateTime.Now;

            txtCliente.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtTipoCliente.Enabled = false;
            txtCiudad.Enabled = false;
            dtpFecha.Enabled = false;

            btnAgregar.Visible = false;
            btnModificar.Visible = false;
            btnCancelar.Visible = false;

            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnDeshabilitar.Enabled = true;
            idSeleccionado = 0;
        }

        void ModoEdicion()
        {
            txtCliente.Enabled = true;
            txtRazonSocial.Enabled = true;
            txtTipoCliente.Enabled = true;
            txtCiudad.Enabled = true;
            dtpFecha.Enabled = true;

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnCancelar.Visible = true;
        }

        private void dgvClientes_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;
            var fila = dgvClientes.CurrentRow;

            idSeleccionado = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            txtCliente.Text = fila.Cells["NombreCliente"].Value.ToString();
            txtRazonSocial.Text = fila.Cells["RazonSocial"].Value.ToString();
            txtTipoCliente.Text = fila.Cells["TipoCliente"].Value.ToString();
            txtCiudad.Text = fila.Cells["Ciudad"].Value.ToString();
            chkEstado.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);
            dtpFecha.Value = Convert.ToDateTime(fila.Cells["FechaRegistro"].Value);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoEdicion();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            dtpFecha.Value = DateTime.Now;
            txtCliente.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un cliente primero."); return; }
            ModoEdicion();
            btnModificar.Visible = true;
            btnAgregar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            { MessageBox.Show("El nombre es obligatorio."); return; }

            var c = new entCliente
            {
                NombreCliente = txtCliente.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                TipoCliente = txtTipoCliente.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logC.Registrar(c))
            {
                MessageBox.Show("Cliente registrado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            { MessageBox.Show("El nombre es obligatorio."); return; }

            var c = new entCliente
            {
                IdCliente = idSeleccionado,
                NombreCliente = txtCliente.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                TipoCliente = txtTipoCliente.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logC.Modificar(c))
            {
                MessageBox.Show("Cliente modificado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un cliente primero."); return; }

            var res = MessageBox.Show("¿Deshabilitar este cliente?", "Confirmar",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (logC.Deshabilitar(idSeleccionado))
                {
                    MessageBox.Show("Cliente deshabilitado.");
                    CargarGrid();
                    ModoVista();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoVista();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MantenedorCliente_Load(object sender, EventArgs e)
        {

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            MantenedorProveedor frmProveedor = new MantenedorProveedor();
            frmProveedor.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            MantenedorProducto frmProducto = new MantenedorProducto();
            frmProducto.ShowDialog();
        }

        private void btnProformas_Click(object sender, EventArgs e)
        {
            MantenedorProforma frmProforma = new MantenedorProforma();
            frmProforma.ShowDialog();
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            MantenedorVehiculo frmVehiculo = new MantenedorVehiculo();
            frmVehiculo.ShowDialog();
        }

        
    }
}