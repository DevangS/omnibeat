using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace OmniBeat
{
    class SampleSource
    {
         public static SampleSource[] CreateFromWaveFile(string fileName)
        {
            using (var reader = new WaveFileReader(fileName))
            {
                ISampleProvider sp;
                int sourceSamples;
                if (reader.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
                {
                    if (reader.WaveFormat.BitsPerSample == 16)
                    {
                        sp = new Pcm16BitToSampleProvider(reader);
                        sourceSamples = (int)(reader.Length / 2);
                    }
                    else if (reader.WaveFormat.BitsPerSample == 24)
                    {
                        sp = new Pcm24BitToSampleProvider(reader);
                        sourceSamples = (int)(reader.Length / 3);
                    }
                    else
                    {
                        throw new ArgumentException("Currently only 16 or 24 bit PCM samples are supported");
                    }
                }
                else if (reader.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
                {
                    sp = new WaveToSampleProvider(reader);
                    sourceSamples = (int)(reader.Length / 4);
                }
                else
                {
                    throw new ArgumentException("Must be PCM or IEEE float");
                }
                float[] sampleData = new float[sourceSamples];
                int n = sp.Read(sampleData, 0, sourceSamples);
                if (n != sourceSamples)
                {
                    throw new InvalidOperationException(String.Format("Couldn't read the whole sample, expected {0} samples, got {1}", n, sourceSamples));
                }

                SampleSource[] result = new SampleSource[9];
                // Normal pitch
                int normal = 4;
                result[normal] = new SampleSource(sampleData, sp.WaveFormat);
                int pitch = 5;

                for (int currentPitch = 0; currentPitch < normal; currentPitch++, pitch--)
                {
                    float[] changedPitchData = new float[sampleData.Length/pitch];
                    for (int j = 0, i = 0; i  < sampleData.Length && j < changedPitchData.Length; i++)
                    {
                        if (i % pitch == 0 && j != 0)
                        {
                            changedPitchData[j] = sampleData[i];
                            j++;
                        }
                    }

                    result[currentPitch] = new SampleSource(changedPitchData, sp.WaveFormat);
                }

                pitch = 2;

                for (int currentPitch = normal+1; currentPitch < result.Length; currentPitch++, pitch++)
                {
                    float[] changedPitchData = new float[sampleData.Length * pitch];
                    for (int j = 0, i = 0; i < sampleData.Length && j < changedPitchData.Length; j++)
                    {
                        changedPitchData[j] = sampleData[i];
                                        
                        if (j % pitch == 0 && j != 0)
                        {
                            i++;
                        }
                    }

                    result[currentPitch] = new SampleSource(changedPitchData, sp.WaveFormat);
                    pitch--;
                }
                return result;
            }
        }

        public SampleSource(float[] sampleData, WaveFormat waveFormat) :
            this(sampleData, waveFormat, 0, sampleData.Length)
        {
        }

        public SampleSource(float[] sampleData, WaveFormat waveFormat, int startIndex, int length)
        {
            this.SampleData = sampleData;
            this.SampleWaveFormat = waveFormat;
            this.StartIndex = startIndex;
            this.Length = length;
        }

        /// <summary>
        /// Sample data
        /// </summary>
        public float[] SampleData { get; private set; }
        /// <summary>
        /// Format of sampleData
        /// </summary>
        public WaveFormat SampleWaveFormat { get; private set; }
        /// <summary>
        /// Index of the first sample to play
        /// </summary>
        public int StartIndex { get; private set; }
        /// <summary>
        /// Number of valid samples
        /// </summary>
        public int Length { get; private set; }
    }
}
