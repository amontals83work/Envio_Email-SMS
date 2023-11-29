using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace EnvioEmailSMS
{
    public partial class Form1 : Form
    {
        ConexionDB conn = new ConexionDB();
        MCCommand mcComm = new MCCommand();
        private OpenFileDialog openFileDialog;

        public Form1()
        {
            InitializeComponent();
            rBtnSMS.AutoCheck = true;
            openFileDialog = new OpenFileDialog();
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboCartera.DataSource = conn.Select("SELECT DISTINCT CASE WHEN idCliente in (84, 85, 86, 87, 88) THEN 84 ELSE idCliente END idCliente, CASE WHEN idCliente IN (84, 85, 86, 87, 88) THEN 'ARBORKNOT' ELSE Descripcion END Descripcion FROM clientes WHERE idcliente IN (41, 81, 83, 84, 85, 86, 87, 88)");
            cboCartera.DisplayMember = "Descripcion";
            cboCartera.ValueMember = "idCliente";
        }

        private void btnFichero_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFichero.Text = openFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un fichero para cargar los expedientes");
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarLista(Convert.ToInt32(cboCartera.SelectedValue));            
        }
        
        private void GuardarLista(int id)
        {
            string hoja = nHoja();

            try
            {
                OleDbConnection oleConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFichero.Text.Trim() + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';");
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter("SELECT * FROM [" + hoja + "$]", oleConnection);

                DataSet ds = new DataSet();
                oleAdapter.Fill(ds);
                oleConnection.Close();

                DataTable dt = ds.Tables[0];
                ConexionDB.AbrirConexion();

                if (rBtnSMS.Checked)
                {
                    switch (id)
                    {
                        case 41://TDX
                            {
                                string tabla = "Acciones";
                                string campo = "IdExpediente";
                                Inserciones_SMS_TDX(dt, tabla, campo);
                                break;
                            }
                        case 81://AXACTOR
                            {
                                string tabla = "AXALLAMADAS"; //AXALLAMADAS
                                string campo = "idclienteald";
                                Inserciones_SMS_AXACTOR(dt, tabla, campo);
                                break;
                            }
                        case 83://NASSAU
                            {
                                string tabla = "NASSLLAMADAS";
                                string campo = "codigoparticipante";
                                Inserciones_SMS_NASSAU(dt, tabla, campo);
                                break;
                            }
                        case 84://ARBORKNOT
                            {
                                string tabla = "Acciones";
                                string campo = "IdExpediente";
                                Inserciones_SMS_ARBORKNOT(dt, tabla, campo);
                                break;
                            }
                        default:
                            MessageBox.Show("Cliente no contemplado en la aplicación.");
                            break;
                    }
                }
                else if (rBtnEmail.Checked)
                {
                    switch (id)
                    {
                        case 41://TDX
                            {
                                string tabla = "Acciones";
                                string campo = "IdExpediente";
                                Inserciones_EMAIL_TDX(dt, tabla, campo);
                                break;
                            }
                        case 81://AXACTOR
                            {
                                string tabla = "AXALLAMADAS"; //_Pruebas_AXACTOR//AXALLAMADAS
                                string campo = "idclienteald";
                                Inserciones_EMAIL_AXACTOR(dt, tabla, campo);
                                break;
                            }
                        case 83://NASSAU
                            {
                                string tabla = "NASSLLAMADAS";
                                string campo = "codigoparticipante";
                                Inserciones_EMAIL_NASSAU(dt, tabla, campo);
                                break;
                            }
                        case 84://ARBORKNOT
                            {
                                string tabla = "Acciones";
                                string campo = "IdExpediente";
                                Inserciones_EMAIL_ARBORKNOT(dt, tabla, campo);
                                break;
                            }
                        default:
                            MessageBox.Show("Cliente no contemplado en la aplicación.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar SMS o E-mail.");
                }
                ConexionDB.CerrarConexion();

            }
            catch (Exception e)
            {
                MessageBox.Show("error " + e);
            }
        }

        //COLUMNA 1º 'EXPEDIENTE', 2ª COLUMNA TELEFONO/EMAIL
        private void Inserciones_SMS_TDX(DataTable dt, string tabla, string campo)
        {
            //insert into acciones(IdExpediente, idTipoNota, Fecha, Observaciones, Usuario, Telefono, Hermes, Borrado, Activa)
            //values(idexpediente,788,getdate(),'Enviado SMS','Automarcador',telefono,0,0,1)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string telefono = fila[1].ToString();

                mcComm.CommandText = "SELECT idExpediente FROM Expedientes WHERE RefCliente=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", expediente);

                int result = Convert.ToInt32(mcComm.ExecuteScalar());
                if (result != 0)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",idTipoNota,Fecha,Observaciones,Usuario,Telefono,Hermes,Borrado,Activa) VALUES (@value1,788,GETDATE(),'Enviado SMS','Automarcador',@value2,0,0,1)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", result);
                    mcComm.command.Parameters.AddWithValue("@value2", telefono);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }

        private void Inserciones_EMAIL_TDX(DataTable dt, string tabla, string campo)
        {
            //email --> insert into acciones(IdExpediente,idTipoNota,Fecha,Observaciones,Usuario,Hermes,Borrado,Activa)
            //values(idexpediente,822,getdate(),'Enviado email'+,'Automarcador',0,0,1)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string email = fila[1].ToString();

                mcComm.CommandText = "SELECT idExpediente FROM Expedientes WHERE RefCliente=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", expediente);

                int result = Convert.ToInt32(mcComm.ExecuteScalar());
                if (result != 0)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",idTipoNota,Fecha,Observaciones,Usuario,Hermes,Borrado,Activa) VALUES (@value1,822,GETDATE(),'Enviado E-mail ' + @value2,'Automarcador',0,0,1)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", result);
                    mcComm.command.Parameters.AddWithValue("@value2", email);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }


        private void Inserciones_SMS_ARBORKNOT(DataTable dt, string tabla, string campo)
        {
            //insert into acciones(IdExpediente, idTipoNota, Fecha, Observaciones, Usuario, Telefono, Hermes, Borrado, Activa)
            //values(idexpediente,788,getdate(),'Enviado SMS','Automarcador',telefono,0,0,1)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string telefono = fila[1].ToString();

                mcComm.CommandText = "SELECT idExpediente FROM Expedientes WHERE cast(Expediente as varchar)=@expediente OR RefCliente=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", expediente);

                int result = Convert.ToInt32(mcComm.ExecuteScalar());
                if (result != 0)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",idTipoNota,Fecha,Observaciones,Usuario,Telefono,Hermes,Borrado,Activa) VALUES (@value1,788,GETDATE(),'Enviado SMS','Automarcador',@value2,0,0,1)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", result);
                    mcComm.command.Parameters.AddWithValue("@value2", telefono);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }

        private void Inserciones_EMAIL_ARBORKNOT(DataTable dt, string tabla, string campo)
        {
            //email --> insert into acciones(IdExpediente,idTipoNota,Fecha,Observaciones,Usuario,Hermes,Borrado,Activa)
            //values(idexpediente,822,getdate(),'Enviado email'+,'Automarcador',0,0,1)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string email = fila[1].ToString();

                mcComm.CommandText = "SELECT idExpediente FROM Expedientes WHERE cast(Expediente as varchar)=@expediente OR RefCliente=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", expediente);

                int result = Convert.ToInt32(mcComm.ExecuteScalar());
                if (result != 0)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",idTipoNota,Fecha,Observaciones,Usuario,Hermes,Borrado,Activa) VALUES (@value1,822,GETDATE(),'Enviado E-mail ' + @value2,'Automarcador',0,0,1)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", result);
                    mcComm.command.Parameters.AddWithValue("@value2", email);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }


        private void Inserciones_SMS_AXACTOR(DataTable dt, string tabla, string campo)
        {
            //insert into AXALLAMADAS(idclienteald,fecha,telefono,gestion,interlocutor,respuesta,observaciones,usuario)
            //values(idclienteald,getdate(),telefono,86,1,20,'SMS','Automarcador')
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string telefono = fila[1].ToString();

                mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",fecha,telefono,gestion,interlocutor,respuesta,observaciones,usuario) VALUES (@value1,GETDATE(),@value2,86,1,20,'Enviado SMS','Automarcador')";
                mcComm.command.Parameters.Clear();
                mcComm.command.Parameters.AddWithValue("@value1", expediente);
                mcComm.command.Parameters.AddWithValue("@value2", telefono);
                mcComm.ExecuteNonQuery();
            }
            MessageBox.Show("Guardado con éxito.");
        }

        private void Inserciones_EMAIL_AXACTOR(DataTable dt, string tabla, string campo)
        {
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string expediente = fila[0].ToString();
                string email = fila[1].ToString();

                mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",fecha,gestion,interlocutor,respuesta,observaciones,usuario) VALUES (@value1,GETDATE(),86,1,20,'Enviado E-mail ' + @value2,'Automarcador')";
                mcComm.command.Parameters.Clear();
                mcComm.command.Parameters.AddWithValue("@value1", expediente);
                mcComm.command.Parameters.AddWithValue("@value2", email);
                mcComm.ExecuteNonQuery();
            }
            MessageBox.Show("Guardado con éxito.");
        }


        //COLUMNA 1º 'EXPEDIENTE', 2ª COLUMNA TELEFONO/EMAIL, 3ª COLUMNA CONTRATO
        private void Inserciones_SMS_NASSAU(DataTable dt, string tabla, string campo)
        {
            //insert into NASSLLAMADAS(codigoparticipante, fecha, telefono, gestion, interlocutor, respuesta, observaciones, usuario, contrato)
            //values(codigoparticipante,getdate(),telefono,86,1,20,'SMS','Automarcador',Contrato)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string codigoParticipante = string.Empty;
                string telefono = fila[1].ToString();
                string contrato = fila[2].ToString();

                mcComm.CommandText = "SELECT CodigoParticipante FROM NASSTITULARES WHERE Contrato=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", contrato);

                codigoParticipante = mcComm.ExecuteScalar().ToString();
                if (codigoParticipante != null)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",fecha,telefono,gestion,interlocutor,respuesta,observaciones,usuario,contrato) VALUES (@value1,GETDATE(),@value2,86,1,20,'Enviado SMS','Automarcador',@value3)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", codigoParticipante);
                    mcComm.command.Parameters.AddWithValue("@value2", telefono);
                    mcComm.command.Parameters.AddWithValue("@value3", contrato);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }

        private void Inserciones_EMAIL_NASSAU(DataTable dt, string tabla, string campo)
        {
            //insert into NASSLLAMADAS(codigoparticipante, fecha, telefono, gestion, interlocutor, respuesta, observaciones, usuario, contrato)
            //values(codigoparticipante,getdate(),telefono,11,1,22,'EMAIL','Automarcador',Contrato)
            mcComm.command.Connection = conn.ObtenerConexion();
            foreach (DataRow fila in dt.Rows)
            {
                string codigoParticipante = string.Empty;
                string email = fila[1].ToString();
                string contrato = fila[2].ToString();

                mcComm.CommandText = "SELECT CodigoParticipante FROM NASSTITULARES WHERE Contrato=@expediente";
                mcComm.command.Parameters.AddWithValue("@expediente", contrato);

                codigoParticipante = mcComm.ExecuteScalar().ToString();
                if (codigoParticipante != null)
                {
                    mcComm.command.CommandText = "INSERT INTO " + tabla + " (" + campo + ",fecha,gestion,interlocutor,respuesta,observaciones,usuario,contrato) VALUES (@value1,GETDATE(),11,1,22,'Enviado E-mail ' + @value2,'Automarcador',@value3)";
                    mcComm.command.Parameters.Clear();
                    mcComm.command.Parameters.AddWithValue("@value1", codigoParticipante);
                    mcComm.command.Parameters.AddWithValue("@value2", email);
                    mcComm.command.Parameters.AddWithValue("@value3", contrato);
                    mcComm.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Guardado con éxito.");
        }

        private string nHoja()
        {
            string hoja;
            var xlsApp = new Excel.Application();
            xlsApp.Workbooks.Open(txtFichero.Text.Trim());

            hoja = xlsApp.Sheets[1].Name;

            xlsApp.DisplayAlerts = false;
            xlsApp.Workbooks.Close();
            xlsApp.DisplayAlerts = true;

            xlsApp.Quit();
            xlsApp = null;

            return hoja;
        }
    }
}