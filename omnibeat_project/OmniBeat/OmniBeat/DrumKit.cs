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
        private List<SampleSource> sampleSources;
        private WaveFormat waveFormat;
        private string sampleDir = "Samples\\";
        private string[] filenames = {"kick-trimmed", "snare-trimmed", "closed-hat-trimmed", "open-hat-trimmed",
                                      "beats", "cymbal-and-drum", "cymbal", "dubwub", "electrohouse", "everybody",
                                      "gangsta", "guitar", "help", "hip-hop-non-stop", "i-love-college", 
                                      "never-gonna-give-you-up", "oh-yeah", "one-more-time", "scratch", "shots",
                                      "swagga", "swangies", "youre-a-jerk" };


        public DrumKit()
        {
            //SampleSource kickSample = SampleSource.CreateFromWaveFile("Samples\\kick-trimmed.wav");
            //SampleSource snareSample = SampleSource.CreateFromWaveFile("Samples\\snare-trimmed.wav");
            //SampleSource closedHatsSample = SampleSource.CreateFromWaveFile("Samples\\closed-hat-trimmed.wav");
            //SampleSource openHatsSample = SampleSource.CreateFromWaveFile("Samples\\open-hat-trimmed.wav");
            sampleSources = new List<SampleSource>();
            SampleSource temp;
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
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(temp.SampleWaveFormat.SampleRate, temp.SampleWaveFormat.Channels);
        }

        public virtual WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }

        public MusicSampleProvider GetSampleProvider(int note)
        {
            return new MusicSampleProvider(this.sampleSources[note]);
        }
    }
}
