using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SGS_DEPLOYMENTPROJECT
{
    public partial class IOCheck : Form
    {
        public SerialPort serialPort;
        public string newValue;
        public IOCheck()
        {
            
            InitializeComponent();
            textBox1.Text = "000";
            textBox4.Text = "0";
            textBox6.Text = "00";
            textBox2.Text = "10";
            textBox3.Text = "10";
            textBox5.Text = "10";
            textBox7.Text = "000000";
            serialPort = ArdiunoConnection.IntializeAudiun();
         serialPort.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
                newValue = IncrementValue(textBox1.Text, Convert.ToInt32(textBox2.Text));
                textBox1.Text = "00"+newValue;
            
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newValue =  DecrementValue(textBox1.Text);
            textBox1.Text = "00"+newValue;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            newValue = IncrementValue(textBox4.Text, Convert.ToInt32(textBox3.Text));
            textBox4.Text = newValue;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            newValue = DecrementValue(textBox4.Text);
            textBox4.Text = newValue;
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
                newValue = IncrementValue(textBox6.Text,Convert.ToInt32(textBox5.Text));
            textBox6.Text = "0"+newValue;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            newValue = DecrementValue(textBox6.Text);
            textBox6.Text = "0"+newValue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendToAurdino(newValue+"1");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendToAurdino(newValue);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendToAurdino(newValue);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendToAurdino(textBox6.Text+textBox4.Text+"1");
        }

        private string IncrementValue(string s, int i) {

            if ( Convert.ToInt32(s) <=i) {
                var temp = Convert.ToInt32(s);
                temp++;
                return Convert.ToString(temp);
            }
            return "";
        }
        private string DecrementValue(string s)
        {
            if (Convert.ToInt32(s)>0) { 
            var temp = Convert.ToInt32(s) - 1;
            return Convert.ToString(temp);
        }
        return "";
        }

        private void SendToAurdino(string s) {

           serialPort.WriteLine(s);

        }

        private void ReadAurdino() {
            var flag = true;
            while (flag) {
                if (serialPort.ReadLine() != "") {
                    textBox8.Text = serialPort.ReadLine();
                    flag = false;
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReadAurdino();
        }
    }
}
