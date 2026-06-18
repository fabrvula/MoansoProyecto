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
    public partial class MantenedorVehiculo : Form
    {
        logVehiculo logV = new logVehiculo();
        logCliente logC = new logCliente();
        int idSeleccionado = 0;

        private void MantenedorVehiculo_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public MantenedorVehiculo()
        {
            InitializeComponent();
            CargarClientes();
            CargarGrid();
            ModoVista();
        }

        void CargarClientes()
        {
            cboCliente.DataSource = null;
            cboCliente.DataSource = logC.Listar();
            cboCliente.DisplayMember = "NombreCliente";
            cboCliente.ValueMember = "IdCliente";
        }

        void CargarGrid()
        {
            dgvVehiculos.DataSource = null;
            dgvVehiculos.DataSource = logV.Listar();
        }

        void ModoVista()
        {
            txtMarca.Clear();
            txtModelo.Clear();
            txtAnio.Clear();
            txtPlaca.Clear();
            txtColor.Clear();
            chkEstado.Checked = false;
            dtpFecha.Value = DateTime.Now;

            cboCliente.Enabled = false;
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            txtAnio.Enabled = false;
            txtPlaca.Enabled = false;
            txtColor.Enabled = false;
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
            cboCliente.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtAnio.Enabled = true;
            txtPlaca.Enabled = true;
            txtColor.Enabled = true;
            dtpFecha.Enabled = true;

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnCancelar.Visible = true;
        }

        private void dgvVehiculos_Click(object sender, EventArgs e)
        {
            if (dgvVehiculos.CurrentRow == null) return;
            var fila = dgvVehiculos.CurrentRow;

            idSeleccionado = Convert.ToInt32(fila.Cells["IdVehiculo"].Value);
            cboCliente.SelectedValue = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            txtMarca.Text = fila.Cells["Marca"].Value.ToString();
            txtModelo.Text = fila.Cells["Modelo"].Value.ToString();
            txtAnio.Text = fila.Cells["Anio"].Value.ToString();
            txtPlaca.Text = fila.Cells["Placa"].Value.ToString();
            txtColor.Text = fila.Cells["Color"].Value.ToString();
            chkEstado.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);
            dtpFecha.Value = Convert.ToDateTime(fila.Cells["FechaRegistro"].Value);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoEdicion();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            dtpFecha.Value = DateTime.Now;
            txtMarca.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un vehículo primero."); return; }
            ModoEdicion();
            btnModificar.Visible = true;
            btnAgregar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            { MessageBox.Show("La placa es obligatoria."); return; }

            if (cboCliente.SelectedValue == null)
            { MessageBox.Show("Selecciona un cliente."); return; }

            var clienteSeleccionado = (entCliente)cboCliente.SelectedItem;

            var v = new entVehiculo
            {
                IdCliente = Convert.ToInt32(cboCliente.SelectedValue),
                NombreCliente = clienteSeleccionado.NombreCliente,
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Anio = Convert.ToInt32(txtAnio.Text.Trim()),
                Placa = txtPlaca.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logV.Registrar(v))
            {
                MessageBox.Show("Vehículo registrado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            { MessageBox.Show("La placa es obligatoria."); return; }

            var clienteSeleccionado = (entCliente)cboCliente.SelectedItem;

            var v = new entVehiculo
            {
                IdVehiculo = idSeleccionado,
                IdCliente = Convert.ToInt32(cboCliente.SelectedValue),
                NombreCliente = clienteSeleccionado.NombreCliente,
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Anio = Convert.ToInt32(txtAnio.Text.Trim()),
                Placa = txtPlaca.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logV.Modificar(v))
            {
                MessageBox.Show("Vehículo modificado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un vehículo primero."); return; }

            var res = MessageBox.Show("¿Deshabilitar este vehículo?", "Confirmar",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (logV.Deshabilitar(idSeleccionado))
                {
                    MessageBox.Show("Vehículo deshabilitado.");
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
    }
}
