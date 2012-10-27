using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NAudio.Wave;
using NAudio.CoreAudioApi;

    class Key
    {
        public bool isPressed = false;
        public bool isPlaying = false;
        public string sampleFile;

        public WaveChannel32 channelStream;
        public WaveOffsetStream offsetStream;


        public Key(string sampleFile)
        {
            this.sampleFile = sampleFile;
            WaveFileReader reader = new WaveFileReader(sampleFile);
            offsetStream = new WaveOffsetStream(reader);
            channelStream = new WaveChannel32(offsetStream);
            channelStream.Position = channelStream.Length;
        }
        

        public void setPlayState(bool state)
        {
            if (state && !isPlaying)
            {
                channelStream.Position = 0;
            }
            this.isPlaying = state;
            this.isPressed = state;
        }


    }

