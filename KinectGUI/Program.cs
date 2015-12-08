using System;
using System.IO;
using System.Collections.Generic;
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
        public static IList<Body> _bodies;
        public static bool first = true;
        public static Form1 form;
        public static String name;
        public static WriteableBitmap colorBitmap;
        public static byte[] colorPixels;


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        public static void runSensor()
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


            PointHolder wrist = new PointHolder("Wrist");
            PointHolder elbow = new PointHolder("Elbow");
            PointHolder shoulder = new PointHolder("Shoulder");
            TimeClass time = new TimeClass();
            //FileProcessing input = new FileProcessing(name);
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
                    {
                        if (body.IsTracked)
                        {
                            Joint shoulderJoint = body.Joints[JointType.ShoulderRight];
                            Joint elbowJoint = body.Joints[JointType.ElbowRight];
                            Joint wristJoint = body.Joints[JointType.WristRight];
                            double angle2;
                            form.setLabel1("Now tracking body");

                            wrist.x = wristJoint.Position.X;
                            wrist.y = wristJoint.Position.Y;
                            wrist.z = wristJoint.Position.Z;
                            elbow.x = elbowJoint.Position.X;
                            elbow.y = elbowJoint.Position.Y;
                            elbow.z = elbowJoint.Position.Z;
                            shoulder.x = shoulderJoint.Position.X;
                            shoulder.y = shoulderJoint.Position.Y;
                            shoulder.z = shoulderJoint.Position.Z;

                            //Calculate angle with Trig method
                            /*forearmLength = Calculate.length(wrist, elbow);
                            bicepLength = Calculate.length(elbow, shoulder);
                            hypoteneuseLength = Calculate.length(wrist, shoulder);
                            angle = Calculate.angle(forearmLength, bicepLength, hypoteneuseLength);*/


                            //Calculate angle with vector method
                            PointHolder vector1 = Calculate.CreateVector(wrist, elbow);
                            PointHolder vector2 = Calculate.CreateVector(shoulder, elbow);
                            double dot_product = Calculate.DotProduct(vector1, vector2);
                            double length_vector1 = Calculate.VectorLength(vector1);
                            double length_vector2 = Calculate.VectorLength(vector2);
                            angle2 = Calculate.ToDegree(Math.Acos(dot_product / (length_vector1 * length_vector2)));

                            form.addChart2(angle2);
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
        public String name;
        public PointHolder(String nameInput)
        {
            name = nameInput;
        }

        public void setValue(float valueX, float valueY, float valueZ)
        {
            x = valueX;
            y = valueY;
            z = valueZ;
        }
    }

    public class Calculate
    {
        public static double length(PointHolder point1, PointHolder point2)
        {
            double lengthValue;
            double sqX = Math.Pow(point2.x - point1.x, 2);
            double sqY = Math.Pow(point2.y - point1.y, 2);
            double sqZ = Math.Pow(point2.z - point1.z, 2);
            lengthValue = Math.Sqrt(sqX + sqY + sqZ);
            return lengthValue;
        }
        /*a -> length of forearm
         *b -> length of bicep
         *c -> length of hypoteneuse*/
        public static double angle(double forearmLength, double bicepLength, double hypoteneuseLength)
        {
            double finalAngle, intermediary, intermediary2;
            double a = forearmLength, b = bicepLength, c = hypoteneuseLength;
            double sqA, sqB, sqC;
            sqA = Math.Pow(a, 2);
            sqB = Math.Pow(b, 2);
            sqC = Math.Pow(c, 2);
            intermediary = sqA + sqB - sqC;
            intermediary2 = intermediary / 2 * a * b;
            finalAngle = ToDegree (Math.Acos(intermediary));
            return finalAngle;
        }

        //The CreateVector method will take two points and generate the vector between them in 3 dimensions
        public static PointHolder CreateVector (PointHolder point1, PointHolder point2)
        {
            PointHolder vector = new PointHolder("Vector");

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

    public class TimeClass//does not work properly
    {
        public int initial;
        
        public void getInitial()
        {
            initial = getValue();
        }

        public int getValue()
        {
            int hour, minute, second;
            int hValue, mValue;
            int value;
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            second = DateTime.Now.Second;
            hValue = hour * 60 * 60;
            mValue = minute * 60;
            value = hValue + mValue + second;
            return value;
        }
        
        public int getTime()
        {
            int finalTime;
            int timeNow = getValue();
            finalTime = timeNow - this.initial;
            return finalTime;
        }
    }

    public class FileProcessing
    {
        public String name;
        public StreamWriter fileIO;
        public FileProcessing(String name)
        {
            this.name = name + ".txt";
            fileIO = new StreamWriter(this.name);
        }
        public void write(double input)
        {
            fileIO.WriteLine(input);
        }
        public void writeTime()
        {
            fileIO.WriteLine(DateTime.Now.Hour + " " + 
                DateTime.Now.Minute + " " + DateTime.Now.Second + " " + DateTime.Now.Millisecond);
        }
    }
}

