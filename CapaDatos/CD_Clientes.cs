using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_Clientes
    {
        CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand cmd = new SqlCommand();
        DataTable tabla = new DataTable();


        public DataTable MostrarCliente(string consultaclientes)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = consultaclientes;
            cmd.CommandType = CommandType.Text;
            leer = cmd.ExecuteReader();
            tabla.Clear();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }
        //METODO INSERTAR
        public void InsertarClientes(CE_Clientes insertarcli)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_INSERTARCLIENT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Docu", insertarcli.Documento);
            cmd.Parameters.AddWithValue("@Nombre", insertarcli.Nombre);
            cmd.Parameters.AddWithValue("@Direccion", insertarcli.Direccion);
            cmd.Parameters.AddWithValue("@Tel", insertarcli.Telefono);
            cmd.Parameters.AddWithValue("@Corr", insertarcli.Correo);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO EDITAR
        public void EditarClientes(CE_Clientes editarcli)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ACTUALCLIENT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Docu", editarcli.Documento);
            cmd.Parameters.AddWithValue("@Nombre", editarcli.Nombre);
            cmd.Parameters.AddWithValue("@Direccion",editarcli.Direccion);
            cmd.Parameters.AddWithValue("@Tel",editarcli.Telefono);
            cmd.Parameters.AddWithValue("@Corr", editarcli.Correo);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        //METODO ELIMINAR
        public void EliminarClientes(CE_Clientes eliminarcli)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "SP_ELIMINARCLIENT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Docu", eliminarcli.Documento);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
    }
}
