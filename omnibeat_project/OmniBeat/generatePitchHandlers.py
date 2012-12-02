template = """        private void pitch_NewContact%d%d(object sender, NewContactEventArgs e)
        {
            PitchController.pitchSequence.Click(%d, %d, ref state);
            this.setPitch(MainWindow.chosenButton, %d, %d);
        }

"""
maxCol = 8
maxRow = 9
with open("PitchHandlers", "w") as f:
	for col in range(maxCol):
	  for row in range(maxRow):
		f.write(template % (col, row, col, row, col, row))
