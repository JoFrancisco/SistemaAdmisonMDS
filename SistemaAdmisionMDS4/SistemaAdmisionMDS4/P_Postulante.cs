using CapaNegocio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAdmisionMDS4
{
    public partial class P_Postulante : Form
    {
        N_Servicios notas = new N_Servicios();
        DataTable dataNotas;
        public P_Postulante()
        {
            dataNotas = notas.Notas();
            InitializeComponent();
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                picBoxCerrar.Location = new Point(531, 12);
                picBoxMinimizar.Location = new Point(469, 12);
                pictureBox1.Location = new Point(500, 12);
                pictureBox3.Location = new Point(500, 12);
                pictureBox1.Visible = true;
                pictureBox3.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBoxCerrar.Location = new Point(1280, 12);
                picBoxMinimizar.Location = new Point(1218, 12);
                pictureBox1.Location = new Point(1249, 12);
                pictureBox3.Location = new Point(1249, 12);
                pictureBox1.Visible = false;
                pictureBox3.Visible = true;
            }
        }

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void P_Postulante_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataNotas;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            dataNotas.DefaultView.RowFilter = $"dni LIKE '{textBox1.Text}%'";
            dataGridView1.DataSource= dataNotas;
        }
    }
}
