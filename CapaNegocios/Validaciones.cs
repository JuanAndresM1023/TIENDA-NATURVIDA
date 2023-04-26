using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CapaDatos;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaNegocios
{
    public class Validaciones
    {
        CD_Conexion oCD_Conexion = new CD_Conexion();
        public bool ValidarEmail(string comprobarEmai1)
        {
            string emailFormato;
            emailFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(comprobarEmai1, emailFormato))
            {
                if (Regex.Replace(comprobarEmai1, emailFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ValidarUsuarioVendedor(string user)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Vendedores WHERE Usuario='{user}'", oCD_Conexion.AbrirConexion());
            SqlDataReader leer;
            leer = cmd.ExecuteReader();

            if (leer.Read())
            {
                oCD_Conexion.CerrarConexion();
                return true;

            }
            else
            {
                oCD_Conexion.CerrarConexion();
                return false;
            }
        }

        public bool ValidarIdVendedor(int Codigo)
        {

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Vendedores WHERE Codigo={Codigo}", oCD_Conexion.AbrirConexion());
            SqlDataReader leer;
            leer = cmd.ExecuteReader();

            if (leer.Read())
            {
                oCD_Conexion.CerrarConexion();
                return true;

            }
            else
            {
                oCD_Conexion.CerrarConexion();
                return false;
            }
        }

        public bool ValidarDescripcionProducto(string desc)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Productos WHERE Descripción='{desc}'", oCD_Conexion.AbrirConexion());
            SqlDataReader leer;
            leer = cmd.ExecuteReader();

            if (leer.Read())
            {
                oCD_Conexion.CerrarConexion();
                return true;

            }
            else
            {
                oCD_Conexion.CerrarConexion();
                return false;
            }
        }

    }
}   
