@ chcp 936 >nul
@ title 生成强命名程序集
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\强命名Test\

@ cd ..

@ rd /s /q %TestPath%

@ rem 编译根目录下的 AssemblyDLL.cs
@ mkdir %TestPath%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%AssemblyDLL.dll AssemblyDLL.cs

@ rem 编译 CodeBase 目录下的 AssemblyDLL.cs
@ mkdir %TestPath%CodeBase
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%CodeBase\AssemblyDLL.dll CodeBase\AssemblyDLL.cs

@ rem 编译 PrivatePath 目录下的 AssemblyDLL.cs
@ mkdir %TestPath%PrivatePath
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%PrivatePath\AssemblyDLL.dll PrivatePath\AssemblyDLL.cs

@ rem 编译 GAC 目录下的 AssemblyDLL.cs
@ mkdir %TestPath%GAC
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%TestPath%GAC\AssemblyDLL.dll GAC\AssemblyDLL.cs

@ rem 编译主程序
@ csc /t:exe /out:%TestPath%main.exe /r:%TestPath%AssemblyDLL.dll Program.cs

@ rem 拷贝配置文件
@ xcopy /y %CfgPath%main.exe.config %TestPath% >nul

pause
