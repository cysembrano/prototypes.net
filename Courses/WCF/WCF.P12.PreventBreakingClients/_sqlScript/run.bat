set server=DESKTOP-QOS4LQB\SQLEXPRESS
set database=COURSE_YT_WCF

rem set uid=FlowSQL
rem set password=Flowsq1!
rem set inputfile=V1.sql
rem sqlcmd -S %server% -d %database% -U %uid% -P %password% -i %inputfile%

set inputfile=DBScript.sql
sqlcmd -S %server% -d %database% -i %inputfile%

pause