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
            //SampleSource kickSample = SampleSource.CreateFromWaveFile("Samples\\kick-trimmed.wav");
            //SampleSource snareSample = SampleSource.CreateFromWaveFile("Samples\\snare-trimmed.wav");
            //SampleSource closedHatsSample = SampleSource.CreateFromWaveFile("Samples\\closed-hat-trimmed.wav");
            //SampleSource openHatsSample = SampleSource.CreateFromWaveFile("Samples\\open-hat-trimmed.wav");
            sampleSources = new List<SampleSource[]>();
            SampleSource[] temp;
            foreach (string s in filenames)
            {
                temp = SampleSource.CreateFromWaveFile(sampleDir + s + ".wav");
                sampleSources.Add(temp);
            }
            temp = sampleSources.ElementAt(0);
            //sampleSources.Add(kickSample);
            //sampleSources.Add(snareSample);
            //sampleSources.Add(closedHatsSample);
            //sampleSources.Add(openHatsSample);
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
