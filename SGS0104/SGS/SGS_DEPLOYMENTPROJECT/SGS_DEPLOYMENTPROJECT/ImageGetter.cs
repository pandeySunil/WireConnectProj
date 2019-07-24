﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SGS_DEPLOYMENTPROJECT
{
  public  class ImageGetter
    {
        public string filePath { get; set; }
        public Image image { get; set; }
        public Bitmap pbImageBitmap { get; set; }
        public List<JsonShape> jsonShapes;
        public List<shape> drawnShapes = new List<shape>();
        public ImageGetter() {

            try
            {

                using (StreamReader r = new StreamReader(@"C:\Users\Sunil.Pandey\Desktop\CustmImageEditor\CustmImageEditor\ImageConfig.json"))
                {
                    Console.WriteLine("Reading File..");
                    string json = r.ReadToEnd();
                    Console.WriteLine("Reading File Done");
                    jsonShapes = JsonConvert.DeserializeObject<List<JsonShape>>(json);



                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);

            }
            foreach (var r in jsonShapes)
            {
                Pen p = new Pen(Color.Green);
                if (r.penColor == "White")
                {
                    p = new Pen(Color.White);
                }
                else if (r.penColor == "Black")
                {
                    p = new Pen(Color.Black);
                }
                else if (r.penColor == "Yellow")
                {
                    p = new Pen(Color.Yellow);
                }
                else if (r.penColor == "Red")
                {
                    p = new Pen(Color.Red);
                }
                drawnShapes.Add(new shape()
                {
                    x_cortinate = r.x_cortinate,
                    y_cortinate = r.y_cortinate,
                    penSize = r.penSize,
                    reatSize = r.reatSize,
                    reactString = r.reactString,
                    p = p

                });

            }

        }
        public Bitmap GetBitmap(int shapepointer) {
            filePath = "C:\\Users\\Sunil.Pandey\\Desktop\\Capture2.PNG";
            if (shapepointer == 0)
            {

                return new Bitmap(filePath);
            }
            else
            {
               
                image = Image.FromFile(filePath);
                DrawShape(drawnShapes[shapepointer].x_cortinate, drawnShapes[shapepointer].y_cortinate,
                    drawnShapes[shapepointer].reatSize, drawnShapes[shapepointer].p, drawnShapes[shapepointer].penSize, drawnShapes[shapepointer].reactString
                    );
                return pbImageBitmap;
            }
        }
        private void DrawShape(int x, int y, int reacSize, Pen pen, int penSize, string reactString)
        {
             pbImageBitmap = (Bitmap)(image);
          var   graphics = Graphics.FromImage((Image)pbImageBitmap);
            Rectangle rect = new Rectangle(x, y, reacSize, reacSize);
            pen.Width = penSize;
            graphics.DrawRectangle(pen, rect);
          //  pictureBox1.Refresh();
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(pen.Color);
            Font font = new Font("Times New Roman", 12.0f);
            graphics.DrawString(reactString, font, myBrush, x, y);


        }
    }
    public class RectColor
    {
        public Pen p { get; set; }
        public string ColorName { get; set; }

    }
    public class JsonRectColor
    {
        public string ColorName { get; set; }
    }
    public class RectSize
    {
        public int size { get; set; }


    }
    public class PenSize
    {
        public int size { get; set; }

    }
    public class RootObject
    {
        List<JsonShape> jsonShapes { get; set; }
    }
    public class JsonShape
    {
        public int Id { get; set; }
        public int x_cortinate { get; set; }
        public int y_cortinate { get; set; }
        public int penSize { get; set; }
        public int reatSize { get; set; }
        public string penColor { get; set; }
        public string reactString { get; set; }

    }
    public class shape
    {
        public int Id { get; set; }
        public int x_cortinate { get; set; }
        public int y_cortinate { get; set; }
        public int penSize { get; set; }
        public int reatSize { get; set; }
        public Pen p { get; set; }
        public string reactString { get; set; }

    }
}
