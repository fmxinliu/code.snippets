@ chcp 936 >nul
@ title ����ǿ��������
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\ǿ����Test\

@ cd ..

@ rd /s /q %TestPath%

@ rem �����Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%AssemblyDLL.dll AssemblyDLL.cs

@ rem ���� CodeBase Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%CodeBase
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%CodeBase\AssemblyDLL.dll CodeBase\AssemblyDLL.cs

@ rem ���� PrivatePath Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%PrivatePath
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%PrivatePath\AssemblyDLL.dll PrivatePath\AssemblyDLL.cs

@ rem ���� GAC Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%GAC
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%GAC\AssemblyDLL.dll GAC\AssemblyDLL.cs

@ rem ����������
@ csc /t:exe /out:%TestPath%main.exe /r:%TestPath%AssemblyDLL.dll Program.cs

@ rem ���������ļ�
@ xcopy /y %CfgPath%main.exe.config %TestPath% >nul

pause
