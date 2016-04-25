using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinectExpirement
{
    public class Upsampler
    {
        public Upsampler()
        {

        }

        public List<double> DownSample(ref List<double> samples, int desired_frequency, int duration)
        {
            double temp = (desired_frequency * duration) / 1000;
            int desired_samples = Convert.ToInt32(Math.Round(temp));
            List<double> samples_ds = new List<double>(desired_samples);
            int actual_samples = samples.Count;
            double offset = (double)actual_samples / (desired_samples - 1);
            List<double> keepers = new List<double>(desired_samples);
            int position = 0;

            samples_ds.Add(samples[0]);
            keepers.Add(1);
            for (int i = 1; i < desired_samples; i++)
            {
                keepers.Add(Math.Round(keepers[i - 1] + offset));
                position = Convert.ToInt32(keepers[i]);
                samples_ds.Add(samples[position]);
            }
            return samples_ds;
        }

        public List<double> UpSample(ref List<double> samples, int desired_frequency, int duration)
        {
            double temp = (desired_frequency * duration) / 1000;
            int desired_samples = Convert.ToInt32(Math.Round(temp));
            List<double> samples_us = new List<double>(desired_samples);
            int actual_samples = samples.Count;
            double offset = (double)desired_samples / actual_samples;
            List<double> positions = new List<double>(actual_samples + 1);

            try
            {
                positions.Add(1);
                for (int i = 1; i < actual_samples; i++)
                {
                    positions.Add(positions[i - 1] + offset);
                }
                positions.Add(actual_samples);

                int index = 1;
                samples_us.Add(samples[0]);
                for (int i = 1; i < actual_samples; i++)
                {
                    int next_position = Convert.ToInt32(Math.Round(positions[i]));
                    for (int j = 0; j < next_position - index; j++)
                    {
                        samples_us.Add(samples[i - 1]);
                        index++;
                    }
                }
                samples_us.Add(samples[actual_samples - 1]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return samples_us;
        }
    }
}
