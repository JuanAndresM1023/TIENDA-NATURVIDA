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
    public class CN_Productos
    {
        //EN LA CAPA DE NEGOCIOS SE HARAN LOS PROCEDIMIENTOS Y CONVERSIONES DE LOS CAMPOS REQUERIDOS 

        private CD_Productos oCD_productos = new CD_Productos();

        public DataTable MostrarProd(string consulta) //USADO PARA MOSTRAR LOS PRODUCTOS EN UN DATA TABLE
        {
            DataTable dt = new DataTable();
            dt = oCD_productos.MostrarProductos(consulta);
            return dt;
        }

        //USADO PARA INSERTAR LOS PRODUCTOS
        public void InsertarProd(CE_Productos ingresar)
        {
            oCD_productos.InsertarProductos(ingresar);
        }

        //USADO PARA EDITAR LOS PRODUCTOS
        public void EditarProd(CE_Productos editar)
        {
            oCD_productos.EditarProductos(editar);
        }
     
        //USADO PARA ELIMINAR LOS PRODUCTOS
        public void EliminarProd(CE_Productos eliminar)
        {
            oCD_productos.EliminarProductos(eliminar);
        }

        public string MostrarValorProducto(CE_Productos producto)
        {
            string tabla = oCD_productos.MostrarValorProducto(producto);
            return tabla;
        }

        public DataTable MostrarInventarioConsultar(CE_Productos inventario)
        {
            DataTable tabla = new DataTable();
            tabla = oCD_productos.MostrarInventarioConsultar(inventario);
            return tabla;
        }

        public DataTable MostrarInventarioMostrarTodo()
        {
            DataTable tabla = new DataTable();
            tabla = oCD_productos.MostrarInventarioMostrarTodo();
            return tabla;
        }
    }
}
