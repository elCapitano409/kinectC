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
            button2.Enabled = false;
            button1.Enabled = false;
        }

        public void addChart(double a)
        {
            //Need to take away the offset to have proper angle values
            double angle_offset = Convert.ToDouble(textBox2.Text.ToString());
            double angle = -(a - angle_offset);
            label4.Text = "Angle: " + angle;
            if (chart2.Series["Series1"].Points.Count < 300)
            {
                chart2.Series["Series1"].Points.Add(angle);
            }
            else
            {
                chart2.Series["Series1"].Points.Add(angle);
                chart2.Series["Series1"].Points.RemoveAt(0);
            }
        }

        public void setLabelFile(String input)
        {
            labelFile.Text = input;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KinectSensorClass.runSensor();
            KinectSensorClass.lists.Add(new List<double>());
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            
        }

        public void startTimer()
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KinectSensorClass.sensor.Close();
            KinectSensorClass.input.write(KinectSensorClass.lists);
            button1.Enabled = true;
            button2.Enabled = false;
            label1.Text = "Sensor is off";
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = textBox1.Text;
            button1.Enabled = true;
            labelFile.Text = "The file name is " + KinectSensorClass.name + ".txt";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KinectSensorClass.name = DateTime.Now + "";
            labelFile.Text = "The file name is " + KinectSensorClass.name + ".txt";
            button1.Enabled = true;
        }

        public void setButtonEnabled2(bool value)
        {
           button2.Enabled = value;
        }

        public void setLabel1(String input)
        {
            label1.Text = input;
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            KinectSensorClass.lists[KinectSensorClass.listCounter].Add(DateTime.Now.Second);
        }

    }
}
