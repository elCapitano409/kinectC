using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace KinectGUI
{
    
    class KinectSensorClass
    {

        public static KinectSensor sensor;
        public static IList<Body> _bodies;
        public static bool first = true;
        public static Form1 form;
        
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

            
            PointHolder wrist = new PointHolder ("Wrist");
            PointHolder elbow = new PointHolder ("Elbow");
            PointHolder shoulder = new PointHolder ("Shoulder");
            TimeClass time = new TimeClass();

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
                            double forearmLength, bicepLength, hypoteneuseLength;
                            double forearmLength2, bicepLength2;
                            double angle, angle2;
                            form.setLabel1("Now tracking body");
                            /*time stamp shinanegans
                            if (first)
                            {
                                time.getInitial();
                                first = false;
                            }*/

                            wrist.x = wristJoint.Position.X;
                            wrist.y = wristJoint.Position.Y;
                            wrist.z = wristJoint.Position.Z;
                            elbow.x = elbowJoint.Position.X;
                            elbow.y = elbowJoint.Position.Y;
                            elbow.z = elbowJoint.Position.Z;
                            shoulder.x = shoulderJoint.Position.X;
                            shoulder.y = shoulderJoint.Position.Y;
                            shoulder.z = shoulderJoint.Position.Z;

                            forearmLength = Calculate.length(wrist, elbow);
                            bicepLength = Calculate.length(elbow, shoulder);
                            hypoteneuseLength = Calculate.length(wrist, shoulder);
                            angle = Calculate.angle(forearmLength, bicepLength, hypoteneuseLength);

                            /*
                            forearmLength2 = Calculate.length2(wrist, elbow);
                            bicepLength2 = Calculate.length2(elbow, shoulder);
                            angle2 = Calculate.angle2(forearmLength2, bicepLength2

                            form.addChart2(angle2);*/
                            form.addChart1(angle);

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

    public class Display
    {
        public static void coordinates(int time, PointHolder point)
        {
            Console.WriteLine("Joint: " + point.name);
            Console.WriteLine("Time: " + time);
            Console.WriteLine("X is: " + point.x);
            Console.WriteLine("Y is: " + point.y);
            Console.WriteLine("Z is: " + point.z);
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
        /*
        public static double length2(PointHolder point1, PointHolder point)
        {
            double lengthValue;
            return lengthValue;
        }

        public static double angle2(double lengthA, double lengthB)
        {
            double angle;
            return angle;
        }*/

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
}

