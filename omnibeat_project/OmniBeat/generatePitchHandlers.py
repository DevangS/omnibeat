template = """        private void pitch_NewContact%d%d(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(%d, %d, state);
            this.setPitch(MainWindow.chosenButton, %d, %d);
        }

"""
maxCol = 8
maxRow = 9
for col in range(maxCol):
  for row in range(maxRow):
    print template % (col, row, col, row, col, row)
