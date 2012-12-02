import re

template = 'MouseDown="%s_MouseDown" '
nameRegex = re.compile("(rectangle\d\d)")
with open("WpfApplication3/MainWindow.xaml", "r") as mainWinFile:
  for line in mainWinFile:
    m = nameRegex.search(line)
    if m and "MouseDown" not in line:
#      print m.group(0)
      i = line.rindex('/')
#      print i
      print line[:i] + template % (m.group(0)) + line[i:]
    
