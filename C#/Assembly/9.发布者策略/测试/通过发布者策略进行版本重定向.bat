@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\以管理员身份运行.bat %~0
@ exit
)

@ chcp 936 >nul
@ title 发布者策略演示
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=%cd%\
@ set TestPath=%cd%\Test\
@ set AssemblyDLL_V1_Path=%TestPath%v1.1.1.1\
@ set AssemblyDLL_V2_Path=%TestPath%v2.2.2.2\

@ rem 策略文件名有如下规则：
@ set AssemblyOldVersion=1.1
@ set AssemblyName=TestAssembly
@ set PolicyFileName=policy.%AssemblyOldVersion%.%AssemblyName%
@ set PolicyFileVersion=1.0.0.0


@ cd ..
@ rd /s /q %TestPath% >nul

@ rem 如果GAC存在程序集，先卸载
@ gacutil /u %AssemblyName% >nul
@ gacutil /u %PolicyFileName% >nul


@ rem ---
@ title 安装软件，初始DLL版本号：v1.1
@ rem ---


@ rem 编译V1版本
@ mkdir %AssemblyDLL_V1_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V1_Path%%AssemblyName%.dll %AssemblyName%.v1.1.1.1.cs >nul

@ rem 编译主程序
@ csc /t:exe /out:%TestPath%main.exe /r:%AssemblyDLL_V1_Path%%AssemblyName%.dll Program.cs

@ rem 安装V1版本到GAC
@ gacutil /i %AssemblyDLL_V1_Path%%AssemblyName%.dll

@ rem 运行
@ %TestPath%main.exe


@ rem ---
@ title 安装新DLL，版本号：v2.2，并通过发布者策略（版本v1.0.0.0），引用新DLL（不覆盖v1.1）
@ rem ---


@ rem 编译V2版本
@ mkdir %AssemblyDLL_V2_Path%
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:%AssemblyDLL_V2_Path%%AssemblyName%.dll %AssemblyName%.v2.2.2.2.cs >nul

@ rem 安装V2版本到GAC
@ gacutil /i %AssemblyDLL_V2_Path%%AssemblyName%.dll


@ rem 打包发布者策略文件
@ xcopy /y %CfgPath%%AssemblyName%.config %TestPath% >nul
@ al /keyfile:%CfgPath%MyCompany.snk /version:%PolicyFileVersion% /out:%TestPath%%PolicyFileName%.dll /link:%CfgPath%%AssemblyName%.config

@ rem 安装发布者策略到GAC
@ gacutil /i %TestPath%%PolicyFileName%.dll

@ rem 运行
@ %TestPath%main.exe


pause


@ rem ---
@ title 卸载程序集，结束测试
@ rem ---

@ gacutil /u %AssemblyName% >nul
@ gacutil /u %PolicyFileName% >nul
