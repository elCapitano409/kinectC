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
        public LinkedList<object> list;
        public int a = 0;
        public Form1()
        {
            InitializeComponent();
            list = new LinkedList<object>();
        }

        public void setLabel1(String input)
        {
            label1.Text = input;
        }

        public void addChart1(double a)
        {

        }

        public void addChart2(double a)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //KinectSensorClass.runSensor();
            list.AddFirst(a);
            a++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //KinectSensorClass.sensor.Close();
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
