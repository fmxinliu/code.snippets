@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ set CfgPath=%cd%\
@ set TestPath=%cd%\ǿ����Test\

@ rem ��GACж��
@ gacutil /u AssemblyDLL >nul

@ rem ��װ��GAC
@ gacutil /i %TestPath%GAC\AssemblyDLL.dll

pause
