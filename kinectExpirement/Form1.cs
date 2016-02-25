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
        private Color red = ColorTranslator.FromHtml("#ED1C24");
        private Color yellow = ColorTranslator.FromHtml("#FFF200");
        private Color green = ColorTranslator.FromHtml("#20B14C");
        public float kinect_offset = EncoderSensorClass.kinect_offset;
        public int velocity_goal;

        public Form1()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            btnStart.Enabled = false;
            grahics = velocityScale.CreateGraphics();
            rdioVelocity1.Checked = true;
        }

        public void addChart(double a)
        {
            //Need to take away the offset to have proper angle values
            double angle_offset = kinect_offset;
            double angle = -(a - angle_offset);
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

            if (rdioVelocity1.Checked)
                velocity_goal = 5;
            else if (rdioVelocity2.Checked)
                velocity_goal = 10;
            else
                velocity_goal = 15;

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
        }


        private void btnFile_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            lblFileName.Text = "The file name is " + KinectSensorClass.name + ".txt";
            btnStart.Enabled = true;
            erase(60);
        }

        private void btnOffset_Click(object sender, EventArgs e)
        {
            kinect_offset = EncoderSensorClass.kinect_offset;
            lblOffset.Text = "Current Offset: " + kinect_offset;
            draw(60, penBlack);
        }

        public void draw(int placement, Pen pen)
        {
            Point top, bottom;
            top = new Point(placement, 0);
            bottom = new Point(placement, 75);
            this.grahics.DrawLine(pen, top, bottom);
        }

        public void erase(int placement)
        {
            Color color;
            Pen penTemp;
            if (placement <= 75 || placement >= 325)
                color = red;
            else if ((placement >= 75 && placement < 160) || (placement >= 240 && placement <= 325))
                color = yellow;
            else
                color = green;
            penTemp = new Pen(color, penWidth);
            draw(placement, penTemp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            EncoderSensorClass.add(EncoderSensorClass.arduino.readSerial());
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

        private void btnZero_Click(object sender, EventArgs e)
        {
            EncoderSensorClass.zero();
        }

        public void setArduinoLabel(string input)
        {
            lblArduinoStatus.Text = input;
        }

        
    }
}
