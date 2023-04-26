using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Clientes
    {
        private CD_Clientes oCD_clientes = new CD_Clientes();

        public DataTable MostrarCli(string consultaclientes) //USADO PARA MOSTRAR LOS PRODUCTOS EN UN DATA TABLE
        {
            DataTable dt = new DataTable();
            dt = oCD_clientes.MostrarCliente(consultaclientes);
            return dt;
        }
        //USADO PARA INSERTAR CLIENTES
        public void InsertarCli(CE_Clientes insertarcli)
        {
            oCD_clientes.InsertarClientes(insertarcli);
        }

        //USADO PARA EDITAR CLIENTES
        public void EditarCli(CE_Clientes editarcli)
        {
            oCD_clientes.EditarClientes(editarcli);
        }

        //USADO PARA ELIMINAR CLIENTES
        public void EliminarCli(CE_Clientes eliminarcli)
        {
            oCD_clientes.EliminarClientes(eliminarcli);
        }
    }
}
