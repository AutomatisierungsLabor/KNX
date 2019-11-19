rd AppData /S /Q
rd ProgramData /S /Q

md AppData\KNX\ETS5
md ProgramData\KNX\ETS5


xcopy "%LOCALAPPDATA%\KNX\ETS5\*.*"  "AppData\KNX\ETS5\*.*" /E /Y
xcopy "c:\ProgramData\KNX\ETS5\*.*" "ProgramData\KNX\ETS5\*.*" /E /Y

del installer.guid /S