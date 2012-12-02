using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace OmniBeat
{
    class DrumKit
    {
        private List<SampleSource[]> sampleSources;
        private WaveFormat waveFormat;
        private string sampleDir = "Samples\\";
        public static string[] filenames = {"kick-trimmed.wav", "snare-trimmed.wav", "closed-hat-trimmed.wav", "open-hat-trimmed.wav",
                                      "cymbal.wav", "everybody.wav", "oh-yeah.wav", "one-more-time.wav", "shots.wav", "youre-a-jerk.wav" };
        public DrumKit()
        {
            sampleSources = new List<SampleSource[]>();
            SampleSource[] temp;
            for (int i = 0; i < 2 ; i++ ){
                Console.WriteLine("Load #" + i);
                foreach (string s in filenames)
                {
                    Console.WriteLine(sampleDir + s);
                    temp = SampleSource.CreateFromWaveFile(sampleDir + s);
                    sampleSources.Add(temp);
                    Console.WriteLine(temp[4].SampleWaveFormat.SampleRate + " " + temp[4].SampleWaveFormat.Channels);
                }
            }   
            temp = sampleSources.ElementAt(0);

            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(temp[4].SampleWaveFormat.SampleRate, temp[4].SampleWaveFormat.Channels);



        }

        public virtual WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }

        public MusicSampleProvider GetSampleProvider(int note, int pitch)
        {
            return new MusicSampleProvider(this.sampleSources[note][pitch]);
        }
    }
}
