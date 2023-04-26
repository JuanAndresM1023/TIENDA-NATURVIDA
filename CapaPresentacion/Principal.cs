using CapaEntidades;
using CapaNegocios;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace CapaPresentacion
{
    public partial class Principal : Form
    {  
        //REGIONES PARA ORGANIZAR PROCESOS
        #region INICIO

        //INSTANCIAMOS LA CAPA DE NEGOCIOS 

        CN_Productos oCN_Productos = new CN_Productos();
		CE_Productos oCE_Productos = new CE_Productos();
        CN_Clientes oCN_Clientes = new CN_Clientes();
        CE_Clientes oCE_Clientes = new CE_Clientes();
        Validaciones oCN_Validaciones = new Validaciones();

        System.Data.DataTable tabla = new System.Data.DataTable();

		string dato;
        string datocli;
        string consulta = "Select * from productos";
        string consultaclientes = "select * from cliente";
        string consultavendedor = "select * from vendedores";
        
        public Principal()
        {
            InitializeComponent();
            mostrarcombo(cmbProductos);
            MostrarComboCliente(cmbClientes);
        }

#endregion

        #region PRODUCTOS

        //BOTON PARA QUE CARGUE EL DATAGRIDVIEW CON LOS REGISTROS

        private void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            dgvProductos.DataSource = oCN_Productos.MostrarProd(consulta);
            mostrarcombo(cmbProductos);
        }

        //METODO PARA MOSTRAR LOS NOMBRES DE LOS PRODUCTOS EN EL COMBOBOX
        public void mostrarcombo(ComboBox combo)
        {
            combo.DataSource = oCN_Productos.MostrarProd(consulta);
            combo.ValueMember = "Codigo";
            combo.DisplayMember = "Descripción";
        }

        //BOTON CONSULTAR PROGRAMADO CON EL METODO MOSTRAR NOMBRES  

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MostrarNombresProductos();
        }

        private void Consultar_prod_Enter(object sender, EventArgs e)
        {
            dgvProductos.DataSource = oCN_Productos.MostrarProd(consulta);
            mostrarcombo(cmbProductos);
        }

        //PARA INGRESAR PRODUCTOS   
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (oCN_Validaciones.ValidarDescripcionProducto(txtboxdescripcion.Text))
            {
                MessageBox.Show("Lo sentimos, esta descripción ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if 
                (string.IsNullOrEmpty(txtboxcodigo.Text) || 
                string.IsNullOrEmpty(txtboxdescripcion.Text) || 
                string.IsNullOrEmpty(txtboxvalorU.Text) || 
                string.IsNullOrEmpty(txtboxcantidad.Text))
                {
                    MessageBox.Show("Debe ingresar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            else
             {
               try
                {
                  lblCodigoExist.Text = " ";
                  ConseguirDatos(txtboxcodigo, txtboxdescripcion, txtboxvalorU, txtboxcantidad);
                  oCN_Productos.InsertarProd(oCE_Productos);
                  MessageBox.Show("Ingreso Correctamente");
                  mostrarcombo(cmbProductos);
                }
                catch
                  {  
                     lblCodigoExist.Text = "El código ya existe";
                  }
             }
            
        }


        //LIMPIAR LOS TEXTBOX   
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtboxcodigo.Clear();
            txtboxdescripcion.Clear();
            txtboxvalorU.Clear();
            txtboxcantidad.Clear();
        }

        private void MostrarNombresProductos()
        {
            dato = cmbProductos.SelectedValue.ToString();
            consulta = "Select * from productos where Codigo=" + dato;
            dgvProductos.DataSource = oCN_Productos.MostrarProd(consulta);
            consulta = "Select * from productos";
        }

		private void buttonconsultar_Click(object sender, EventArgs e)
        {

            MostrarNombresProductos();

            if (cmbProductosedi.SelectedIndex>=0)
            {
                txtcodigo2.Text = dgvProductos.CurrentRow.Cells["Codigo"].Value.ToString();
                txtboxdescri2.Text=dgvProductos.CurrentRow.Cells["Descripción"].Value.ToString();
                txtboxvalor.Text = dgvProductos.CurrentRow.Cells["Valor_Unidad"].Value.ToString();
                txtboxcant.Text = dgvProductos.CurrentRow.Cells["Cantidad"].Value.ToString();
            }
        }

        private void editar_Enter(object sender, EventArgs e)
        {
            mostrarcombo(cmbProductosedi);
		}

        private void Eliminar_Enter(object sender, EventArgs e)
        {
            mostrarcombo(comboBoxProductoEli);
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¡Esta seguro que desea Eliminar?", "Advertencia", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
				oCE_Productos.cod = Convert.ToInt32(cmbProductos.SelectedValue);
                oCN_Productos.EliminarProd(oCE_Productos);
                MessageBox.Show("Se ha eliminado");
                mostrarcombo(comboBoxProductoEli);
            }
        }

        private void Consultar_Enter(object sender, EventArgs e)
        {
            dgvProductos.DataSource = oCN_Productos.MostrarProd(consulta);
        }

        

		private void btnGuardarCam_Click(object sender, EventArgs e)
        {
            
			try 
            {
				ConseguirDatos(txtcodigo2, txtboxdescri2, txtboxvalor, txtboxcant);
				oCN_Productos.EditarProd(oCE_Productos);
				MessageBox.Show("Editado Correctamente");
				Limpiar();
			}
            catch
            {
                MessageBox.Show("ERROR");
            }
			
		}


        // Mis métodos

        private void Limpiar()
        {
            txtcodigo2.Clear();
            txtboxdescri2.Clear();
            txtboxvalor.Clear();
            txtboxcant.Clear();
        }

        private void ConseguirDatos(TextBox text, TextBox text2, TextBox text3, TextBox text4)
        {
			oCE_Productos.cod = Convert.ToInt32(text.Text);
			oCE_Productos.descripcion = text2.Text;
			oCE_Productos.valorU = Convert.ToInt32(text3.Text);
			oCE_Productos.cantidad = Convert.ToInt32(text4.Text);
		}

        #endregion

        #region CLIENTES

        //CLIENTES------------------------------------------------------------------------------------------------------------------------------
        private void MostrarComboCliente(ComboBox comboCli)
        {
            comboCli.DataSource = oCN_Clientes.MostrarCli(consultaclientes);
            comboCli.ValueMember = "Documento";
            comboCli.DisplayMember = "Nombre";
        }

        private void btnMostrarCli_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = oCN_Clientes.MostrarCli(consultaclientes);
            MostrarComboCliente(cmbClientes);
        }

        private void MostrarNombresClientes()
        {
            datocli= cmbClientes.SelectedValue.ToString();
            consultaclientes = "Select * from cliente where Documento=" + datocli;
            dgvClientes.DataSource = oCN_Clientes.MostrarCli(consultaclientes);
            consultaclientes = "Select * from cliente";
        }

        private void btnconsultarCli_Click(object sender, EventArgs e)
        {
            MostrarNombresClientes();
        }
        private void consultar_cli_Enter(object sender, EventArgs e)
        {
            dgvClientes.DataSource = oCN_Clientes.MostrarCli(consultaclientes);
            MostrarComboCliente(cmbClientes);
        }

        private void btnlimpiardatoscliente_Click(object sender, EventArgs e)
        {
            txtboxdocumento.Clear();
            txtboxnombre.Clear();
            txtboxdireccion.Clear();
            txtboxtelefono.Clear();
            txtboxcorreo.Clear();
        }

        private void ConseguirDatosCliente(TextBox text, TextBox text2, TextBox text3, TextBox text4, TextBox text5)
        {
            oCE_Clientes.Documento = Convert.ToInt32(text.Text);
            oCE_Clientes.Nombre = text2.Text;
            oCE_Clientes.Direccion = text3.Text;
            oCE_Clientes.Telefono = text4.Text;
            oCE_Clientes.Correo = text5.Text;
        }

        private void btnguardarcliente_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrEmpty(txtboxdocumento.Text) ||
                string.IsNullOrEmpty(txtboxnombre.Text) ||
                string.IsNullOrEmpty(txtboxdireccion.Text) ||
                string.IsNullOrEmpty(txtboxtelefono.Text) ||
                string.IsNullOrEmpty(txtboxcorreo.Text))
                {
                    MessageBox.Show("Debe ingresar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            else if (oCN_Validaciones.ValidarEmail(txtboxcorreo.Text) == true)
            {
                ConseguirDatosCliente(txtboxdocumento, txtboxnombre, txtboxdireccion, txtboxtelefono, txtboxcorreo);
                oCN_Clientes.InsertarCli(oCE_Clientes);
                lblformatocorreo.Text = "Dirección Valida";
                lblformatocorreo.ForeColor = Color.Green;
                MessageBox.Show("Ingreso Correctamente");
                MostrarComboCliente(cmbClientes);
                lblformatocorreo.Text = "-";
            }
               
            else if (oCN_Validaciones.ValidarEmail(txtboxcorreo.Text) == false)
             {
                lblformatocorreo.Text = "Dirección Invalida";
                lblformatocorreo.ForeColor = Color.Red;
             }
        }

        private void btnconsulaclienteedi_Click(object sender, EventArgs e)
        {
            MostrarNombresClientes();

            if (cmbclienteedi.SelectedIndex >= 0)
            {
                txtboxdocumentocli.Text = dgvClientes.CurrentRow.Cells["Documento"].Value.ToString();
                txtnombrecliente.Text = dgvClientes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtdireccioncliente.Text = dgvClientes.CurrentRow.Cells["Direccion"].Value.ToString();
                txttelefonocliente.Text = dgvClientes.CurrentRow.Cells["Telefono"].Value.ToString();
                txtcorreocliente.Text = dgvClientes.CurrentRow.Cells["Correo"].Value.ToString();
            }
        }

        private void Editar_Clientes_Enter(object sender, EventArgs e)
        {
            MostrarComboCliente(cmbclienteedi);
        }

        private void LimpiarDatosCli()
        {
            txtboxdocumentocli.Clear();
            txtnombrecliente.Clear();
            txtdireccioncliente.Clear();
            txttelefonocliente.Clear();
            txtcorreocliente.Clear();
        }

        private void btnguardarcmCLI_Click(object sender, EventArgs e)
        {
            try
            {
                ConseguirDatosCliente(txtboxdocumentocli,txtnombrecliente,txtdireccioncliente,txttelefonocliente,txtcorreocliente);
                oCN_Clientes.EditarCli(oCE_Clientes);
                MessageBox.Show("Editado Correctamente");
                LimpiarDatosCli();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void Eliminar_Cliente_Enter(object sender, EventArgs e)
        {
            MostrarComboCliente(cmbclienteeliminar);
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¡Esta seguro que desea Eliminar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                oCE_Clientes.Documento = Convert.ToInt32(cmbclienteeliminar.SelectedValue);
                oCN_Clientes.EliminarCli(oCE_Clientes);
                MessageBox.Show("Se ha eliminado");
                MostrarComboCliente(cmbclienteeliminar);
            }
        }

        #endregion

        #region VENDEDORES

        //VENDEDORES

        CE_Vendedores oCE_Vendedores = new CE_Vendedores();
        CN_Vendedores oCN_Vendedores = new CN_Vendedores();

        private void MostrarComboVendedor(ComboBox comboVen)
        {
            comboVen.DataSource = oCN_Vendedores.MostrarVendedor(consultavendedor);
            comboVen.ValueMember = "Codigo";
            comboVen.DisplayMember = "Usuario";
        }

        
        private void MostrarNombresVendedores()
        {
            consultavendedor = "Select * from Vendedores where Codigo=" + cmbvendedor.SelectedValue.ToString();
            dgvVendedores.DataSource = oCN_Vendedores.MostrarVendedor(consultavendedor);
            consultavendedor = "select * from Vendedores";
        }

        private void btnmostrartodovendedor_Click(object sender, EventArgs e)
        {
            dgvVendedores.DataSource = oCN_Vendedores.MostrarVendedor(consultavendedor);
            MostrarComboVendedor(cmbvendedor);
        }

        private void btnconsultarvendedor_Click(object sender, EventArgs e)
        {
            MostrarNombresVendedores();
        }

        private void ConseguirDatosVendedor(TextBox text, TextBox text2, TextBox text3,TextBox text4)
        {
            Encriptar oCN_Encriptar = new Encriptar();
            oCE_Vendedores.Codigo = Convert.ToInt32(text.Text);
            oCE_Vendedores.Usuario = text2.Text;
            oCE_Vendedores.Contraseña = Encriptar.GetSHA256(text3.Text);
            oCE_Vendedores.Nombre = text4.Text;
        }

        private void btnguardarvendedores_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrEmpty(txtboxcodigovendedor.Text) ||
                string.IsNullOrEmpty(txtboxusuariovendedor.Text) ||
                string.IsNullOrEmpty(txtboxcontraseñavendedor.Text) ||
                string.IsNullOrEmpty(txtnombrevendedor.Text))
            {
                MessageBox.Show("Debe ingresar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (oCN_Validaciones.ValidarIdVendedor(Convert.ToInt32(txtboxcodigovendedor.Text)))
            {
                MessageBox.Show("Lo sentimos, este codigo ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (oCN_Validaciones.ValidarUsuarioVendedor(txtboxusuariovendedor.Text))
            {
                MessageBox.Show("Lo sentimos, esta usuario ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ConseguirDatosVendedor(txtboxcodigovendedor, txtboxusuariovendedor,txtcontraseñavendedi, txtnombrevendedor);
                oCN_Vendedores.InsertarVendedor(oCE_Vendedores);
                MessageBox.Show("Ingreso Correctamente");
                MostrarComboVendedor(cmbvendedor);
            }
        }

        private void editarvendedor_Enter(object sender, EventArgs e)
        {
            MostrarComboVendedor(cmbvendedoresedi);
        }

        private void btnlimpiarvendedores_Click(object sender, EventArgs e)
        {
            txtboxcodigovendedor.Clear();
            txtboxusuariovendedor.Clear();
            txtboxcontraseñavendedor.Clear();
            txtnombrevendedor.Clear();
        }

        private void btnconsultarvendedoredi_Click_1(object sender, EventArgs e)
        {
            MostrarNombresVendedores();

            if (cmbvendedoresedi.SelectedIndex >= 0)
            {
                txtcodigovendedor.Text = dgvVendedores.CurrentRow.Cells["Codigo"].Value.ToString();
                txtusuariovendedi.Text = dgvVendedores.CurrentRow.Cells["Usuario"].Value.ToString();
                txtnombrevendedoredi.Text = dgvVendedores.CurrentRow.Cells["Nombre"].Value.ToString();
            }
        }

        private void consultarvendedor_Enter(object sender, EventArgs e)
        {
            dgvVendedores.DataSource = oCN_Vendedores.MostrarVendedor(consultavendedor);
            MostrarComboVendedor(cmbvendedor);
        }

        private void LimpiarDatosVendedor()
        {
            txtcodigovendedor.Clear();
            txtusuariovendedi.Clear();
            txtnombrevendedoredi.Clear();
        }

        private void btnguardarcambiosvendedor_Click(object sender, EventArgs e)
        {
            try
            {
                ConseguirDatosVendedor(txtcodigovendedor,txtusuariovendedi,txtcontraseñavendedi,txtnombrevendedoredi);
                oCN_Vendedores.EditarVendedor(oCE_Vendedores);
                MessageBox.Show("Editado Correctamente");
                LimpiarDatosVendedor();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void eliminarvendedor_Enter(object sender, EventArgs e)
        {
            MostrarComboVendedor(cmbvendedorEli);
        }

        private void btneliminarvendedor_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¡Esta seguro que desea Eliminar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                oCE_Vendedores.Codigo = Convert.ToInt32(cmbvendedorEli.SelectedValue);
                oCN_Vendedores.EliminarVendedor(oCE_Vendedores);
                MessageBox.Show("Se ha eliminado");
                MostrarComboVendedor(cmbvendedorEli);
            }
        }

        #endregion

        #region FACTURACION
        //FACTURACION----------------------------------------------------------------------------------
        private void Principal_Load(object sender, EventArgs e)
        {
            MostrarDataGridView();
            MostrarNFactura();
            MostrarComboCliente(cmbclientesfactura);
            mostrarcombo(cmbproductosfactura);
        }

        CN_Factura oCN_Factura = new CN_Factura();
        int factura = 0;
        List<int> lista = new List<int>();

        private void MostrarNFactura()
        {
            if (oCN_Factura.MostrarNFactura() != " ")
            {
                factura = Convert.ToInt32(oCN_Factura.MostrarNFactura()) + 1;
                txtNumerofact.Text = factura.ToString();
            }
            else
                txtNumerofact.Text = 1.ToString();
        }

        private void facturacion_Enter(object sender, EventArgs e)
        {

        }
        System.Data.DataTable tabla2 = new System.Data.DataTable();
        private void MostrarDataGridView()
        {
            tabla2.Columns.Add("Codigo Producto");
            tabla2.Columns.Add("Producto");
            tabla2.Columns.Add("Valor Unitario");
            tabla2.Columns.Add("Cantidad");
            tabla2.Columns.Add("Subtotal");
           
            dgvfacturacion.DataSource = tabla2;
            dgvfacturacion.Columns["Codigo Producto"].Visible = false;

        }

        private int TotalFactura()
        {
            int Valor_Unidad = 0;
            foreach (DataRow filas in tabla2.Rows)
            {
                Valor_Unidad += Convert.ToInt32(filas["Subtotal"]);
            }
            int valor = Valor_Unidad;
            return valor;
        }

        private void LimpiarAgregar()
        {
            cmbProductos.SelectedIndex = -1;
            txtboxcantidadfac.Clear();
        }

        private void LimpiarTodoFactura()
        {
            cmbClientes.SelectedIndex = -1;
            cmbProductos.SelectedIndex = -1;
            txtboxcantidadfac.Clear();
            txtboxtotalfactura.Clear();
        }

        private void EliminarDataGridView()
        {
            System.Data.DataTable dt= (System.Data.DataTable)dgvfacturacion.DataSource;
            dt.Clear();
        }


        CE_Factura oCE_Factura = new CE_Factura();
        CE_Detalle_Factura oCE_Detalles_Fact = new CE_Detalle_Factura();


        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtboxcantidadfac.Text) >= 1)
                {
                    DataRow filas = tabla2.NewRow();
                    CE_Productos producto = new CE_Productos();
                    int valor_unitario = 0;

                    oCE_Detalles_Fact.Cod_Productos = Convert.ToInt32(cmbproductosfactura.SelectedValue);
                    int cant = Convert.ToInt32(oCN_Factura.TraerCantidad(oCE_Detalles_Fact));

                    if (cant - Convert.ToInt32(txtboxcantidadfac.Text) >= 0)
                    {


                        filas["Codigo Producto"] = cmbproductosfactura.SelectedValue;
                        filas["Producto"] = cmbproductosfactura.Text;
                        lista.Add(Convert.ToInt32(cmbproductosfactura.SelectedValue));
                        filas["Cantidad"] = txtboxcantidadfac.Text;
                        producto.cod = Convert.ToInt32(cmbproductosfactura.SelectedValue);
                        valor_unitario = Convert.ToInt32(oCN_Productos.MostrarValorProducto(producto));
                        filas["Valor Unitario"] = valor_unitario;
                        filas["Subtotal"] = Convert.ToInt32(txtboxcantidadfac.Text) * Convert.ToInt32(oCN_Productos.MostrarValorProducto(producto));
                        tabla2.Rows.Add(filas);
                        txtboxtotalfactura.Text = TotalFactura().ToString();
                        LimpiarAgregar();

                    }
                    else
                        MessageBox.Show("No hay suficiente stock de este producto");
                }
            }
            catch
            {
                MessageBox.Show("Debe ingresar una cantidad","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnterminarfac_Click(object sender, EventArgs e)
        {
            oCE_Factura.Fecha_Factura = dtpfecha.Value;
            oCE_Factura.Documento_Cliente = Convert.ToInt32(cmbClientes.SelectedValue);
            oCE_Factura.Codigo_Vendedor = 1542;
            oCN_Factura.InsertarFactura(oCE_Factura);

            foreach(DataRow filas in tabla2.Rows)
            {
                oCE_Detalles_Fact.Cod_Productos = Convert.ToInt32(filas["Codigo Producto"]);
                oCE_Detalles_Fact.Cantidad = Convert.ToInt32(filas["Cantidad"]);
                oCN_Factura.ActualizarCantidad(oCE_Detalles_Fact);

                oCE_Detalles_Fact.Cod_Productos = Convert.ToInt32(filas["Codigo Producto"]);
                oCE_Detalles_Fact.Numero_Factura = Convert.ToInt32(txtNumerofact.Text);
                oCE_Detalles_Fact.Cantidad = Convert.ToInt32(filas["Cantidad"]);
                oCE_Detalles_Fact.Valor_Unidad = Convert.ToInt32(filas["Valor Unitario"]);              
                oCN_Factura.InsertarDetalleFactura(oCE_Detalles_Fact);
               
            }
            MostrarNFactura();
            EliminarDataGridView();
            LimpiarTodoFactura();
        }
        #endregion

        #region INVENTARIO

        //INVENTARIO

        private void inventario_Enter(object sender, EventArgs e)
        {
            mostrarcombo(cmbinventario);
        }

        private void MostrarInventarioConsultar()
        {
            CE_Productos inventario1= new CE_Productos();
            inventario1.cod = Convert.ToInt32(cmbinventario.SelectedValue);
            dgvInventario.DataSource= oCN_Productos.MostrarInventarioConsultar(inventario1);
        }

        private void MostrarInventarioMostrarTodo()
        {
            dgvInventario.DataSource = oCN_Productos.MostrarInventarioMostrarTodo();
        }

        private void btnmostrarinventario_Click(object sender, EventArgs e)
        {
            MostrarInventarioMostrarTodo();
        }
        private void btnconsultarinventario_Click(object sender, EventArgs e)
        {
            MostrarInventarioConsultar();
        }

        #endregion

        #region VALIDACIONES
        
        private void Solonum(object cajastxt)
        {
            TextBox cajita = cajastxt as TextBox;
            bool error = false;
            foreach (char c in cajita.Text) 
            {
                if (!char.IsDigit(c)) 
                {
                    error = true;  
                    break;
                }

            }

            if (error == true)
            {
                ERRORPROVIDER1.SetError(cajita, "Solo se puede numero");
            }
            else
            {
                ERRORPROVIDER1.SetError(cajita, "");
            }
        }

        //LETRA ESPACIOS
        private bool LetraEspacio(object objeto)
        {
            TextBox texto = objeto as TextBox;
            bool error = false;
            foreach (char c in texto.Text)
            {
                if (!char.IsLetter(c) && c != ' ') // Agrega una condición para permitir espacios en blanco
                {
                    error = true;
                    break;
                }
            }
            if (error == true)
            {
                ERRORPROVIDER1.SetError(texto, "Solo se pueden letras"); // Modifica el mensaje de error para incluir espacios en blanco
            }
            else
            {
                ERRORPROVIDER1.SetError(texto, "");
            }
            return error;
        }

        private void txtboxvalorU_TextChanged(object valoru, EventArgs e)
        {
            Solonum(valoru);
        }

        private void txtboxcantidad_TextChanged(object cantidad, EventArgs e)
        {

            Solonum(cantidad);
        }

        private void txtboxcodigo_TextChanged(object codigo, EventArgs e)
        {
            Solonum(codigo);
        }

        private void txtboxvalor_TextChanged(object valor, EventArgs e)
        {
            Solonum(valor);
        }

        private void txtboxcant_TextChanged(object cant, EventArgs e)
        {
            Solonum(cant);
        }

        private void txtboxdocumento_TextChanged(object docu, EventArgs e)
        {
            Solonum(docu);
        }

        private void txtboxtelefono_TextChanged(object telefono, EventArgs e)
        {
            Solonum(telefono);
        }

        private void txttelefonocliente_TextChanged(object phone, EventArgs e)
        {
            Solonum(phone);
        }

        private void txtboxcodigovendedor_TextChanged(object vendedor, EventArgs e)
        {
            Solonum(vendedor);
        }

        private void txtboxcantidadfac_TextChanged(object cantfac, EventArgs e)
        {
            Solonum(cantfac);
        }

        private void txtboxnombre_TextChanged(object nombre, EventArgs e)
        {
            LetraEspacio(nombre);
        }

        private void txtnombrecliente_TextChanged(object nombrecli, EventArgs e)
        {
            LetraEspacio(nombrecli);
        }

        private void txtnombrevendedor_TextChanged(object sender, EventArgs e)
        {
            LetraEspacio(sender);
        }

        private void txtnombrevendedoredi_TextChanged(object vendedor, EventArgs e)
        {
            LetraEspacio(vendedor);
        }

        private void txtnombrevendedor_TextChanged_1(object sender, EventArgs e)
        {
            LetraEspacio(sender);
        }

        #endregion

        #region EXPORTAR EXCEL Y PDF DATAGRIDVIEW PRODUCTOS
        //EXPORTAR A EXCEL EL DATAGRIDVIEW DE PRODUCTOS
        public void ExportarExcel(DataGridView Dgvproductos)
        {
            Microsoft.Office.Interop.Excel.Application exportar = new Microsoft.Office.Interop.Excel.Application();

            exportar.Application.Workbooks.Add(true);

            int Indiceclumna = 0;

            foreach(DataGridViewColumn columns in dgvProductos.Columns) //RECORRA LAS COLUMNAS DEL DATA GRID VIEW PRODUCTOS
            {
                Indiceclumna++;
                exportar.Cells[1,Indiceclumna] = columns.Name;
            }

            int indiceFila=0;

            foreach(DataGridViewRow Fila in dgvProductos.Rows)
            {
                indiceFila++;
                Indiceclumna = 0;

                foreach(DataGridViewColumn columns in dgvProductos.Columns)
                {
                    Indiceclumna++;

                    exportar.Cells[indiceFila+1,Indiceclumna]= Fila.Cells[columns.Name].Value;
                }
            }
            exportar.Visible= true;
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            ExportarExcel(dgvProductos);
        }

        //PDF
        private void btnexportarpdf_Click(object sender, EventArgs e)
        {
            FileStream filestream = new FileStream(@"C:\Users\Sena CSET\Downloads\PDF\pdfproductos.PDF",FileMode.Create);
            {
                Document document= new Document(PageSize.A4);
                PdfWriter pdfwriter = PdfWriter.GetInstance(document,filestream);
                document.Open();

                PdfPTable pdftable = new PdfPTable(dgvProductos.Columns.Count);

                foreach(DataGridViewColumn columns in dgvProductos.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(columns.HeaderText));
                    pdftable.AddCell(cell);
                }

                if (dgvProductos.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdftable.AddCell(cell.Value?.ToString() ?? "");
                        }
                    }
                }
                else
                    MessageBox.Show("Error al insertar el PDF");
                document.Add(pdftable);
                document.Close();
            }
        }


        #endregion

        #region SALIR
        private void btnsalir_Click(object sender, EventArgs e)
        {
            INICIO_SESION inicio = new INICIO_SESION();
            this.Hide();
            inicio.Show();
        }

        private void btnsalir2_Click(object sender, EventArgs e)
        {
            INICIO_SESION inicio = new INICIO_SESION();
            this.Hide();
            inicio.Show();
        }

        private void btnsalir3_Click(object sender, EventArgs e)
        {
            INICIO_SESION inicio = new INICIO_SESION();
            this.Hide();
            inicio.Show();
        }

        private void btnsalir4_Click(object sender, EventArgs e)
        {
            INICIO_SESION inicio = new INICIO_SESION();
            this.Hide();
            inicio.Show();
        }

        private void btnsalir5_Click(object sender, EventArgs e)
        {
            INICIO_SESION inicio = new INICIO_SESION();
            this.Hide();
            inicio.Show();
        }

        #endregion

    }

}
