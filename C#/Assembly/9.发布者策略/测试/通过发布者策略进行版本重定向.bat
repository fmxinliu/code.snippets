@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ chcp 936 >nul
@ title �����߲�����ʾ
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\Test\
@ set AssemblyDLL_V1_Path=%TestPath%v1.1.1.1\
@ set AssemblyDLL_V2_Path=%TestPath%v2.2.2.2\

@ rem �����ļ��������¹���
@ set AssemblyOldVersion=1.1
@ set AssemblyName=TestAssembly
@ set PolicyFileName=policy.%AssemblyOldVersion%.%AssemblyName%
@ set PolicyFileVersion=1.0.0.0


@ cd ..
@ rd /s /q %TestPath% >nul

@ rem ���GAC���ڳ��򼯣���ж��
@ gacutil /u %AssemblyName% >nul
@ gacutil /u %PolicyFileName% >nul


@ rem ---
@ title ��װ�������ʼDLL�汾�ţ�v1.1
@ rem ---


@ rem ����V1�汾
@ mkdir %AssemblyDLL_V1_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V1_Path%%AssemblyName%.dll %AssemblyName%.v1.1.1.1.cs >nul

@ rem ����������
@ csc /t:exe /out:%TestPath%main.exe /r:%AssemblyDLL_V1_Path%%AssemblyName%.dll Program.cs

@ rem ��װV1�汾��GAC
@ gacutil /i %AssemblyDLL_V1_Path%%AssemblyName%.dll

@ rem ����
@ %TestPath%main.exe


@ rem ---
@ title ��װ��DLL���汾�ţ�v2.2����ͨ�������߲��ԣ��汾v1.0.0.0����������DLL��������v1.1��
@ rem ---


@ rem ����V2�汾
@ mkdir %AssemblyDLL_V2_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V2_Path%%AssemblyName%.dll %AssemblyName%.v2.2.2.2.cs >nul

@ rem ��װV2�汾��GAC
@ gacutil /i %AssemblyDLL_V2_Path%%AssemblyName%.dll


@ rem ��������߲����ļ�
@ xcopy /y %CfgPath%%AssemblyName%.config %TestPath% >nul
@ al /keyfile:%CfgPath%MyCompany.snk /version:%PolicyFileVersion% /out:%TestPath%%PolicyFileName%.dll /link:%CfgPath%%AssemblyName%.config

@ rem ��װ�����߲��Ե�GAC
@ gacutil /i %TestPath%%PolicyFileName%.dll

@ rem ����
@ %TestPath%main.exe


pause


@ rem ---
@ title ж�س��򼯣���������
@ rem ---

@ gacutil /u %AssemblyName% >nul
@ gacutil /u %PolicyFileName% >nul
