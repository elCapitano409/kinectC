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
    public partial class KinectForm : Form
    {
        private Graphics grahics;
        private Pen pen_black = new Pen(Color.Black, pen_width);
        private Color red = ColorTranslator.FromHtml("#ED1C24");
        private Color yellow = ColorTranslator.FromHtml("#FFF200");
        private Color green = ColorTranslator.FromHtml("#20B14C");
        public Butterworth angle_butterworth = new Butterworth(Butterworth.ANGLE);
        public Butterworth velocity_butterworth = new Butterworth(Butterworth.VELOCITY);
        private List<double> prev = new List<double>(2);
        private List<double> velocity = new List<double>();
        private List<List<double>> collection = new List<List<double>>();
        private List<List<double>> unfiltered = new List<List<double>>();
        private List<double> encoder_filtered = new List<double>();
        private int listDoubleCounter = 0, listListCounter = 0;
        private const float pen_width = 4;
        public float kinect_offset = 0;
        private bool first = true;
        public int velocity_goal, velocity_counter;
        private double prev_angle;

        public KinectForm()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            btnStart.Enabled = false;
            grahics = velocityScale.CreateGraphics();
            rdioVelocity1.Checked = true;
            prev[0] = 0;
            prev[1] = 0;
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
            angle_butterworth.Filter((List<float>)EncoderSensorClass.input_filtered_values, prev);
            EncoderSensorClass.input.Write(EncoderSensorClass.input_values);
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
            draw(60, pen_black);
        }

        public int placement(double velocity)
        {
            int value = 0;
            double temp, distance, max_distance;
            if(velocity == velocity_goal){
                value = 200;
            }
            else if (velocity > velocity_goal)
            { 
                if (velocity < velocity_goal + 1)
                {
                    distance = velocity - velocity_goal;
                    max_distance = 40;
                    temp = distance * max_distance;
                    value = Convert.ToInt32(temp) + 200;
                }
                else if(velocity < velocity_goal + 10)
                {
                    distance = velocity - (velocity_goal + 1);
                    max_distance = 85;
                    temp = distance/9;
                    value = Convert.ToInt32(temp) * 85 + 240;
                }
                else if (velocity < velocity_goal + 20)
                {

                }
            }
            return value;
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
            penTemp = new Pen(color, pen_width);
            draw(placement, penTemp);
        }

        public void addPosition(double input)
        {
            double current_velocity = 0;
            double average_velocity = 0, doub_temp = 0;
            
            if(first){
                first = false;
                prev_angle = input;
            }
            else
            {
                current_velocity = Calculate.FindVelocity(prev_angle, input);
                velocity.Add(current_velocity);
                if(velocity.Count >= 60)
                {
                    if (velocity.Count % 60 == 0)
                    {
                        foreach (double a in velocity)
                        {
                            doub_temp += a;
                        }
                        average_velocity = doub_temp / 60;
                        SetVelocity(average_velocity);

                    }
                    velocity.RemoveAt(0);
                }
                
            }
            
        }

        private void SetVelocity(double velocity)
        {
            velocity_label.Text = "" + velocity;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                EncoderSensorClass.add(EncoderSensorClass.arduino.readSerial());
            }
            catch
            {
                Console.WriteLine("ARDUINO NOT CONNECTED");
            }
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

        public void setVelocityTitleLabel(string input)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
