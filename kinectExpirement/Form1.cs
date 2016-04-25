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
	/// <summary> Inheriated from Form class, GUI for incoder and Kinect handling. </summary>
    public partial class KinectForm : Form
    {
        /// <summary> The time stamp that the sensors start recording. </summary>
        public string timeStart;
        /// <summary> The time stamp that the sensors stop recording. </summary>
        public string timeEnd;
        public double startStamp, endStamp, duration;
        private Graphics grahics;
        private Pen pen_black = new Pen(Color.Black, pen_width);
        private Color red = ColorTranslator.FromHtml("#ED1C24");
        private Color yellow = ColorTranslator.FromHtml("#FFF200");
        /// <summary> Determines if the values read in should be written to a file. </summary>
        public bool write = false;
        private bool open = false;
        private Color green = ColorTranslator.FromHtml("#20B14C");
		/// <summary> Butterworth filter for filtering angle values. </summary>
        public Butterworth angle_butterworth = new Butterworth(Butterworth.ANGLE);
		/// <summary> Butterworth filter for filtering velocity values. </summary>
        public Butterworth velocity_butterworth = new Butterworth(Butterworth.VELOCITY);
        private List<double> prev = new List<double>(2);
        private List<double> velocity = new List<double>();
        private List<List<double>> collection = new List<List<double>>();
        private List<List<double>> unfiltered = new List<List<double>>();
        private List<double> encoder_filtered = new List<double>();

        private const float pen_width = 4;
		/// <summary> Floating point value for the offset to take of of value from the Kinect. </summary>
        public float kinect_offset = 0;
        private bool first = true;
		/// <summary> The desired velocity of the session. </summary>
        public int velocity_goal;
		/// <summary> Counter of how many velocity values have been calculated </summary>
		public int velocity_counter;
        private double prev_angle;

        public EncoderSensorClass encoder;
        public KinectSensorClass kinect;
        public Upsampler interp;

		/// <summary> Constructor for the KinectForm class. </summary>
        public KinectForm()
        {
            InitializeComponent();
            encoder = new EncoderSensorClass(this);
            kinect = new KinectSensorClass(this);
            interp = new Upsampler();
            btnStop.Enabled = true;
            btnStart.Enabled = true;
            grahics = velocityScale.CreateGraphics();
            rdioVelocity1.Checked = true;
            prev.Add(0);
            prev.Add(0);    
        }
		
		/// <summary> Applies an offset to the value and adds it to the graph. </summary>
		/// <param name = "a"> The orignial input value to add to the chart. </param>
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

        /// <summary> An event handler that checks if the <c>btnStart</c> button had been clicked. Starts recording from the kinect sensor. </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (open)
            {
                encoder.arduino.save_data = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnFileName.Enabled = false;
                write = true;
                timeStart = DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
                startStamp = (double)DateTime.Now.Minute * 60 + (double)DateTime.Now.Second + ((double)DateTime.Now.Millisecond / 1000);

                if (rdioVelocity1.Checked)
                    velocity_goal = 5;
                else if (rdioVelocity2.Checked)
                    velocity_goal = 10;
                else
                    velocity_goal = 15;
            }

        }

        /// <summary> An even handler that checks if the <c>btnStop</c> button had been clicked. Stops the Kinect sensor. </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            encoder.arduino.save_data = false;
            encoder.arduino.read_data = false;
            encoder.arduino.loop_thread = false;
            kinect.sensor.Close();
            timeEnd = DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;
            endStamp = (double)DateTime.Now.Minute * 60 + (double) DateTime.Now.Second + ((double)DateTime.Now.Millisecond / 1000);
            kinect.input_no_filter.Write(kinect.lists);
            //kinect.input.Write(angle_butterworth.Filter(kinect.lists, prev));
            //encoder.input.Write(angle_butterworth.Filter(encoder.input_filtered_values, prev));
            encoder.input_no_filter.Write(encoder.input_values);
            duration = endStamp - startStamp;
            int temp_duration = (int)duration * 1000;
            List<double> upsampled_kinect = interp.UpSample(ref kinect.lists, 150, temp_duration);
            List<double> upsampled_encoder = interp.UpSample(ref encoder.input_values, 150, temp_duration);
            kinect.input_upsampled.Write(upsampled_kinect);
            encoder.input_upsampled.Write(upsampled_encoder);
            kinect.first = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Sensor is off";
            timer1.Stop();
            write = false;
        }

        /// <summary> An event handler that checks if <c>btnFileName</c> has been clicked. Sets the file name to the user input. </summary>
        private void btnFileName_Click(object sender, EventArgs e)
        {
            kinect.name = txtFile.Text;
            btnStart.Enabled = true;
            lblFileName.Text = "The file name is " + kinect.name + ".txt";
        }

		/// <summary> An event handler that checks if <c>btnFile</c> has been clicked. Sets the file name to the date and time. </summary>
        private void btnFile_Click(object sender, EventArgs e)
        {
            kinect.name = DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            lblFileName.Text = "The file name is " + kinect.name + ".txt";
            btnStart.Enabled = true;
            erase(60);
        }
		
		/// <summary> An event handler that checks if <c>btnOffset</c> has been clicked. Sets the Kinect sensor offset from the encoder value. </summary>
        private void btnOffset_Click(object sender, EventArgs e)
        {
            kinect_offset = encoder.kinect_offset;
            lblOffset.Text = "Current Offset: " + kinect_offset;
            draw(60, pen_black);
        }
		
		/// <summary> Calculates the position of the marker on the gradient. </summary>
		/// <param name = "velocity"> The velocity of the elbow movement. </param>
		/// <returns> The pixel position of the line. </returns>
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
		
		/// <summary> Draws black line on the gradient. </summary>
		/// <param name = "placement"> The pixel postion of the line. </param>
		/// <param name = "pen"> The pen object that will be used to draw the line. <param>
        public void draw(int placement, Pen pen)
        {
            Point top, bottom;
            top = new Point(placement, 0);
            bottom = new Point(placement, 75);
            this.grahics.DrawLine(pen, top, bottom);
        }

		/// <summary> Erases the black line one the gradient. </summary>
		/// <param name = "placement"> The pixel position of the black line. </param>
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

		/// <summary> Adds the postion to an array and calculates the velocity. </summary>
		/// <param name = "input"> Value of elbow position. </param>
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

		/// <summary> Sets the text of the <c>velocity_label</c> label. </summary>
		/// <param name = "velocity"> The intantanous velcity of the elbow.</param>
        private void SetVelocity(double velocity)
        {
            velocity_label.Text = "" + velocity;
        }

		/// <summary> Reads in a value from the serial every 8 miliseconds. </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (encoder.arduino.serial.BytesToRead > 0)
                //{
                    //encoder.add(encoder.arduino.readSerial());
                //}
            }
            catch
            {
                Console.WriteLine("ARDUINO NOT CONNECTED");
            }
        }

		/// <summary> Sets the status of the <c>btnStop</c> button. </summary>
        public void setButtonEnabled2(bool value)
        {
            btnStop.Enabled = value;
        }

		/// <summary> Sets the text of the <c>setLabel1</c> label. </summary>
        public void setLabel1(String input)
        {
            lblStatus.Text = input;
        }

		/// <summary> Starts the timer for the arduino. </summary>
        public void startTimer()
        {
            timer1.Start();
        }

		/// <summary> Sets the text of the <c>lblFileName</c> label. </summary>
        public void setLabelFile(string input)
        {
            lblFileName.Text = input;
        }

		/// <summary> Event handler that checks if <c>btnZero</c> has been clicked, zeros the encoder. </summary>
        private void btnZero_Click(object sender, EventArgs e)
        {
            encoder.zero();
        }

		/// <summary> Sets the text of the <c>lblArduinoStatus</c> label. </c>
        public void setArduinoLabel(string input)
        {
            lblArduinoStatus.Text = input;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            encoder.arduino.read_data = true;
            btnOpen.Enabled = false;
            kinect.RunSensor();
            open = true;
            startTimer();
        }        
    }
}
