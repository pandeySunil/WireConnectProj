using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SGS_TEST_SYSTEM_ConsoleAPP
{

    class Program
    {
        public static int rowCount;
        public static int colCount;
        public static Dictionary<string, string> SheetReturValues = new Dictionary<string, string>();
        public static Dictionary<string, int> SheetReturCount = new Dictionary<string, int>();
        public static Excel.Range xlRange;
        public static int loopCount = 1;
        public static string ledOnCode;
        public static string ledOffCode;
        public static string trayCode;
        public static string sWInput;
        public static string switchCode;
        static void Main(string[] args)
        {
            //ReadExcelSheet();
            //InitailizeExelSheet();
            //SheetReturValues.Add("j", Convert.ToString(1));
            //SheetReturValues.Add("i", Convert.ToString(1));
            //while (true)
            //{

            //    ExelSheetValues(Convert.ToInt32(SheetReturValues["j"]), Convert.ToInt32(SheetReturValues["i"]));
            //    SheetReturValues["i"] = Convert.ToString(Convert.ToInt32(SheetReturValues["i"])+1);
            //}
            //  Console.ReadLine();Convert
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Sunil.Pandey\Desktop\CABLETESTSHEET1\CABLETESTSHEET1\sheet1.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            for (int i = 2; i < 12; i++)
            {
                if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                {
                    
                    trayCode = xlRange.Cells[i, 2].Value2.ToString().Trim('/');
                    string temp1 = xlRange.Cells[i, 3].Value2.ToString().Trim('/');
                    string temp2 = xlRange.Cells[i, 5].Value2.ToString().Trim('/');
                    ledOnCode = trayCode + temp1;
                    ledOffCode = trayCode + temp2;
                    switchCode = xlRange.Cells[i, 4].Value2.ToString().Trim('/');
                    sWInput = SwitchPress();
                    
                    Console.WriteLine(switchCode);
                    Console.WriteLine(ledOnCode);
                    if (sWInput == switchCode)
                    {
                        Console.WriteLine(ledOffCode);
                    }


                }
            }
        }
        private static void ReadExcelSheet()
        {

            try
            {
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Sunil.Pandey\Desktop\CABLETESTSHEET1\CABLETESTSHEET1\sheet1.xlsx");
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                rowCount = xlRange.Rows.Count;
                colCount = xlRange.Columns.Count;
                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        //new line
                        if (j == 1)
                            Console.Write("\r\n");

                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)

                        {
                            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                        }
                    }
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);

            }


            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!



        }

        public static Dictionary<string, int> InitailizeExelSheet()
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Sunil.Pandey\Desktop\CABLETESTSHEET1\CABLETESTSHEET1\sheet1.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;
            SheetReturCount.Add("rowCount", rowCount);
            SheetReturCount.Add("colCount", colCount);
            return SheetReturCount;
        }
        public static Dictionary<string, string> ExelSheetValues(int j, int i)
        {

            if (j == 1)
                Console.Write("\r\n");

            //write the value to the console
            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
            {
                var value = xlRange.Cells[i, j].Value2.ToString() + "\t";

                Console.Write(value);
                loopCount++;
                Console.WriteLine(loopCount);
            }
            if (i <= rowCount)
            {

                SheetReturValues["i"] = Convert.ToString(i);

                while (j <= colCount)
                {
                    j++;
                    SheetReturValues["j"] = Convert.ToString(j);
                    ExelSheetValues(j, i);
                }
            }

            return SheetReturValues;

        }

        public static string SwitchPress()
        {
            bool Flag = true;
            while (Flag)
            {

                var readText = Console.ReadLine();
                if (readText != "")
                {
                    return readText;

                }

            }
            return string.Empty;
        }
    }

}
