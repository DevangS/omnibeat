import re

template = 'MouseDown="%s_MouseDown" '
nameRegex = re.compile("(rectangle\d\d)")
with open("WpfApplication3/MainWindow.xaml", "r") as mainWinFile:
  for line in mainWinFile:
    m = nameRegex.search(line)
    if m and "MouseDown" in line:
#      print m.group(0)
      i = line.rindex('/')
      j = line.rindex('M')
      k = line[:j].rindex('M')
#      print i
      print line[:k] + line[i:]
    
