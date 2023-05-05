using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHT11
{
    public partial class Form1 : Form
    {

        string datoSerial = "";
        public Form1()
        {
            InitializeComponent();
            string[] puertos = SerialPort.GetPortNames();
            foreach (string item in puertos)
            {
                CmbPts.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            prtSerial.PortName = CmbPts.SelectedItem.ToString();
            MessageBox.Show("Puerto " + prtSerial.PortName + " seleccionado.");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbrir.Checked)
            {
                try
                {
                    if (!prtSerial.IsOpen)
                    {
                        prtSerial.Open();
                        chkAbrir.Text = "Puerto COM Abierto";
                    }
                }
                catch (Exception error)
                {

                    MessageBox.Show(error.ToString());
                    chkAbrir.Checked = false;
                }
            }
            else
            {
                try
                {
                    if (prtSerial.IsOpen)
                    {
                        prtSerial.Close();
                        chkAbrir.Text = "Puerto COM Cerrado";
                    }
                }
                catch (Exception error)
                {

                    MessageBox.Show(error.ToString());
                    //chkAbrir.Checked = false;
                }
            }
        }

        private void prtSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                prtSerial.ReadTimeout = 100;
                datoSerial = prtSerial.ReadTo("\n");
                Invoke(new EventHandler(MostrarTexto));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                prtSerial.DiscardInBuffer();
            }

        }

        private void MostrarTexto(Object sender, EventArgs e)
        {
            txtSerial.Clear();
            txtSerial.AppendText(datoSerial);
            AnalizarCadena(datoSerial);
        }

        private void AnalizarCadena(String cadena)
        {
            char[] separadores = { ':' , '%' ,'*' };
            string[] trama = cadena.Split(separadores);
            foreach (string item in trama)
            {
                item.Trim();
            }
            int tamano = trama.Length;
            //MessageBox.Show(trama[2]);
            if (trama.Length > 1)
            {
                try
                {
                    grafica.Series[0].Points.Add(Convert.ToDouble(trama[1]));
                    grafica.Series[1].Points.Add(Convert.ToDouble(trama[3]));
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
        }
        
        //Agregar metodo que cierre el puerto COM después de cerrar la aplición 
