using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;


namespace KinectGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            btnStart.Enabled = false;
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
        }


        private void btnFile_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = DateTime.Now + "";
            lblFileName.Text = "The file name is " + KinectSensorClass.name + ".txt";
            btnStart.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //KinectSensorClass.lists[KinectSensorClass.listCounter].Add(DateTime.Now.Second);
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

        public void setLabelFile(String input)
        {
            lblFileName.Text = input;
        }
    }
}
