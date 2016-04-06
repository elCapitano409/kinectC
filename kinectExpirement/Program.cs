using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Fusion;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace kinectExpirement
{
	/// <summary> Holds all the functions for reading in and storeing values from the Kinect, also constrols the state of the sensor. </summary>
    class KinectSensorClass
    {

        public static KinectSensor sensor;
        public static IList<Body> bodies;
        public static bool first = true, checkVelocity = false;
        public static List<double> lists = new List<double>();
        public static List<double> velocityAngle = new List<double>();
        public static int addCounter = 0, listCounter = 0, velocityCounter = 0;
        public static KinectForm form;
        public static String name;
        public static FileProcessing input, kinect_no_filter;


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new KinectForm();
            form.startTimer();
            Application.Run(form);
        }
		/// <summary> Opens and runs the methods for using the Kinect Sensor. </summary>
        public static void RunSensor()
        {
            MultiSourceFrameReader reader;
            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                sensor.Open();
            }
            reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                             FrameSourceTypes.Depth |
                                             FrameSourceTypes.Infrared |
                                             FrameSourceTypes.Body);
            reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
        }
		
		/// <summary> Reads in the input from the Kinect sensor and adds it to the GUI </summary>
        static void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();


            PointHolder wrist = new PointHolder();
            PointHolder elbow = new PointHolder();
            PointHolder shoulder = new PointHolder();
            input = new FileProcessing(name, "KINECT_FILTERED");
            kinect_no_filter = new FileProcessing(name, "KINECT_UNFILTERED");
            EncoderSensorClass.input = new FileProcessing(name, "ENCODER_FILTERED");
            EncoderSensorClass.input_no_filter = new FileProcessing(name, "ENCODER_UNFILTERED");
            double tempVelocity;
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(bodies);

                    foreach (var body in bodies)
                    {
                        if (body.IsTracked)
                        {
                            //If this is the first time running the sensor, the timer is started
                            if (first)
                            {
                                first = false;
                                //form.startTimer
                            }
                            Joint shoulderJoint = body.Joints[JointType.ShoulderRight];
                            Joint elbowJoint = body.Joints[JointType.ElbowRight];
                            Joint wristJoint = body.Joints[JointType.WristRight];
                            double angle;
                            form.setLabel1("Now tracking body");

                            //Set the x,y, and z positions for each joint relative to sensor
                            wrist.x = wristJoint.Position.X;
                            wrist.y = wristJoint.Position.Y;
                            wrist.z = wristJoint.Position.Z;
                            elbow.x = elbowJoint.Position.X;
                            elbow.y = elbowJoint.Position.Y;
                            elbow.z = elbowJoint.Position.Z;
                            shoulder.x = shoulderJoint.Position.X;
                            shoulder.y = shoulderJoint.Position.Y;
                            shoulder.z = shoulderJoint.Position.Z;

                            //Calculate angle with vector method
                            PointHolder vector1 = Calculate.CreateVector(wrist, elbow);
                            PointHolder vector2 = Calculate.CreateVector(shoulder, elbow);
                            double dot_product = Calculate.DotProduct(vector1, vector2);
                            double length_vector1 = Calculate.VectorLength(vector1);
                            double length_vector2 = Calculate.VectorLength(vector2);
                            angle = Calculate.ToDegree(Math.Acos(dot_product / (length_vector1 * length_vector2)));

                            form.addChart(angle);
                            form.addPosition(angle);
                            

                            if (checkVelocity)
                            {
                                velocityAngle.Insert(9, angle);
                                tempVelocity = Calculate.FindVelocity(velocityAngle[0], velocityAngle[59]);
                            }
                            else if (velocityCounter >= 60)
                            {
                                checkVelocity = true;
                                continue;
                            }
                            else
                            {
                                velocityCounter++;
                                velocityAngle.Add(angle);
                            }
                            if(form.write)
                                lists.Add(angle);

                        }
                        else
                        {
                            form.setLabel1("No Input");

                        }
                    }
                }
            }
        }
    }

	/// <summary> Contains functions for reading in values from the Arduino. </summary>
    public class ArduinoControl
    {
        //can run for max 2 minutes
        private static string port = "COM3";
        private SerialPort serial = new SerialPort(port);
		
		/// <summary>Opens the serial </summary>
        public ArduinoControl()
        {
            serial.Open();
        }
        
		/// <summary> Reads in a float from the serial that was sent by the Arduino. </summary>
		/// <returns> A float value for the output of the Arduino. </returns>
        public float readSerial(){
            float value;
            string input;
            input = serial.ReadLine();
            try
            {
                value = Convert.ToSingle(input);
            }
            catch (System.FormatException e)
            {
                value = 0;
                Console.WriteLine(e.Message);
                Console.WriteLine(input);
            }
            
            return value;
        }

        
    }

	///<summary> Handles all of the functions for dealing with values from the encoder. </summary>
    public class EncoderSensorClass
    {
        private static float offset, temp_value;
		/// <summary> The offset that will be subtracted from the kinect values. </summary>
        public static float kinect_offset = 0;
        private static int counter = 0;
        private static int list_counter = 0;
        private static bool first = true;
		/// <summary>The list of lists of doubles that will hold all the unfiltered values read in by the encoder. </summary>
        public static List<double> input_values = new List<double>();
		/// <summary> The list of doubles that will hold all of the filtered values read in by the encoder. </summary>
        public static List<double> input_filtered_values = new List<double>();
		/// <summary> An instance of the <c>ArduinoControl</c> class that represents the Arduino that is reading in values from the encoder. </summary>
        public static ArduinoControl arduino = new ArduinoControl();
		/// <summary> An instance of the <c>FileProcessing</c> class to write all the filtered encoder values to a text file. </summary>
        public static FileProcessing input;
		/// <summary> An instance of the <c>FileProcessing</c> class to write all the unfiltered encoder values to a text file. </summary> 
		public static FileProcessing input_no_filter;
        
		///<summary> Adds the parameter to the unfiltered list. </summary>
		/// <param name = "input"> The floating point value that will be added to the list. </param>
        public static void add(float input)
        {
            float value = input - offset;
            temp_value = value;
            kinect_offset = value;
            KinectSensorClass.form.setArduinoLabel(value + "");
            if(KinectSensorClass.form.write)
                input_values.Add(value);
        }
		
		/// <summary> Sets the offset at the current value that the encoder is at, causing it to zero. </summary>
        public static void zero()
        {
            offset = temp_value;
        }

    }

	/// <summary> Holds the x,y and z positions of a point. </summary>
    public class PointHolder
    {
		/// <summary> The x positon of the point. </summary>
        public float x = 0;
		/// <summary>The y position of the point. </summary>
		public float y = 0;
		/// <summary> The z position of the point. </summary>
		public float z = 0;

        /// <summary> Sets the x, y, and z values of the point. </summary>
		/// <param name = "valueX"> The x position of the point. </param>
		/// <param name = "valueY"> The y position of the point. </param>
		/// <param name = "valueZ"> The z position of the point. </param>
        public void SetValue(float valueX, float valueY, float valueZ)
        {
            x = valueX;
            y = valueY;
            z = valueZ;
        }
    }

	///<summary>Holds all the calculation functions.</summary>
    public class Calculate
    {
        /// <summary> Takes two points and generate a vector between them in 3 dimensions. </summary>
		/// <param name = "point1"> The first point in three dimensional space. </param>
		/// <param name = "point2"> The second point in three dimensional space. </param>
		/// <returns> The vector that was generated. </returns>
        public static PointHolder CreateVector(PointHolder point1, PointHolder point2)
        {
            PointHolder vector = new PointHolder();

            vector.x = point1.x - point2.x;
            vector.y = point1.y - point2.y;
            vector.z = point1.z - point2.z;

            return vector;
        }

        /// <summary> Calculates the length of the vector. </summary>
		/// <param name = "vector"> The vector that the magitudes will be taken from. </param>
		/// <returns> The length of the vector. </returns>
        public static double VectorLength(PointHolder vector)
        {
            double vector_length = 0;

            vector_length = Math.Sqrt(Math.Pow(vector.x, 2) + Math.Pow(vector.y, 2) + Math.Pow(vector.z, 2));

            return vector_length;
        }

        /// <summary> Calculates the dot product of two vectors. </summary>
		/// <param name = "vector1"> The first three dimensional vector. </param>
		/// <param name = "vector2"> The second three dimensional vector </param>
		/// <returns> The dot product of the two vectors. </returns>
        public static double DotProduct(PointHolder vector1, PointHolder vector2)
        {
            double dot_product = 0;

            dot_product = vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;

            return dot_product;
        }

        /// <summary> Converts a value from radians to degrees. </summary>
		/// <param name = "value"> The value in radians </param>
		/// <returns> The value in degrees </returns>
        public static double ToDegree(double value)
        {
            return value * (180.0 / Math.PI);
        }
        
        /// <summary>Calculates the velocity of angle movement, assuming that the frequency is 30Hz. </summary>
		/// <param name = "inital_angle"> The intial angle of the elbow. </param>
		/// <param name = "final_angle"> The final angle of the elbow. </param>
		/// <returns> The instantanous velcity of the elbow. </returns>
        public static double FindVelocity(double inital_angle, double final_angle)
        {
            double value = 0;
            double frequency = 30; //in Hz
            double period = 1 / frequency; //in seconds
            value = (final_angle - inital_angle) / period;
            return value;
        }
    }

	/// <summary> A butterworth filter for the values taken from the Kinect and encoder. </summary>
    public class Butterworth
    {
		/// <summary> Constant boolean for the constructor to set the list to values for filtering velcity. </summary>
        public const bool VELOCITY = true;
		/// <summary> Constant boolean for the constructor to set the list to values for filtering velcity. </summary>
        public const bool ANGLE = false;
        private List<double> A, B;
        private int order = 0;

		/// <summary> Constructor for the butterworth class </summary>
        public Butterworth(bool input)
        {
            const int coeffiecients = 3;
            A = new List<double>();
            B = new List<double>();
            for (int a = 0; a < 3; a++)
            {
                A.Add(0);
                B.Add(0);
            }

            if (input == VELOCITY)
            {
                B[0] = 0.000944691843840;
                B[1] = 0.001889383687680;
                B[2] = 0.000944691843840;
                A[0] = 1.000000000000000;
                A[1] = -1.911197067426073;
                A[2] = 0.914975834801434;
            }
            else if (input == ANGLE)
            {
                B[0] = 0.000039130205399;
                B[1] = 0.000078260410798;
                B[2] = 0.000039130205399;
                A[0] = 1.000000000000000;
                A[1] = -1.982228929792529;
                A[2] = 0.982385450614126;
            }
            order = coeffiecients - 1;
        }
		
		/// <summary> Filters the list of values. </summary>
		/// <param name = "x"> The list of values to be filtered. </param>
		/// <param name = "prev"> The list of previous values before <c>x</c> was being recorded. </param>
		/// <returns> A list of filtered values. </returns>
        public List<double> Filter(List<double> x, List<double> prev)
        {
            int sample_length = x.Count;
            List<double> y = new List<double>(sample_length + order);
            List<double> final_values = new List<double>(sample_length);
            for (int c = 0; c < sample_length; c++)
            {
                if (c < order)
                {
                    try
                    {
                        y[c] = prev[c];
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (c >= order)
                {
                    try
                    {
                        y[c] = 0;
                    }
                    catch(ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    for (int d = 0; d <= order; d++)
                    {
                        try
                        {
                            if (d == 0)
                                y[c] += B[d] * x[c];
                            else
                                y[c] += (B[d] * x[c - d]) + (-A[d] * y[c - d]);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            try
            {
                for (int i = 0; i < sample_length; i++)
                {
                    try
                    {
                        final_values[i] = y[i + order];
                    }
                    catch (ArgumentOutOfRangeException e) 
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
                return final_values;
        }
    }

	/// <summary> Controls all the file processing. </summary>
    public class FileProcessing
    {
        private String path;

        /// <summary>Constructor for the <c>FileProcessing</c> class. Sets the path for the <c>StreamWriter</c>. </summary>
		/// <param name = "name">The name of the file. </param>
		/// <param name = "type">The type of values being written to the file. </param>
        public FileProcessing(String name, String type)
        {
            path = @"C:\\Users\Kyle\Results\" + name + "-" + type + ".txt";
        }

        /// <summary> Writes all the values to a text file. </summary>
		/// <param name = "values"> The values that will be written to the file. </param>
        public void Write(List<List<double>> values)
        {
            using (StreamWriter io = new StreamWriter(path))
            {
                io.WriteLine(DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond);
                foreach (List<double> index in values)
                {
                    foreach (double a in index)
                    {
                        io.WriteLine(a);
                    }
                }
            }
        }

		/// <summary> Writes all the values to a text file. </summary>
		/// <param name = "values"> The values that will be written to the file. </param>
        public void Write(List<List<float>> values)
        {
            using (StreamWriter io = new StreamWriter(path))
            {
                io.WriteLine(DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond);
                foreach (List<float> index in values)
                {
                    foreach (float a in index)
                    {
                        io.WriteLine(a);
                    }
                }
            }
        }

		///<summary> Writes all the values to a text file. </summary>
		///<param name = "values"> The values that will be written to the file. </param>
        public void Write(List<double> values)
        {
            using(StreamWriter io = new StreamWriter(path)){
                io.WriteLine(DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond);
                foreach(double index in values){
                    io.WriteLine(index);
                }
            }
        }
    }
}

