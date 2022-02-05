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
        N_Login n_Login = new N_Login();
        public P_Registro()
        {
            InitializeComponent();
        }
        private void P_Registro_Load(object sender, EventArgs e)
        {
            this.errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            ImageList list = new ImageList();
            list.ImageSize = new Size(16, 16);
            list.Images.Add(Image.FromFile(@"C:\Users\USUARIO\Documents\GitHub\SistemaAdmisonMDS\SistemaAdmisionMDS4\Imagenes\IcoError.ico"));
            this.errorProvider1.Icon = Icon.FromHandle(((Bitmap)list.Images[0]).GetHicon());
            errorProvider1.SetError(textDni, "campo obligatorio");
            errorProvider1.SetError(textNombres, "campo obligatorio");
            errorProvider1.SetError(textContrasenia, "campo obligatorio");
            errorProvider1.SetError(textVerContrasenia, "campo obligatorio");
            errorProvider1.SetError(textRecibo, "campo obligatorio");
            errorProvider1.SetError(textPaterno, "campo obligatorio");
            errorProvider1.SetError(textMaterno, "campo obligatorio");
        }

        #region Mover form , Minimizar, Cerrar
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
        #endregion
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(errorProvider1.GetError(textDni) == "" && errorProvider1.GetError(textNombres) == "" &&
               errorProvider1.GetError(textContrasenia) == "" && errorProvider1.GetError(textVerContrasenia) == "" &&
               errorProvider1.GetError(textRecibo) == "")
            {
                n_Postulante.Dni = textDni.Text;
                n_Postulante.Nombres = textNombres.Text;
                n_Postulante.ApPaterno = textPaterno.Text;
                n_Postulante.ApMaterno = textMaterno.Text;
                n_Postulante.Fecha = (DateTime)datePicker.Value;
                n_Postulante.Recibo = textRecibo.Text;
                if (n_Postulante.VerificarRecibo())
                {
                    if (!n_Postulante.BuscarPostulante(n_Postulante.Dni))
                    {
                        if (!n_Login.BuscarCuenta(n_Postulante.Dni))
                        {
                            n_Postulante.Estado = EstadoEntidad.Agregad;
                            string result = n_Postulante.GuardarCambios(textContrasenia.Text);
                            MessageBox.Show(result);
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
                    MessageBox.Show("No exite un numero de recibo correspondiente al Dni de Usted\n " +
                        "o el numero de recibo es incorrecto");
                }
                
            }
            else
            {
                MessageBox.Show("Error al ingresar campos");
            }
        }

        #region text Validacion changed
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
                    pbCorrectoDni.Visible = false;
                    errorProvider1.SetError(textDni, list[0]);  
                }
                else
                {
                    pbCorrectoDni.Visible = true;
                    errorProvider1.SetError(textDni, "");
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
                    pbCorrectoNombres.Visible = false;
                    errorProvider1.SetError(textNombres, list[0]);
                }
                else
                {
                    pbCorrectoNombres.Visible = true;
                    errorProvider1.SetError(textNombres, "");
                }
            }
        }

        private void textContrasenia_TextChanged(object sender, EventArgs e)
        {
            if(textContrasenia.Text.Length < 6 || textContrasenia.Text.Length > 40)
            {
                pbCorrrectoContrasenia.Visible = false;
                errorProvider1.SetError(textContrasenia, "La contraseña debe contener entre 6 y 40 caracteres");
            }
            else
            {
                pbCorrrectoContrasenia.Visible = true;
                errorProvider1.SetError(textContrasenia, "");
            }
        }

        private void textVerContrasenia_TextChanged(object sender, EventArgs e)
        {
            if (textVerContrasenia.Text.Length >= 6 && textVerContrasenia.Text.Length <= 40)
            {
                if (textVerContrasenia.Text != textContrasenia.Text)
                {
                    pbCorrectoVerContrasenia.Visible = false;
                    errorProvider1.SetError(textVerContrasenia, "Las Contraseñas no son iguales");
                }
                else
                {
                    pbCorrectoVerContrasenia.Visible = true;
                    errorProvider1.SetError(textVerContrasenia, "");
                }
                
            }
            else
            {
                pbCorrectoVerContrasenia.Visible = false;
                if (textVerContrasenia.Text.Length == 0)
                {
                    errorProvider1.SetError(textVerContrasenia, "campo obligatorio");
                }
                else
                {
                    errorProvider1.SetError(textVerContrasenia, "La contraseña debe contener entre 6 y 40 caracteres");
                    
                }
            }
        }

        private void textRecibo_TextChanged(object sender, EventArgs e)
        {

            var postulante = new N_Postulante();
            postulante.Recibo = textRecibo.Text;
            bool error = postulante.Validacion();
            List<string> list = postulante.mensajesRecibo();
            if (error != true)
            {
                if (list.Count != 0)
                {
                    pbCorrectoRecibo.Visible = false;
                    errorProvider1.SetError(textRecibo, list[0]);
                }
                else
                {
                    pbCorrectoRecibo.Visible = true;
                    errorProvider1.SetError(textRecibo, "");
                }
            }
        }
        private void textPaterno_TextChanged(object sender, EventArgs e)
        {
            var postulante = new N_Postulante();
            postulante.ApPaterno = textPaterno.Text;
            bool error = postulante.Validacion();
            List<string> list = postulante.mensajesApPaterno();
            if (error != true)
            {
                if (list.Count != 0)
                {
                    pbPaterno.Visible = false;
                    errorProvider1.SetError(textPaterno, list[0]);
                }
                else
                {
                    pbPaterno.Visible = true;
                    errorProvider1.SetError(textPaterno, "");
                }
            }
        }
        private void textMaterno_TextChanged(object sender, EventArgs e)
        {
            var postulante = new N_Postulante();
            postulante.ApMaterno = textMaterno.Text;
            bool error = postulante.Validacion();
            List<string> list = postulante.mensajesApMaterno();
            if (error != true)
            {
                if (list.Count != 0)
                {
                    pbMaterno.Visible = false;
                    errorProvider1.SetError(textMaterno, list[0]);
                }
                else
                {
                    pbMaterno.Visible = true;
                    errorProvider1.SetError(textMaterno, "");
                }
            }
        }
        #endregion
    }
}
