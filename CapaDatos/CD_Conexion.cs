using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Conexion
    {

        //SE REALIZA LA CONEXION A LA BASE DE DATOS

        //private SqlConnection Conexion = new SqlConnection("Server=BUCDFPCSEFSD017;Database=NaturVida; Integrated Security=true");

        private SqlConnection Conexion = new SqlConnection("Data Source=SQL8005.site4now.net;Initial Catalog=db_a9828f_naturvida;User Id=db_a9828f_naturvida_admin;Password=Juan1023T");

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;

        }

        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;

        }
    }
}
