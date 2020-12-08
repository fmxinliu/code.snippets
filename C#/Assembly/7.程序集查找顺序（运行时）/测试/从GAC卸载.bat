@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\以管理员身份运行.bat %~0
@ exit
)

@ title 从GAC卸载强命名程序集
@ set CfgPath=%cd%\
@ set TestPath=%cd%\强命名Test\

@ rem 从GAC卸载
@ gacutil /u AssemblyDLL

pause
