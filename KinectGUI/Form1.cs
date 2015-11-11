using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectGUI
{
    public partial class Form1 : Form
    {
        public LinkedList<double> list;
        public int counter = 0;
        public Form1()
        {
            InitializeComponent();
            list = new LinkedList<double>();
            button2.Enabled = false;
            button1.Enabled = false;
        }

        public void setLabel1(String input)
        {
            label1.Text = input;
        }

        public void addChart1(double a)
        {
            counter++;
            chart1.Series["Series1"].Points.AddXY(counter, a);
        }

        public void addChart2(double a)
        {
            chart2.Series["Series1"].Points.AddXY(counter, a);
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

        private void clearChart1()
        {
            chart1.Series["PositionTime"].Points.Clear();
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
