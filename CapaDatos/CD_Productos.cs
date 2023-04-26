using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_Productos
    {
        //SE INSTANCIA LA CONEXION Y SE CREAN LOS COMANDOS Y LAS VARIABLES PARA REALIZAR LOS METODOS CRUD
        CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand cmd = new SqlCommand();
        DataTable tabla = new DataTable();

        //METODO MOSTRAR
        public DataTable MostrarProductos(string consulta)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = consulta;
            cmd.CommandType = CommandType.Text;
            leer = cmd.ExecuteReader();
            tabla.Clear();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }
        //METODO INSERTAR
        public void InsertarProductos(CE_Productos insertar)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_INSERTARPRODUCT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", insertar.cod);
            cmd.Parameters.AddWithValue("@Descri", insertar.descripcion);
            cmd.Parameters.AddWithValue("@ValUnd", insertar.valorU);
            cmd.Parameters.AddWithValue("@Cantida", insertar.cantidad);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO EDITAR
        public void EditarProductos(CE_Productos editar)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ACTUALIZARPROD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cod", editar.cod);
            cmd.Parameters.AddWithValue("@Descri", editar.descripcion);
            cmd.Parameters.AddWithValue("@ValUnd", editar.valorU);
            cmd.Parameters.AddWithValue("@Cant", editar.cantidad);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO ELIMINAR
        public void EliminarProductos(CE_Productos eliminar)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ELIMINARPROD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cod", eliminar.cod);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public string MostrarValorProducto(CE_Productos producto)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "MostrarValorProducto";
            cmd.Parameters.AddWithValue("@Codigo", producto.cod);
            cmd.CommandType= CommandType.StoredProcedure;
            leer = cmd.ExecuteReader();
            if (leer.Read())
            {
                string factura = leer["Valor_Unidad"].ToString();
                leer.Close();
                return factura;
            }
            else
            {
                leer.Close ();
                conexion.CerrarConexion();
            }
            return " ";
            }
            

        public DataTable MostrarInventarioConsultar(CE_Productos inventario)
        {
            DataTable tabla4 = new DataTable();
            cmd.Parameters.Clear();
            tabla4.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_BUSCARINVENTARIO";
            cmd.Parameters.AddWithValue("@Prod", inventario.cod);
            cmd.CommandType = CommandType.StoredProcedure;
            leer = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            tabla4.Load(leer);
            conexion.CerrarConexion();
            return tabla4;
        }



        public DataTable MostrarInventarioMostrarTodo()
        {
            DataTable tabla5 = new DataTable();
            cmd.Parameters.Clear();
            tabla5.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_BUSCARINVENTARIOS";
            cmd.CommandType = CommandType.StoredProcedure;
            leer = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            tabla5.Load(leer);
            conexion.CerrarConexion();
            return tabla5;
        }



    }
}
