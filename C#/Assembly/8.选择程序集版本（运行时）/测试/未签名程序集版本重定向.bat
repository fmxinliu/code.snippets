@ chcp 936 >nul
@ title 未签名程序集版本重定向（运行时）
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\未签名Test\
@ set AssemblyDLL_V1_Path=%TestPath%v1.1.1.1\
@ set AssemblyDLL_V2_Path=%TestPath%v2.2.2.2\

@ cd ..

@ rd /s /q %TestPath%

@ rem 编译V1版本
@ mkdir %AssemblyDLL_V1_Path%
@ csc /t:library /out:%AssemblyDLL_V1_Path%AssemblyDLL.dll AssemblyDLL.v1.1.1.1.cs >nul

@ rem 编译V2版本
@ mkdir %AssemblyDLL_V2_Path%
@ csc /t:library /out:%AssemblyDLL_V2_Path%AssemblyDLL.dll AssemblyDLL.v2.2.2.2.cs >nul

@ rem 编译主程序
@ csc /t:exe /out:%TestPath%main.exe /r:%AssemblyDLL_V1_Path%AssemblyDLL.dll Program.cs >nul


@ echo 测试1
@ copy /y %CfgPath%main.exe.未签名.1.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe

@ echo 测试2
@ xcopy /y %CfgPath%main.exe.未签名.2.config %TestPath%main.exe.config >nul
@ %TestPath%main.exe
