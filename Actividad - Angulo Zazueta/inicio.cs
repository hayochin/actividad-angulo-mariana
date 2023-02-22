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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string action = "Agregar";
            form fr = new form(action);
            fr.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string action = "Modificar";
            form fr = new form(action);
            fr.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string action = "Eliminar";
            form fr = new form(action);
            fr.ShowDialog();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                dsCedisTableAdapters.articulosTableAdapter ta = new dsCedisTableAdapters.articulosTableAdapter();
                ta.GetData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                this.Close();
            }
        }
    }
}
