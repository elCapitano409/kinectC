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
            button1.Enabled = true;

        }

        public void setLabel1(String input)
        {
            label1.Text = input;
        }

        /*public void addChart1(double a)
        {
            //You must use Add() instead of AddXY() for the data window sliding to work
            //Also, I set the scale of the X axis to static values of 0-300 instead of auto-scaling
            if (chart1.Series["Series1"].Points.Count < 300)
            {
                chart1.Series["Series1"].Points.Add(a);
            }
            else
            {
                chart1.Series["Series1"].Points.Add(a);
                chart1.Series["Series1"].Points.RemoveAt(0);
            }
        }*/

        public void addChart2(double a)
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
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            KinectSensorClass.sensor.Close();
            button1.Enabled = true;
            button2.Enabled = false;
            label1.Text = "Sensor is off";
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
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
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

    }

    public class Record
    {
        public int time;
        public double angle;
        public Record(int time, double angle)
        {
            this.time = time;
            this.angle = angle;
        }
    }

}
