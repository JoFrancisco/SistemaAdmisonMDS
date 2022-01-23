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
        N_Recibo n_Recibo = new N_Recibo();
        N_Login n_Login = new N_Login();
        private bool verificaContrasenia;
        private bool verificaverContrasenia;
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
            if(verificaContrasenia == verificaverContrasenia && verificaContrasenia == true)
            {
                n_Postulante.Dni = textDni.Text;
                n_Postulante.Nombres = textNombres.Text;
                n_Postulante.Fecha = (DateTime)datePicker.Value;
                if (n_Postulante.Validacion())
                {
                    n_Recibo.Dni = textDni.Text;
                    n_Recibo.NroRecibo = textRecibo.Text;
                    if (n_Recibo.Validacion())
                    {
                        if (n_Recibo.buscarRecibo(n_Recibo.Dni, n_Recibo.NroRecibo))
                        {
                            if (!n_Postulante.BuscarPostulante(n_Postulante.Dni))
                            {
                                if (!n_Login.BuscarCuenta(n_Postulante.Dni))
                                {
                                    n_Postulante.Estado = EstadoEntidad.Agregad;
                                    string result = n_Postulante.GuardarCambios();
                                    MessageBox.Show(result);
                                    n_Login.AgregarCuenta(n_Postulante.Dni, n_Postulante.Nombres, textContrasenia.Text, "Postulante");
                                    MessageBox.Show("cuenta registrada !");
                                }
                                else
                                {
                                    MessageBox.Show("Ya existe una cuenta del Postulante");
                                }
                            }
                            else
                            {
                                MessageBox.Show("ya existe un postulante registrado con ese Recibo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No exite un numero de Recibo correspondiente al Dni de Usted\n " +
                                "o El numero de recibo es incorrecto");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El numero de Recibo es Invalido");
                    }
                }
                else
                {
                    MessageBox.Show("Los datos del postulante son Invalidos");
                }
            }
            else
            {
                MessageBox.Show("Error al ingresar contraseñas");
            }
        }

        private void textDni_TextChanged(object sender, EventArgs e)
        {
            var postulante = new N_Postulante();
            postulante.Dni = textDni.Text;
            bool error = postulante.Validacion();
            List<string> list = postulante.mensajesDni();
            if (error != true)
            {
                if(list.Count != 0)
                {
                    
                    linkDni.Text = list[0];
                    linkDni.Location = new Point(430 + (60 - list[0].Length), 130);
                    linkDni.LinkColor = Color.Red;
                    
                }
                else
                {
                    
                    linkDni.Text = "correcto";
                    linkDni.Location = new Point(615, 130);
                    linkDni.LinkColor = Color.Green;
                }
            }
        }

        private void textNombres_TextChanged(object sender, EventArgs e)
        {
            var postulante = new N_Postulante();
            postulante.Nombres = textNombres.Text;
            bool error = postulante.Validacion();
            List<string> list = postulante.mensajesNombres();
            if (error != true)
            {
                if (list.Count != 0)
                {

                    linkNombres.Text = list[0];
                    linkNombres.Location = new Point(400 + (60 - list[0].Length), 175);
                    linkNombres.LinkColor = Color.Red;

                }
                else
                {

                    linkNombres.Text = "correcto";
                    linkNombres.Location = new Point(615, 175);
                    linkNombres.LinkColor = Color.Green;
                }
            }
        }

        private void textContrasenia_TextChanged(object sender, EventArgs e)
        {
            if(textContrasenia.Text.Length < 6 || textContrasenia.Text.Length > 40)
            {
                linkContrasenia.Text = "La contraseña debe contener entre 6 y 40 caracteres";
                linkContrasenia.Location = new Point(405, 270);
                linkContrasenia.LinkColor = Color.Red;
                verificaContrasenia = false;
            }
            else
            {
                linkContrasenia.Text = "correcto";
                linkContrasenia.Location = new Point(615, 270);
                linkContrasenia.LinkColor = Color.Green;
                verificaContrasenia = true;
            }
        }

        private void textVerContrasenia_TextChanged(object sender, EventArgs e)
        {
            if (textVerContrasenia.Text.Length >= 6 && textVerContrasenia.Text.Length <= 40)
            {
                if (textVerContrasenia.Text != textContrasenia.Text)
                {
                    linkVerContrasenia.Text = "Las Contraseñas no son iguales";
                    linkVerContrasenia.Location = new Point(505, 320);
                    linkVerContrasenia.LinkColor = Color.Red;
                    verificaverContrasenia = false;
                }
                else
                {
                    linkVerContrasenia.Text = "correcto";
                    linkVerContrasenia.Location = new Point(615, 320);
                    linkVerContrasenia.LinkColor = Color.Green;
                    verificaverContrasenia = true;
                }
                
            }
            else
            {
                if(textVerContrasenia.Text.Length == 0)
                {
                    linkVerContrasenia.Text = "obligatorio";
                    linkVerContrasenia.Location = new Point(605, 320);
                    linkVerContrasenia.LinkColor = Color.Red;
                    verificaverContrasenia = false;
                }
                else
                {
                    linkVerContrasenia.Text = "La contraseña debe contener entre 6 y 40 caracteres";
                    linkVerContrasenia.Location = new Point(405, 320);
                    linkVerContrasenia.LinkColor = Color.Red;
                    verificaverContrasenia = false;
                }
            }
        }

        private void textRecibo_TextChanged(object sender, EventArgs e)
        {
            var recibo = new N_Recibo();
            recibo.NroRecibo = textRecibo.Text;
            bool error = recibo.Validacion();
            List<string> list = recibo.mensajesRecibo();
            if (error != true)
            {
                if (list.Count != 0)
                {

                    linkRecibo.Text = list[0];
                    linkRecibo.Location = new Point(395 + (60 - list[0].Length), 370);
                    linkRecibo.LinkColor = Color.Red;

                }
                else
                {

                    linkRecibo.Text = "correcto";
                    linkRecibo.Location = new Point(615, 370);
                    linkRecibo.LinkColor = Color.Green;
                }
            }
        }
    }
}
