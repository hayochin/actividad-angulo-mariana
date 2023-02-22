using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad___Angulo_Zazueta
{
    public partial class form : Form
    {
        private string action;
        public form(string action)
        {
            this.action = action;
            InitializeComponent();
        }

        private void form_Load(object sender, EventArgs e)
        {
            btnGuardar.Text = "Guardar";
            if (this.action == "Eliminar")
            {
                btnGuardar.Text = "Eliminar";
                txtDescripcion.Enabled = false;
                txtNumProveedor.Enabled = false;
                cbActivo.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else if (this.action == "Modificar")
            {
                btnGuardar.Enabled = false;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Trim().Length == 0 || cbActivo.Text == "" || 
                txtCodigo.Text.Trim().Length == 0 || txtNumProveedor.Text.Trim().Length == 0)
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBox.Show("Error: Favor de no dejar campos vacios", "Error", button, MessageBoxIcon.Warning);
            }
            else
            {
                switch (this.action)
                {
                    case "Agregar":
                        agregar();
                        break;

                    case "Modificar":
                        modificar();
                        break;

                    case "Eliminar":
                        eliminar();
                        break;

                    default:
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        MessageBox.Show("Error: No se ha seleccionado ninguna acción", "Error", button, MessageBoxIcon.Warning);
                        break;
                }
            }
        }

        private void agregar()
        {
            dsCedisTableAdapters.articulosTableAdapter ta = new dsCedisTableAdapters.articulosTableAdapter();
            try
            {
                ta.InsertQuery(
                int.Parse(txtCodigo.Text.Trim()),
                txtDescripcion.Text,
                int.Parse(txtNumProveedor.Text.Trim()),
                cbActivo.Text
            );
            MessageBox.Show("Guardado de manera exitsosa", "Exito");
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void modificar()
        {
            dsCedisTableAdapters.articulosTableAdapter ta = new dsCedisTableAdapters.articulosTableAdapter();
            try
            {
                ta.UpdateQuery(
                    txtDescripcion.Text,
                    int.Parse(txtNumProveedor.Text.Trim()),
                    cbActivo.Text,
                    int.Parse(txtCodigo.Text.Trim())
                );
                MessageBox.Show("Guardado de manera exitsosa", "Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cargarRegistro()
        {
            dsCedisTableAdapters.articulosTableAdapter ta = new dsCedisTableAdapters.articulosTableAdapter();
            try
            {
                dsCedis.articulosDataTable dt = ta.GetDataByID(int.Parse(txtCodigo.Text));
                if (dt == null)
                {
                    MessageBox.Show("Registro no encontrado", "Error");
                }
                else
                {
                    btnGuardar.Enabled = true;
                    labelError.Visible = false;
                    dsCedis.articulosRow row = (dsCedis.articulosRow)dt.Rows[0];
                    txtDescripcion.Text = row.descripcion;
                    txtNumProveedor.Text = row.id_proveedor.ToString();
                    cbActivo.Text = row.activo;
                }
            }
            catch(Exception ex)
            {
                labelError.Visible = true;
                txtDescripcion.Text = "";
                txtNumProveedor.Text = "";
                cbActivo.Text = default;
                btnGuardar.Enabled = false;
            }
            
        }

        private void eliminar()
        {
            dsCedisTableAdapters.articulosTableAdapter ta = new dsCedisTableAdapters.articulosTableAdapter();
            DialogResult dialogResult = MessageBox.Show(
                "¿Estás seguro que deseas eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    ta.DeleteQuery(int.Parse(txtCodigo.Text));
                    MessageBox.Show("Eliminado con éxito", "Éxito");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }     
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if(this.action != "Agregar")
            {
                cargarRegistro();
            }
        }
    }
}
