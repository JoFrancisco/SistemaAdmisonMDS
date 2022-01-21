using CapaNegocio.ObjetosValores;
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
    public partial class P_Login : Form
    {
        N_Login acceso = new N_Login();
        public P_Login()
        {
            InitializeComponent();
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void button2_Click(object sender, EventArgs e)
        {
            Form formulario = new P_Registro();
            formulario.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (acceso.ConsultarUsuario(textCodigo.Text, textContrasenia.Text))
            {
                MessageBox.Show("se ingreso correctamente");
=======
=======
>>>>>>> Stashed changes
        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            if (acceso.ConsultarUsuario(textCodigo.ToString(), textContrasenia.ToString()))
            {
                MessageBox.Show("ingreso correctamente");
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            }
            else
            {
                MessageBox.Show("no se ingreso correctamente");
            }
        }
    }
}
