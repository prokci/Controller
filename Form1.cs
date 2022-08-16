using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




using System.IO;
using System.IO.Ports; // для работы с СОМ портом

using System.Timers; // Для работы с таймером

namespace Датчик
{
    public partial class Form1 : Form
    {
        private SerialPort port;
        int  flag = 1;

        const long KEY_TEMP = 0x31;
        const long KEY_LIGHT = 0x32;
        const long KEY_REGIST = 0x33;

        const string LOADING = "Ожидание ответа";
        const string NOT_LOADING = "Всё готово";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            port.RtsEnable = true;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];
            port.Read(buffer, 0, bytes);
            for (int i = 0; i < buffer.Length; i++)
            {
                string dex = "" + buffer[i];
                string hex =Convert.ToString( Convert.ToInt32(dex, 10),16);
               
                if (flag == 1)
                {
                    label2.Invoke(new Action(() => label2.Text = "                " + hex + " - " + dex + "\n" + label2.Text));
                    label10.Invoke(new Action(() => label10.Text = NOT_LOADING));
                    double volts = buffer[i] / 100.0d;
                    label13.Invoke(new Action(() => label13.Text = volts + "B"));
                }
                else if (flag == 2)
                {
                    label5.Invoke(new Action(() => label5.Text = "             " + hex + " - " + dex + "\n" + label5.Text));
                    label11.Invoke(new Action(() => label11.Text = NOT_LOADING));
                    if (buffer[i] > 100) dex = "100";
                    label14.Invoke(new Action(() => label14.Text =dex +"%"));
                }
                else
                {
                    label8.Invoke(new Action(() => label8.Text = "                                       " + hex + " - " + dex + "\n" + label8.Text));
                    label12.Invoke(new Action(() => label12.Text = NOT_LOADING));
                }
            }

        }
       
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            port.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label10.Text = LOADING; 
            port.Write(BitConverter.GetBytes(KEY_TEMP), 0, BitConverter.GetBytes(KEY_TEMP).Length); 
            flag = 1;

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label11.Text = LOADING;
            port.Write(BitConverter.GetBytes(KEY_LIGHT), 0, BitConverter.GetBytes(KEY_LIGHT).Length);
            flag = 2;

           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label12.Text = LOADING;
            port.Write(BitConverter.GetBytes(KEY_REGIST), 0, BitConverter.GetBytes(KEY_REGIST).Length);
            flag = 3;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
