set server=DESKTOP-QOS4LQB\SQLEXPRESS
set database=COURSE_YT_WCF
set uid=FlowSQL
set password=Flowsq1!

rem set inputfile=V1.sql
rem sqlcmd -S %server% -d %database% -U %uid% -P %password% -i %inputfile%

set inputfile=DBScript.sql
sqlcmd -S %server% -d %database% -i %inputfile%

pause