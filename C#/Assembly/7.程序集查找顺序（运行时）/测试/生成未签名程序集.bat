@ chcp 936 >nul
@ title ����δǩ������
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\δǩ��Test\

@ cd ..

@ rd /s /q %TestPath%

@ rem �����Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%
@ csc /t:library /out:%TestPath%AssemblyDLL.dll AssemblyDLL.cs

@ rem ���� CodeBase Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%CodeBase
@ csc /t:library /out:%TestPath%CodeBase\AssemblyDLL.dll CodeBase\AssemblyDLL.cs

@ rem ���� PrivatePath Ŀ¼�µ� AssemblyDLL.cs
@ mkdir %TestPath%PrivatePath
@ csc /t:library /out:%TestPath%PrivatePath\AssemblyDLL.dll PrivatePath\AssemblyDLL.cs

@ rem ����������
@ csc /t:exe /out:%TestPath%main.exe /r:%TestPath%AssemblyDLL.dll Program.cs

@ rem ���������ļ�
@ xcopy /y %CfgPath%main.exe.config %TestPath% >nul

pause
