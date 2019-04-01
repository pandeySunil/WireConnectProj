using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGS_DEPLOYMENTPROJECT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure about your credentilas");
            var userName = "Sunil.Pandey";
            var password = "Apachertr18@";
            if (textBoxUsername.Text == userName && textBoxPassword.Text == password||true) {
                //if (Application.OpenForms.OfType<Login>().Count() > 0)
                //{
                //    Application.OpenForms.OfType<Login>().First().Close();
                //}

                new Form1().Show();
                
            }

            else {

                lblError.Text = "Wrong Credentials Please Enter New One";


            }
            
            
        }
        

    }
}
