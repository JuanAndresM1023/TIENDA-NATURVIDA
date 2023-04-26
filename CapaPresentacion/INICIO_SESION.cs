using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;
using Microsoft.Win32;

namespace CapaPresentacion
{
    public partial class INICIO_SESION : Form
    {
        public INICIO_SESION()
        {
            InitializeComponent();
        }
        string usuario;
        //SqlConnection Conexion = new SqlConnection("Server=BUCDFPCSEFSD017;Database=NaturVida; Integrated Security=true");

        SqlConnection Conexion = new SqlConnection("Data Source=SQL8005.site4now.net;Initial Catalog=db_a9828f_naturvida;User Id=db_a9828f_naturvida_admin;Password=Juan1023T");

        public void ingresar()
        {
            Principal formulario = new Principal();

            Conexion.Open();
            SqlCommand comando = new SqlCommand("Select Usuario, Contraseña from Vendedores where Usuario=@Usuario and Contraseña=@Contraseña", Conexion);
            comando.Parameters.AddWithValue("@Usuario", txtboxusuario.Text);
            comando.Parameters.AddWithValue("@Contraseña", Encriptar.GetSHA256(txtboxcontraseña.Text));

            SqlDataReader lector = comando.ExecuteReader();


            if (lector.Read())
            {
                Conexion.Close();
                BuscarDatosVendedor();
                MessageBox.Show("Bienvenido " + CN_VarGloVendedor.nombre,"Ingreso Exitoso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                formulario.Show();
            }
            else
            {
                MessageBox.Show("Digite nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Conexion.Close();
            }
        }

        private void btniniciar_Click(object sender, EventArgs e)
        {
            usuario = txtboxusuario.Text;
            ingresar();
        }

        private void BuscarDatosVendedor()
        {
            SqlCommand cmd = new SqlCommand($"Select * from Vendedores where Usuario= '{usuario}'", Conexion);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            CN_VarGloVendedor.id = Convert.ToInt32(tabla.Rows[0][0]);
            CN_VarGloVendedor.nombre = tabla.Rows[0]["Nombre"].ToString();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
