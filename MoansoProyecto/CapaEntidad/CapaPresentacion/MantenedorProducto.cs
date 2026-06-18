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
    public partial class MantenedorProducto : Form
    {
        logProducto logP = new logProducto();
        int idSeleccionado = 0;

        public MantenedorProducto()
        {
            InitializeComponent();
            CargarGrid();
            ModoVista();
        }

        void CargarGrid()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = logP.Listar();
        }

        void ModoVista()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtCategoria.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            chkEstado.Checked = false;
            dtpFecha.Value = DateTime.Now;

            txtCodigo.Enabled = false;       // ← siempre deshabilitado
            txtDescripcion.Enabled = false;
            txtCategoria.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
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
            
            txtDescripcion.Enabled = true;
            txtCategoria.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            dtpFecha.Enabled = true;

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnCancelar.Visible = true;
        }

        private void dgvProductos_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;
            var fila = dgvProductos.CurrentRow;

            idSeleccionado = Convert.ToInt32(fila.Cells["IdProducto"].Value);
            txtCodigo.Text = fila.Cells["Codigo"].Value.ToString();
            txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
            txtCategoria.Text = fila.Cells["Categoria"].Value.ToString();
            txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
            txtStock.Text = fila.Cells["Stock"].Value.ToString();
            chkEstado.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);
            dtpFecha.Value = Convert.ToDateTime(fila.Cells["FechaRegistro"].Value);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoEdicion();
            btnAgregar.Visible = true;
            btnModificar.Visible = false;
            dtpFecha.Value = DateTime.Now;
            txtCodigo.Text = logP.GenerarCodigo();   // ← código automático
            txtCodigo.Enabled = false;                  // ← no se puede editar
            txtDescripcion.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un producto primero."); return; }
            ModoEdicion();
            btnModificar.Visible = true;
            btnAgregar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            { MessageBox.Show("La descripción es obligatoria."); return; }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            { MessageBox.Show("El precio es obligatorio."); return; }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Trim(),
                System.Globalization.NumberStyles.Any,
                 System.Globalization.CultureInfo.InvariantCulture, out precio))
            { MessageBox.Show("El precio debe ser un número válido. Ejemplo: 25.50"); return; }

            int stock;
            if (!int.TryParse(txtStock.Text.Trim(), out stock))
            { MessageBox.Show("El stock debe ser un número entero. Ejemplo: 10"); return; }

            var p = new entProducto
            {
                Descripcion = txtDescripcion.Text.Trim(),
                Categoria = txtCategoria.Text.Trim(),
                Precio = precio,
                Stock = stock,
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logP.Registrar(p))  // ← aquí se genera el código dentro de datProducto
            {
                MessageBox.Show("Producto registrado correctamente con código: " + p.Codigo);
                CargarGrid();
                ModoVista();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            { MessageBox.Show("La descripción es obligatoria."); return; }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Trim(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out precio))
            { MessageBox.Show("El precio debe ser un número válido. Ejemplo: 25.50"); return; }

            int stock;
            if (!int.TryParse(txtStock.Text.Trim(), out stock))
            { MessageBox.Show("El stock debe ser un número entero. Ejemplo: 10"); return; }

            var p = new entProducto
            {
                IdProducto = idSeleccionado,
                Codigo = txtCodigo.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Categoria = txtCategoria.Text.Trim(),
                Precio = precio,
                Stock = stock,
                Activo = chkEstado.Checked,
                FechaRegistro = dtpFecha.Value
            };

            if (logP.Modificar(p))
            {
                MessageBox.Show("Producto modificado correctamente.");
                CargarGrid();
                ModoVista();
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            { MessageBox.Show("Selecciona un producto primero."); return; }

            var res = MessageBox.Show("¿Deshabilitar este producto?", "Confirmar",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (logP.Deshabilitar(idSeleccionado))
                {
                    MessageBox.Show("Producto deshabilitado.");
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