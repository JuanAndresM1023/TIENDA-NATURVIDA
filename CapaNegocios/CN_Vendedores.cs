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
    public class CN_Vendedores
    {
        private CD_Vendedores oCD_Vendedores = new CD_Vendedores();

        public DataTable MostrarVendedor(string consulta) //USADO PARA MOSTRAR LOS PRODUCTOS EN UN DATA TABLE
        {
            DataTable dt = new DataTable();
            dt = oCD_Vendedores.MostrarVendedor(consulta);
            return dt;
        }

        public bool BuscarVend(CE_Vendedores vendedores)
        {
            DataTable encontrado = oCD_Vendedores.BuscarVendedor(vendedores);
            if (encontrado.Rows.Count > 0 )
            {
                return true;
            }
            else
                return false;
        }

        public void InsertarVendedor(CE_Vendedores insertar)
        {
            oCD_Vendedores.InsertarVendedor(insertar);
        }


        public void EditarVendedor(CE_Vendedores editar)
        {
            oCD_Vendedores.EditarVendedor(editar);
        }


        public void EliminarVendedor(CE_Vendedores eliminar)
        {
            oCD_Vendedores.EliminarVendedor(eliminar);
        }
    }
}
