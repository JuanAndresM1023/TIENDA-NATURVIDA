using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaNegocios
{
    public class CN_Factura
    {
        CD_Factura oCD_Factura = new CD_Factura();

        public string MostrarNFactura()
        {
            string factura = oCD_Factura.MostrarNumeroFactura();
            return factura;
        }

        public void InsertarFactura(CE_Factura factura)
        {
            oCD_Factura.InsertarFactura(factura);
        }

        public void InsertarDetalleFactura(CE_Detalle_Factura factura)
        {
            oCD_Factura.InsertarDetalleFactura(factura);
        }

        public void ActualizarCantidad (CE_Detalle_Factura factura)
        {
            oCD_Factura.ActualizarCantidad(factura);
        }

        public int TraerCantidad(CE_Detalle_Factura factura)
        {
            int tabla = oCD_Factura.TraerCantidad(factura);
            return tabla;            
        }
        
    }
}
