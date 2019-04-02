using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;

namespace SGS_DEPLOYMENTPROJECT
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            string stationName;
            string PortName;
            stationName = ConfigurationManager.AppSettings.Get("stationName");
            PortName = ConfigurationManager.AppSettings.Get("portName");

            InitializeComponent();

            textBoxPort.Text = PortName;
            textBoxStationName.Text = stationName;
           // textBoxPort.ReadOnly = true;
            textBoxStationName.ReadOnly = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            textBoxStationName.ReadOnly = false;

            MessageBox.Show("Enter The New Name Of Station In The TextBox");
            
            ConfigurationManager.AppSettings.Set("stationName", textBoxStationName.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // textBoxPort.ReadOnly = false;
            MessageBox.Show("Enter New Port In The Text Box");
            //ConfigurationManager.AppSettings.Set("portName", textBoxPort.Text);
            Configuration configuration = ConfigurationManager.
        OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings["portName"].Value = textBoxPort.Text;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show(ConfigurationManager.AppSettings.Get("portName"));


        }
    }
}
