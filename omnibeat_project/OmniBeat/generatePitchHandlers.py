template = """        private void rectangle%d%d_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ConfigClass.pitchGrid.grid[%d,%d].Click();
        }

"""
maxCol = 8
maxRow = 9
with open("PitchHanders", "w") as pitchFile:
  for col in range(maxCol):
    for row in range(maxRow):
      pitchFile.write(template % (col, row, col, row))
