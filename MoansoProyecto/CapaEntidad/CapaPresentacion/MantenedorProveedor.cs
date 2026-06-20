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
    public partial class MantenedorProveedor : Form
    {
        logProveedor logP = new logProveedor();
        int idSeleccionado = 0;

        public MantenedorProveedor()
        {
            InitializeComponent();
            CargarGrid();
            ModoVista();
        }

        void CargarGrid()
        {
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = logP.Listar();
        }

        void ModoVista()
        {
            txtNombreProveedor.Clear();
            txtRazonSocial.Clear();
            txtRuc.Clear();
            txtContacto.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtPais.Clear();
            chkEstado.Checked = false;
            dtpFecha.Value = DateTime.Now;

            txtNombreProveedor.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtRuc.Enabled = false;
            txtContacto.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
            txtPais.Enabled = false;
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
            txtNombreProveedor.Enabled = true;
            txtRazonSocial.Enabled = true;
            txtRuc.Enabled = true;
            txtContacto.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            txtPais.Enabled = true;
            dtpFecha.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnCancelar.Visible = true;

            
        }









        private void dgvProveedores_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.CurrentRow == null) return;
            var fila = dgvProveedores.CurrentRow;
           
            idSeleccionado = Convert.ToInt32(fila.Cells["IdProveedor"].Value);
            txtNombreProveedor.Text = fila.Cells["NombreProveedor"].Value.ToString();
            txtRazonSocial.Text = fila.Cells["RazonSocial"].Value.ToString();
            txtRuc.Text = fila.Cells["Ruc"].Value.ToString();
            txtContacto.Text = fila.Cells["Contacto"].Value.ToString();
            txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
            txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
            txtPais.Text = fila.Cells["Pais"].Value.ToString();
           

            chkEstado.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);
            dtpFecha.Value = Convert.ToDateTime(fila.Cells["FechaRegistro"].Value);

           
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoEdicion();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            dtpFecha.Value = DateTime.Now;
            txtNombreProveedor.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un proveedor primero."); return; }
            ModoEdicion();
            btnModificar.Visible = true;
            btnAgregar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
            { MessageBox.Show("El nombre del proveedor es obligatorio."); return; }

            var p = new entProveedor
            {
                NombreProveedor = txtNombreProveedor.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                Ruc = txtRuc.Text.Trim(),
                Contacto = txtContacto.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Pais = txtPais.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value,
                
            };

            if (logP.Registrar(p))
            {
                MessageBox.Show("Proveedor registrado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
            { MessageBox.Show("El nombre del proveedor es obligatorio."); return; }

            var p = new entProveedor
            {
                IdProveedor = idSeleccionado,
                NombreProveedor = txtNombreProveedor.Text.Trim(),
                RazonSocial = txtRazonSocial.Text.Trim(),
                Ruc = txtRuc.Text.Trim(),
                Contacto = txtContacto.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Pais = txtPais.Text.Trim(),
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value,
                
            };

            if (logP.Modificar(p))
            {
                MessageBox.Show("Proveedor modificado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un proveedor primero."); return; }

            var res = MessageBox.Show("¿Deshabilitar este proveedor?", "Confirmar",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (logP.Deshabilitar(idSeleccionado))
                {
                    MessageBox.Show("Proveedor deshabilitado.");
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

        private void txtNombreProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void MantenedorProveedor_Load(object sender, EventArgs e)
        {

        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtRuc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRazonSocial_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
