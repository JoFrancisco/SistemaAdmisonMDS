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
    public partial class P_Digitador : Form
    {
        public P_Digitador()
        {
            InitializeComponent();
        }

        private void P_Digitador_Load(object sender, EventArgs e)
        {
            this.errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ImageList list = new ImageList();
            list.ImageSize = new Size(16, 16);
            list.Images.Add(Image.FromFile(@"C:\Users\USUARIO\Documents\GitHub\SistemaAdmisonMDS\SistemaAdmisionMDS4\Imagenes\IcoError.ico"));
            this.errorProvider1.Icon = Icon.FromHandle(((Bitmap)list.Images[0]).GetHicon());
            errorProvider1.SetError(textBox1, "campo obligatorio");
            Refresh1();
        }
        private void Refresh1()
        {
            dataGridView1.Rows.Add(20);
            int num = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Value = num;
                num++;
            }
        }
        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picBoxCerrar.Location = new Point(1280, 12);
                picBoxMinimizar.Location = new Point(1218, 12);
                pictureBox2.Location = new Point(1249, 12);
                pictureBox3.Location = new Point(1249, 12);
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                picBoxCerrar.Location = new Point(531, 12);
                picBoxMinimizar.Location = new Point(469, 12);
                pictureBox2.Location = new Point(500, 12);
                pictureBox3.Location = new Point(500, 12);
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) == "")
            {
                var n_servicio = new N_Servicios();
                DataTable dt = n_servicio.Solucionario();
                int nota = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    var res = ((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[1]).Value;
                    if (res != null)
                    {
                        if (dt.Rows[i]["solucion"].ToString().Equals(res.ToString()))
                        {
                            nota++;
                        }
                    }
                }
                n_servicio.InsertarNota(textBox1.Text, nota);

                MessageBox.Show("Guardado Correctamente");
                dataGridView1.Rows.Clear();
                Refresh1();
            }
            else
            {
                MessageBox.Show("Codigo de Postulante invalido");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "campo obligatorio");
                pbCorrecto.Visible = false;
            }
            else
            {
                var n_servicio = new N_Servicios();
                if (!n_servicio.ValidarPostulante(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "error en al ingresar dni");
                    pbCorrecto.Visible = false;
                }
                else
                {
                    pbCorrecto.Visible = true;
                    errorProvider1.SetError(textBox1, "");
                }
            }
        }
    }
}
