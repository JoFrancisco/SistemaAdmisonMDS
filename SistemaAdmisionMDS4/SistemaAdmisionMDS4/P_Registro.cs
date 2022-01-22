using CapaNegocio.Modelos;
using CapaNegocio.ObjetosValores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAdmisionMDS4
{
    public partial class P_Registro : Form
    {
        N_Postulante n_Postulante = new N_Postulante();
        public P_Registro()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public static extern void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void picBoxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picBoxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void P_Registro_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            n_Postulante.Dni = textDni.Text;
            n_Postulante.Nombres = textNombres.Text;
            n_Postulante.Fecha = datePicker.Value;
            n_Postulante.Estado = EstadoEntidad.Agregad;
            n_Postulante.GuardarCambios();
        }

        private void textDni_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
