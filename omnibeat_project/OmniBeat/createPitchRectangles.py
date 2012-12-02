template = '<Rectangle Opacity="100" Fill="Red" HorizontalAlignment="Left" '
template += 'Margin="%d,%d,0,0" Name="rectangle%d%d" VerticalAlignment="Top" '
template += 'Width="%d" Height="%d" '
template += 'mt:MultitouchScreen.NewContact="pitch_NewContact%d%d"/>'
tab='\s\s\s\s'

colMax = 8
rowMax = 9
width = 140
height = 50 
padW = 10
padH = 4

leftStart = 0
topStart = 0
left = leftStart
top = topStart

for col in range(colMax):
  top = topStart
  for row in range(rowMax):
    print template % (left, top, col, row, width, height, col, row)
    top += height + padH
  left += width + padW
