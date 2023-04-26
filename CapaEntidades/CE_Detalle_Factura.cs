using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CE_Detalle_Factura
    {
        public int Numero_Factura { get; set; }

        public int Cod_Productos { get; set; }

        public int Cantidad { get; set; }

        public int Valor_Unidad { get; set; }
    }
}
