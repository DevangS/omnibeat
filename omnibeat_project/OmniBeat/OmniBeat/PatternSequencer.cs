﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OmniBeat
{
    class PatternSequencer
    {
        private readonly DrumPattern drumPattern;
        private readonly DrumKit drumKit;
        private int tempo;
        private int[,] pitch;
        private int samplesPerStep;
        public static bool playClip = false;
        private bool clipPlayed = false;

        public PatternSequencer(DrumPattern drumPattern, DrumKit kit)
        {
            this.drumKit = kit;
            this.drumPattern = drumPattern;
            this.Tempo = 120;
            this.pitch = new int[BeatMaker.noteNum, drumPattern.Steps];
            for (int i = 0; i < BeatMaker.noteNum; i++)
            {
                for (int j = 0; j < drumPattern.Steps; j++)
                {
                    this.pitch[i, j] = 4;
                }
            }
        }

        public int Tempo
        {
            get
            {
                return this.tempo;
            }
            set
            {
                if (this.tempo != value)
                {
                    this.tempo = value;
                    this.newTempo = true;
                }
            }
        }

        public void setPitch(int note, int step, int value)
        {
            pitch[note, step] = value;
        }

        private bool newTempo;
        private int currentStep = 0;
        private double patternPosition = 0;

        public IList<MusicSampleProvider> GetNextMixerInputs(int sampleCount)
        {
            if (clipPlayed == true && currentStep == 7)
            {
                BeatMaker.clearClips();
                clipPlayed = false;
            }
            if (playClip == true && currentStep == 0)
            {
                playClip = false;
                clipPlayed = true;
            }

            List<MusicSampleProvider> mixerInputs = new List<MusicSampleProvider>();
            int samplePos = 0;
            if (newTempo)
            {
                int samplesPerBeat = (this.drumKit.WaveFormat.Channels * this.drumKit.WaveFormat.SampleRate * 60) / tempo;
                this.samplesPerStep = samplesPerBeat / 4;
                //patternPosition = 0;
                newTempo = false;
            }

            while (samplePos < sampleCount)
            {
                double offsetFromCurrent = (currentStep - patternPosition);
                if (offsetFromCurrent < 0) offsetFromCurrent += drumPattern.Steps;
                int delayForThisStep = (int)(this.samplesPerStep * offsetFromCurrent);
                if (delayForThisStep >= sampleCount)
                {
                    // don't queue up any samples beyond the requested time range
                    break;
                }

                for (int note = 0; note < drumPattern.Notes; note++)
                {
                    if (drumPattern[note, currentStep] != 0)
                    {
                        var sampleProvider = drumKit.GetSampleProvider(note, pitch[note, currentStep]);
                        sampleProvider.DelayBy = delayForThisStep;
                        Debug.WriteLine("beat at step {0}, patternPostion={1}, delayBy {2}", currentStep, patternPosition, delayForThisStep);
                        mixerInputs.Add(sampleProvider);
                    }
                }

                samplePos += samplesPerStep;
                currentStep++;
                currentStep = currentStep % drumPattern.Steps;

            }
            this.patternPosition += ((double)sampleCount / samplesPerStep);
            if (this.patternPosition > drumPattern.Steps)
            {
                this.patternPosition -= drumPattern.Steps;
            }

            return mixerInputs;
        }
    }
}
