using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Factura
    {
        CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        DataTable tabla = new DataTable();
        

        public string MostrarNumeroFactura()
        {
            SqlDataReader leer2;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select top 1 IdFactu FROM Factura order by IdFactu desc";
            comando.CommandType = CommandType.Text;
            leer2 = comando.ExecuteReader();
            if (leer2.Read() == true)
            {
                string factura = leer2["IdFactu"].ToString();
                leer2.Close();
                return factura;
            }
            else
            {
                leer2.Close();
                return " ";
            }
            conexion.CerrarConexion();
        }

        public void InsertarFactura(CE_Factura factura)
        {
            comando.Parameters.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SP_AGGFACT";
            comando.CommandType= CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Fech", factura.Fecha_Factura);
            comando.Parameters.AddWithValue("@DoClient", factura.Documento_Cliente);
            comando.Parameters.AddWithValue("@CodVende", factura.Codigo_Vendedor);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void InsertarDetalleFactura(CE_Detalle_Factura factura)
        {
            comando.Parameters.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SP_AGGFACTDETA";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IDFac", factura.Numero_Factura);
            comando.Parameters.AddWithValue("@CodProd", factura.Cod_Productos);
            comando.Parameters.AddWithValue("@Cant", factura.Cantidad);
            comando.Parameters.AddWithValue("@ValUnidad", factura.Valor_Unidad);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void ActualizarCantidad(CE_Detalle_Factura factura)
        {
            comando.Parameters.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SP_DESCONTARCANT";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CodProd", factura.Cod_Productos);
            comando.Parameters.AddWithValue("@Cantidad", factura.Cantidad);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public int TraerCantidad(CE_Detalle_Factura cantproductos)
        {
            comando.Parameters.Clear();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "TraerCantidad";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Codigo", cantproductos.Cod_Productos);
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                int cantidad = Convert.ToInt32(leer["Cantidad"]);
                leer.Close();
                return cantidad;
            }
            else
            {
                leer.Close();
                return 0;
            }
            conexion.CerrarConexion();
        }
    }
}
