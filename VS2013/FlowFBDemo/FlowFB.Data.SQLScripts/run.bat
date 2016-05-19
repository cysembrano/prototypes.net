set inputfile=1.0.sql
set server=.\SQLEXPR_X64_2014
set database=FlowFBAccounting
set uid=FlowFBAccountingSA
set password=F1ow199!

sqlcmd -S %server% -d %database% -U %uid% -P %password% -i %inputfile%

pause