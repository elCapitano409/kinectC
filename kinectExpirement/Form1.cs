using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;


namespace kinectExpirement
{
    public partial class Form1 : Form
    {
        private Graphics grahics;
        private static float penWidth = 4;
        private Pen penBlack = new Pen(Color.Black, penWidth);
        private System.Drawing.Pen[] penColor;

        public Form1()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            btnStart.Enabled = false;
            grahics = velocityScale.CreateGraphics();
        }

        public void addChart(double a)
        {
            //Need to take away the offset to have proper angle values
            double angle_offset = Convert.ToDouble(txtOffset.Text.ToString());
            double angle = -(a - angle_offset);
            lblAngleTitle.Text = "Angle: " + angle;
            if (chart.Series["Series1"].Points.Count < 300)
            {
                chart.Series["Series1"].Points.Add(angle);
            }
            else
            {
                chart.Series["Series1"].Points.Add(angle);
                chart.Series["Series1"].Points.RemoveAt(0);
            }
        }

        //Starts the sensor
        private void btnStart_Click(object sender, EventArgs e)
        {
            KinectSensorClass.RunSensor();
            KinectSensorClass.lists.Add(new List<double>());
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnFileName.Enabled = false;

        }

        //Stops the sensor
        private void btnStop_Click(object sender, EventArgs e)
        {
            KinectSensorClass.sensor.Close();
            KinectSensorClass.input.Write(KinectSensorClass.lists);
            KinectSensorClass.first = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Sensor is off";
            timer1.Stop();
        }

        //Sets the file name to the input from txtFile textbox
        private void btnFileName_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = txtFile.Text;
            btnStart.Enabled = true;
            lblFileName.Text = "The file name is " + KinectSensorClass.name + ".txt";
            draw(60);
        }


        private void btnFile_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = DateTime.Now + "";
            lblFileName.Text = "The file name is " + KinectSensorClass.name + ".txt";
            btnStart.Enabled = true;
            erase(60);//doesn't work
        }

        public void draw(int placement)
        {
            Point top, bottom;
            top = new Point(placement, 0);
            bottom = new Point(placement, 75);
            this.grahics.DrawLine(penBlack, top, bottom);
        }

        public void erase(int placement)
        {
            Color color;
            Pen penTemp;
            if (placement <= 75 || placement >= 325)
                color = Color.Red;
            else if ((placement >= 75 && placement < 160) || (placement >= 240 && placement <= 325))
                color = Color.Yellow;
            else
                color = Color.Green;
            penTemp = new Pen(color, penWidth);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //KinectSensorClass.lists[KinectSensorClass.listCounter].Add(DateTime.Now.Second);
        }

        public void setArduinoLabel(string text)
        {
            lblArduinoStatus.Text = text;
        }

        public void setButtonEnabled2(bool value)
        {
            btnStop.Enabled = value;
        }

        public void setLabel1(String input)
        {
            lblStatus.Text = input;
        }

        public void startTimer()
        {
            timer1.Start();
        }

        public void setLabelFile(string input)
        {
            lblFileName.Text = input;
        }

    }
}
