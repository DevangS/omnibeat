using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Multitouch.Framework.WPF.Input;
using InteractiveSpace.SDK;
using InteractiveSpace.SDK.DLL;
namespace OmniBeat
{
    /// <summary>
    /// Interaction logic for PitchController.xaml
    /// </summary>
    public partial class PitchController : UserControl
    {
        public const int maxRow = 9;
        public const int maxCol = 8;
        private static PitchSequence pitchSequence;
        public DrumPatternSampleProvider dsp;
        public Boolean[,,] state;

        public PitchController()
        {
            InitializeComponent();
            Rectangle[,] pitchGrid = new Rectangle[maxCol, maxRow] { 
                {rectangle00, rectangle01, rectangle02, rectangle03, rectangle04, rectangle05, rectangle06, rectangle07, rectangle08},
                {rectangle10, rectangle11, rectangle12, rectangle13, rectangle14, rectangle15, rectangle16, rectangle17, rectangle18},
                {rectangle20, rectangle21, rectangle22, rectangle23, rectangle24, rectangle25, rectangle26, rectangle27, rectangle28},
                {rectangle30, rectangle31, rectangle32, rectangle33, rectangle34, rectangle35, rectangle36, rectangle37, rectangle38},
                {rectangle40, rectangle41, rectangle42, rectangle43, rectangle44, rectangle45, rectangle46, rectangle47, rectangle48},
                {rectangle50, rectangle51, rectangle52, rectangle53, rectangle54, rectangle55, rectangle56, rectangle57, rectangle58},
                {rectangle60, rectangle61, rectangle62, rectangle63, rectangle64, rectangle65, rectangle66, rectangle67, rectangle68},
                {rectangle70, rectangle71, rectangle72, rectangle73, rectangle74, rectangle75, rectangle76, rectangle77, rectangle78} };
            PitchController.pitchSequence = new PitchSequence(pitchGrid);
            this.state = new Boolean[BeatMaker.noteNum, maxCol, maxRow];
            this.resetState();

        }
        public void resetState()
        {
            for (int note = 0; note < BeatMaker.noteNum; note++)
            {
                for (int col = 0; col < maxCol; col++)
                {
                    for (int row = 0; row < maxRow; row++)
                    {
                        if (row == 4)
                        {
                            state[note, col, row] = true;
                        }
                    }
                }
            }
        }


        public void setPatternSequencer(DrumPatternSampleProvider pSequencer)
        {
            this.dsp = pSequencer;
        }

        public void setPitch(int note, int value, int step)
        {
            if( this.dsp != null)
                this.dsp.setPitch(note, value, step);
        }

        internal void reloadState()
        {

            for (int col = 0; col < maxCol; col++)
            {
                for (int row = 0; row < maxRow; row++)
                {
                    if (state[BeatMaker.chosenButton, col, row] == true)
                    {
                        PitchController.pitchSequence.Click(col, row, ref state);
                        setPitch(BeatMaker.chosenButton, col, row);
                        break;
                    }
                }
            }
        }

        private void pitch_NewContact00(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 0);
        }

        private void pitch_NewContact01(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 1);
        }

        private void pitch_NewContact02(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 2);
        }

        private void pitch_NewContact03(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 3);
        }

        private void pitch_NewContact04(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 4);
        }

        private void pitch_NewContact05(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 5);
        }

        private void pitch_NewContact06(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 6);
        }

        private void pitch_NewContact07(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 7);
        }

        private void pitch_NewContact08(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(0, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 0, 8);
        }

        private void pitch_NewContact10(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 0);
        }

        private void pitch_NewContact11(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 1);
        }

        private void pitch_NewContact12(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 2);
        }

        private void pitch_NewContact13(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 3);
        }

        private void pitch_NewContact14(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 4);
        }

        private void pitch_NewContact15(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 5);
        }

        private void pitch_NewContact16(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 6);
        }

        private void pitch_NewContact17(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 7);
        }

        private void pitch_NewContact18(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(1, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 1, 8);
        }

        private void pitch_NewContact20(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 0);
        }

        private void pitch_NewContact21(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 1);
        }

        private void pitch_NewContact22(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 2);
        }

        private void pitch_NewContact23(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 3);
        }

        private void pitch_NewContact24(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 4);
        }

        private void pitch_NewContact25(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 5);
        }

        private void pitch_NewContact26(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 6);
        }

        private void pitch_NewContact27(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 7);
        }

        private void pitch_NewContact28(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(2, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 2, 8);
        }

        private void pitch_NewContact30(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 0);
        }

        private void pitch_NewContact31(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 1);
        }

        private void pitch_NewContact32(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 2);
        }

        private void pitch_NewContact33(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 3);
        }

        private void pitch_NewContact34(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 4);
        }

        private void pitch_NewContact35(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 5);
        }

        private void pitch_NewContact36(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 6);
        }

        private void pitch_NewContact37(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 7);
        }

        private void pitch_NewContact38(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(3, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 3, 8);
        }

        private void pitch_NewContact40(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 0);
        }

        private void pitch_NewContact41(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 1);
        }

        private void pitch_NewContact42(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 2);
        }

        private void pitch_NewContact43(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 3);
        }

        private void pitch_NewContact44(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 4);
        }

        private void pitch_NewContact45(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 5);
        }

        private void pitch_NewContact46(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 6);
        }

        private void pitch_NewContact47(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 7);
        }

        private void pitch_NewContact48(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(4, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 4, 8);
        }

        private void pitch_NewContact50(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 0);
        }

        private void pitch_NewContact51(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 1);
        }

        private void pitch_NewContact52(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 2);
        }

        private void pitch_NewContact53(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 3);
        }

        private void pitch_NewContact54(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 4);
        }

        private void pitch_NewContact55(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 5);
        }

        private void pitch_NewContact56(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 6);
        }

        private void pitch_NewContact57(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 7);
        }

        private void pitch_NewContact58(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(5, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 5, 8);
        }

        private void pitch_NewContact60(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 0);
        }

        private void pitch_NewContact61(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 1);
        }

        private void pitch_NewContact62(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 2);
        }

        private void pitch_NewContact63(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 3);
        }

        private void pitch_NewContact64(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 4);
        }

        private void pitch_NewContact65(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 5);
        }

        private void pitch_NewContact66(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 6);
        }

        private void pitch_NewContact67(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 7);
        }

        private void pitch_NewContact68(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(6, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 6, 8);
        }

        private void pitch_NewContact70(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 0, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 0);
        }

        private void pitch_NewContact71(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 1, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 1);
        }

        private void pitch_NewContact72(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 2, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 2);
        }

        private void pitch_NewContact73(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 3, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 3);
        }

        private void pitch_NewContact74(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 4, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 4);
        }

        private void pitch_NewContact75(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 5, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 5);
        }

        private void pitch_NewContact76(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 6, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 6);
        }

        private void pitch_NewContact77(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 7, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 7);
        }

        private void pitch_NewContact78(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(7, 8, ref state);
            this.setPitch(BeatMaker.chosenButton, 7, 8);
        }


    }
}
