@ chcp 936 >nul
@ title ǿ�������򼯰汾�ض���
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\ǿ����Test\
@ set AssemblyDLL_V1_Path=%TestPath%v1.1.1.1\
@ set AssemblyDLL_V2_Path=%TestPath%v2.2.2.2\

@ cd ..

@ rd /s /q %TestPath%

@ rem ����V1�汾
@ mkdir %AssemblyDLL_V1_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V1_Path%AssemblyDLL.dll AssemblyDLL.v1.1.1.1.cs >nul

@ rem ����V2�汾
@ mkdir %AssemblyDLL_V2_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V2_Path%AssemblyDLL.dll AssemblyDLL.v2.2.2.2.cs >nul

@ rem ����������
@ csc /t:exe /out:%TestPath%main.exe /r:%AssemblyDLL_V1_Path%AssemblyDLL.dll Program.cs >nul


@ echo ------------ ����1 ------------
@ rem ����ver1������ʱָ����
@ xcopy /y %AssemblyDLL_V1_Path%AssemblyDLL.dll %TestPath% >nul
@ %TestPath%main.exe

@ rem ��ver2�滻ver1
@ copy /y %AssemblyDLL_V2_Path%AssemblyDLL.dll %TestPath% >nul
@ %TestPath%main.exe

@ rem ָ���汾�ض�������ver2��
@ copy /y %CfgPath%main.exe.ǿ����.1.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe
@ del /q %TestPath%AssemblyDLL.dll


@ echo ------------ ����2 ------------
@ xcopy /y %CfgPath%main.exe.ǿ����.2.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe


@ echo ------------ ����3 ------------
@ xcopy /y %CfgPath%main.exe.ǿ����.3.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe
