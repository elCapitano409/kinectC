using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

namespace kinectExpirement
{
    static class Program
    {
        private static KinectForm form;
        private static ArduinoControl arduino;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new KinectForm();
            Thread arduinoThread = new Thread(ReadEncoder);
            arduinoThread.Start();
                        
            Application.Run(form);
            
        }

        public static void ReadEncoder()
        {
            arduino = form.encoder.arduino;
            float value = 0;
            string input = "";
            try
            {
                while (arduino.loop_thread)
                {
                    if (arduino.read_data)
                    {
                        try
                        {
                            Console.WriteLine("Reading Data");
                            arduino.serial.BaudRate = 19200;
                            arduino.serial.Open();
                        }
                        catch(Exception e )
                        {
                            Console.WriteLine(e.Message);
                        }
                        while (arduino.read_data)
                        {
                            if (arduino.serial.BytesToRead > 0)
                            {
                                input = arduino.serial.ReadLine();
                                try
                                {
                                    value = Convert.ToSingle(input);
                                    string temp = value.ToString();
                                    Console.WriteLine(temp);
                                    //form.lblArduinoStatus.Text = temp;
                                    //form.setArduinoLabel(input);
                                }
                                catch (System.FormatException e)
                                {
                                    value = 0;
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(input);
                                }
                                if (arduino.save_data)
                                {
                                    Console.WriteLine("Saving Data");
                                    form.encoder.input_values.Add(value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

