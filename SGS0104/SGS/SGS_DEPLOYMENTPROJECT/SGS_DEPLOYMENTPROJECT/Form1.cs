using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGS_DEPLOYMENTPROJECT
{
    public partial class Form1 : Form
    {
        public bool backgroudThreadSleepFlag;
        BusinessLogic businessLogic { get; set; }
        public static SerialPort SerialPort;
        Microsoft.Office.Interop.Excel.Range xlRange;
        public static int loopCount = 1;
        public static string ledOnCode;
        public static string ledOffCode;
        public static string trayCode;
        public static string sWInput;
        public static string switchCode;
        public Thread BackGroundThread;
        public string imgUrl;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lableLoggedInUser.Text = "Hello, "+Helper.LoggedInUserName;
            if (Helper.ExcelSheetName == "")
            {

                MessageBox.Show("Please Select  Sheet To Be Loaded");
            }
            //SelectExcelFile();

            BackGroundThread = new Thread(() => Navigation());
            BackGroundThread.IsBackground = true;
            //SerialPort = ArdiunoConnection.IntializeAudiun();
            //textBoxDevelopersArea.Text += "Opening Serial Port\\n";
            //SerialPort.Open();
            businessLogic = new BusinessLogic();
            //Helper.ExcelSheetName = "";
           
            
           // textBoxDevelopersArea.Text += "Sheet Intiallized\\n";
            // pictureBox.Dock = DockStyle.Fill;
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // new FileSelect().Show();
            MessageBox.Show("Changing File Will Stop The Current Cycle ");
            SelectExcelFile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("LogOut will Close the Application Do you Wish To Continue ", "LogOut", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                BackGroundThread.Abort();
                Application.Exit();
                LogOut();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackGroundThread.Abort();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
           
            
           // textBoxDevelopersArea.Text += "Navigation Thread is Stated\\n";
            if (Helper.ExcelSheetName != "")
            {
                businessLogic.InitializeExcel();
                xlRange = businessLogic.InitializeExcel();
                BackGroundThread.Start();
            }

            else {
                MessageBox.Show("First Choose The Excel File");
            }
            //new Thread(() => Navigation()) { IsBackground = true }.Start();
        }
        private void Navigation()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            
            
            TextBox.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            SerialPort = ArdiunoConnection.IntializeAudiun();
           // textBoxDevelopersArea.Text += "Opening Serial Port\\n";
            SerialPort.Open();
            businessLogic = new BusinessLogic();
            businessLogic.InitializeExcel();
            xlRange = businessLogic.InitializeExcel();
           // textBoxDevelopersArea.Text += "Sheet Intiallized\\n";

            int Count = 1;

            for (int i = 2; i < 20; i++)
            {
               // textBoxDevelopersArea.Text += "Next Iteration \\n";
                Console.WriteLine(Count);
                if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                {

                    trayCode = xlRange.Cells[i, 2].Value2.ToString().Trim('/');
                    string temp1 = xlRange.Cells[i, 3].Value2.ToString().Trim('/');
                    string temp2 = xlRange.Cells[i, 5].Value2.ToString().Trim('/');
                    Console.WriteLine("Celli,5" + temp2);
                    ledOnCode = trayCode + temp1;
                    ledOffCode = trayCode + temp2;
                    switchCode = xlRange.Cells[i, 4].Value2.ToString().Trim('/');
                    Console.WriteLine("SwitchCode-Sheet: " + switchCode);
                    SerialPort.Write(ledOnCode);
                    sWInput = SwitchPress();
                   // textBoxDevelopersArea.Text += "SwitchCode - Ardiuno: " + sWInput + "\\n";
                    Console.WriteLine("SwitchCode-Ardiuno: " + sWInput);
                    //textBoxDevelopersArea.Text += "SwitchCode - Ardiuno: " + sWInput + "\\n";
                    Console.WriteLine(ledOnCode);

                    var t = switchCode + "\r";

                    var falg = string.Equals(sWInput, t, StringComparison.OrdinalIgnoreCase);
                    if (falg)
                    {
                        SerialPort.Write(ledOffCode);
                        switchCode = xlRange.Cells[i, 6].Value2.ToString().Trim('/');
                        sWInput = SwitchPress();
                        t = switchCode + "\r";
                        falg = string.Equals(sWInput, t, StringComparison.OrdinalIgnoreCase);
                        if (falg)
                        {

                            temp2 = xlRange.Cells[i, 5].Value2.ToString().Trim('/');
                            ledOnCode = trayCode + temp1;
                           // textBoxDevelopersArea.Text += "ledOnCode: " + ledOnCode + "\\n";
                            Console.WriteLine(ledOnCode);
                            SerialPort.Write(ledOnCode);
                            textBoxDescription.Text = xlRange.Cells[i, 7].Value2.ToString().Trim('/');
                           // textBoxDevelopersArea.Text += "DesMag: " + xlRange.Cells[i, 7].Value2.ToString().Trim('/') + "\\n";
                            textBoxPullConnection.Text = xlRange.Cells[i, 8].Value2.ToString().Trim('/');

                            imgUrl = xlRange.Cells[i, 9].Value2.ToString().Trim('/');
                            Bitmap image = new Bitmap("C:\\Users\\Akshay\\Desktop\\SGS\\SGS_MEDIA\\" + imgUrl);
                            pictureBox.Image = (Image)image;
                        }


                    }

                    Count++;
                    if (backgroudThreadSleepFlag) {
                        while (true) {

                        }
                    }
                }
            }
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
            tackTime.Text = elapsedTime;

        }

        private void btnOn_Click(object sender, EventArgs e)
        {



        }
        public static string SwitchPress()
        {
            bool Flag = true;
            while (Flag)
            {

                //var readText = Console.ReadLine();

                var readText = SerialPort.ReadLine();


                if (readText != "")
                {
                    return readText;

                }

            }
            return string.Empty;
        }
        private void SelectExcelFile()
        {
            var filePath = "";
            // Create and open a file selector
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.InitialDirectory = ".";

            // select filter type
            //opnDlg.Filter = "Parm Files (*.parm)|*.parmAll Files (*.*)|*.*";

            if (opnDlg.ShowDialog(this) == DialogResult.OK)
            {
                // If file is in start folder remove path
                FileInfo f = new FileInfo(opnDlg.FileName);

                // use relative path if under application
                // starting directory
                if (f.DirectoryName == Application.StartupPath)
                    filePath = f.Name;  // only file name
                else if (f.DirectoryName.StartsWith(Application.StartupPath))
                    // relative path
                    filePath = f.FullName.Replace(Application.StartupPath, ".");
                else
                    filePath = f.FullName;  // Full path
            }
            Helper.ExcelSheetName = filePath;
            if (Application.OpenForms.OfType<Form1>().Count() > 0)
                Application.OpenForms.OfType<Form1>().First().Close();

            Form1 frm = new Form1();
            frm.Show();

        }
        private void  LogOut() {

            SQLConnectionSetUp conObj = new SQLConnectionSetUp();
            var Con = conObj.GetConn();
            try
            {

                Con.Open();
                SqlCommand cmd = new SqlCommand("LogOut", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", Helper.LoggedInUserName);

                cmd.ExecuteNonQuery();

                }

            catch (Exception Ex)
            {

            }
            finally { Con.Close(); }

            

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroudThreadSleepFlag = true;
            DialogResult dialogResult = MessageBox.Show("Setting Will Terminate The Current Cycle", "Setting", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                BackGroundThread.Abort();
                new SettingForm().Show();
            }
            else if (dialogResult == DialogResult.No)
            {
                backgroudThreadSleepFlag = false;
                return;
            }
            
        }
    }
}