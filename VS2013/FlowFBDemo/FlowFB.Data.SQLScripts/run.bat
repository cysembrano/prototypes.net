set server=.\SQLEXPR_X64_2014
set database=FlowFBAccounting
set uid=FlowFBAccountingSA
set password=F1ow199!

set inputfile=1.0.sql

sqlcmd -S %server% -d %database% -U %uid% -P %password% -i %inputfile%

set inputfile=1.1.sql

sqlcmd -S %server% -d %database% -U %uid% -P %password% -i %inputfile%

pause