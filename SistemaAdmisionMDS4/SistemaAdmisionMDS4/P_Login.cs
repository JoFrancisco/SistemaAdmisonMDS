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
using System.Runtime.InteropServices;
namespace SistemaAdmisionMDS4
{
    public partial class P_Login : Form
    {
        N_Login acceso = new N_Login();
        public P_Login()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public static extern void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            acceso.CodUsuario = textCodigo.Text;
            acceso.Contrasenia = textContrasenia.Text;
            if (acceso.ConsultarUsuario())
            {
                MessageBox.Show("se ingreso correctamente");
            }
            else
            {
                MessageBox.Show("no se ingreso correctamente");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form formulario = new P_Registro();
            formulario.Show();
            this.Visible = false;
        }

        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void P_Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
