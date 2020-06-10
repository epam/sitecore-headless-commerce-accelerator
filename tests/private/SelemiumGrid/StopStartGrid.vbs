Set WshShell = CreateObject("WScript.Shell")
WshShell.Run chr(34) & "kill_grid.bat" & Chr(34), 0
Set WshShell = Nothing

WScript.Sleep 2000

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run chr(34) & "start_hub.bat" & Chr(34), 0
Set WshShell = Nothing

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run chr(34) & "start_node1.bat" & Chr(34), 0
Set WshShell = Nothing

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run chr(34) & "start_node2.bat" & Chr(34), 0
Set WshShell = Nothing