using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Fusion;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace KinectGUI
{

    class KinectSensorClass
    {

        public static KinectSensor sensor;
        public static IList<Body> bodies;
        public static bool first = true;
        public static List<List<double>> lists = new List<List<double>>();
        public static int addCounter = 0, listCounter = 0;
        public static Form1 form;
        public static String name;
        public static FileProcessing input;


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        //The RunSensor method will open and run the methods for using the Kinect Sensor
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
        
        static void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();


            PointHolder wrist = new PointHolder();
            PointHolder elbow = new PointHolder();
            PointHolder shoulder = new PointHolder();
            input = new FileProcessing(name);
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
                                form.startTimer();
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
                            
                            //If the List is at capacity the values are added to the next List
                            if (addCounter != 128)
                            {
                                lists[listCounter].Add(angle);
                                addCounter++;
                            }
                            else
                            {
                                addCounter = 0;
                                listCounter++;
                                lists.Add(new List<double>());
                                lists[0].Add(angle);
                            }

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

    public class PointHolder
    {
        public float x = 0, y = 0, z = 0;
        
        //The SetValue method will set the x,y, and z values of the point 
        public void SetValue(float valueX, float valueY, float valueZ)
        {
            x = valueX;
            y = valueY;
            z = valueZ;
        }
    }

    public class Calculate
    {

        //The CreateVector method will take two points and generate the vector between them in 3 dimensions
        public static PointHolder CreateVector (PointHolder point1, PointHolder point2)
        {
            PointHolder vector = new PointHolder();

            vector.x = point1.x - point2.x;
            vector.y = point1.y - point2.y;
            vector.z = point1.z - point2.z;

            return vector;
        }

        //The VectorLength method will calculate the length of a vector
        public static double VectorLength(PointHolder vector)
        {
            double vector_length = 0;

            vector_length = Math.Sqrt(Math.Pow(vector.x, 2) + Math.Pow(vector.y, 2) + Math.Pow(vector.z, 2));

            return vector_length;
        }

        //The DotProduct method will calculate the dot product of two three dimensional vectors
        public static double DotProduct(PointHolder vector1, PointHolder vector2)
        {
            double dot_product = 0;

            dot_product = vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;

            return dot_product;
        }

        public static double ToDegree(double value)
        {
            return value * (180.0 / Math.PI);
        }

    }

    public class FileProcessing
    {
        private String path;
        
        //The constructor sets the path for the StreamWriter
        public FileProcessing(String name)
        {
            path = @"C:\\Users\Kyle\Results\" + name + ".txt";
        }
        
        //The Write method will write all the values in the List to the file
        public void Write(List<List<double>> values)
        {
            using(StreamWriter io = new StreamWriter(path)){
                io.WriteLine(DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " " + DateTime.Now.Millisecond);
                foreach (List<double> index in values)
                {
                    foreach(double a in index){
                        io.WriteLine(a);
                    }
                }
            }
        }
    }
}

