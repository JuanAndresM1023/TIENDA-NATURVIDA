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
    public class CD_Vendedores
    {
        CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand cmd = new SqlCommand();
        DataTable tabla = new DataTable();

        public DataTable MostrarVendedor(string consultavendedores)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = consultavendedores;
            cmd.CommandType = CommandType.Text;
            leer = cmd.ExecuteReader();
            tabla.Clear();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public DataTable BuscarVendedor (CE_Vendedores buscar)
        {
            cmd.Parameters.Clear();
            tabla.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "BuscarVendedores";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", buscar.Usuario);
            leer = cmd.ExecuteReader();
            tabla.Clear();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        //METODO INSERTAR
        public void InsertarVendedor(CE_Vendedores insertvendedor)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_INSERTARVENDEDOR";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", insertvendedor.Codigo);
            cmd.Parameters.AddWithValue("@Usuario", insertvendedor.Usuario);
            cmd.Parameters.AddWithValue("@Contraseña", insertvendedor.Contraseña);
            cmd.Parameters.AddWithValue("@Nombre", insertvendedor.Nombre);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO EDITAR
        public void EditarVendedor(CE_Vendedores editarvendedor)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ACTUALVENDEDOR";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", editarvendedor.Codigo);
            cmd.Parameters.AddWithValue("@Usuario", editarvendedor.Usuario);
            cmd.Parameters.AddWithValue("@Nombre", editarvendedor.Nombre);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO ELIMINAR
        public void EliminarVendedor(CE_Vendedores eliminarvendedor)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ELIMINARVENDEDOR";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", eliminarvendedor.Codigo);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
    }
}
