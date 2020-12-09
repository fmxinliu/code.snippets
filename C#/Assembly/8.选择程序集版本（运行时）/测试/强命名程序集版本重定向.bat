@ chcp 936 >nul
@ title 强命名程序集版本重定向
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\强命名Test\
@ set AssemblyDLL_V1_Path=%TestPath%v1.1.1.1\
@ set AssemblyDLL_V2_Path=%TestPath%v2.2.2.2\

@ cd ..

@ rd /s /q %TestPath%

@ rem 编译V1版本
@ mkdir %AssemblyDLL_V1_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V1_Path%AssemblyDLL.dll AssemblyDLL.v1.1.1.1.cs >nul

@ rem 编译V2版本
@ mkdir %AssemblyDLL_V2_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V2_Path%AssemblyDLL.dll AssemblyDLL.v2.2.2.2.cs >nul

@ rem 编译主程序
@ csc /t:exe /out:%TestPath%main.exe /r:%AssemblyDLL_V1_Path%AssemblyDLL.dll Program.cs >nul


@ echo ------------ 测试1 ------------
@ rem 加载ver1（编译时指定）
@ xcopy /y %AssemblyDLL_V1_Path%AssemblyDLL.dll %TestPath% >nul
@ %TestPath%main.exe

@ rem 用ver2替换ver1
@ copy /y %AssemblyDLL_V2_Path%AssemblyDLL.dll %TestPath% >nul
@ %TestPath%main.exe

@ rem 指定版本重定向（引用ver2）
@ copy /y %CfgPath%main.exe.强命名.1.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe
@ del /q %TestPath%AssemblyDLL.dll


@ echo ------------ 测试2 ------------
@ xcopy /y %CfgPath%main.exe.强命名.2.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe


@ echo ------------ 测试3 ------------
@ xcopy /y %CfgPath%main.exe.强命名.3.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe
