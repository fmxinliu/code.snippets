@ if "%1" neq "runas" (
@ call D:\Repo\Project\code.snippets\scripts\bat\admin\以管理员身份运行.bat %~0
@ exit
)

@ chcp 936 >nul
@ title 全局程序集缓存-程序集安装与卸载
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ----------------- 对程序集签名 -----------------

@ rem 生成密钥对
@ sn -k MyCompany.snk >nul

@ rem 编译程序集（用私钥签名，公钥嵌入到清单）
@ csc /keyfile:MyCompany.snk /t:library /out:GacDLL.dll GacDLL.cs >nul

@ rem 验证签名是否有效
@ sn -vf GacDLL.dll >nul



@ rem --------------- 安装程序集到 GAC -----------------

@ gacutil /i GacDLL.dll



@ rem --------------- 引用 GAC 安装的程序集 -----------------

@ rem del /q GacDLL.dll >nul

@ rem 编译主程序（引用DLL。注意：编译时，并不会去GAC中查找部署的DLL!!!）
@ csc /t:exe /out:main.exe /r:GacDLL.dll GacDLLTest.cs >nul

@ rem 删除当前目录DLL，测试运行
@ del /q GacDLL.dll >nul
@ main.exe

@ rem 删除临时文件
@ del /q main.exe MyCompany.* >nul



@ rem --------------- 从 GAC 卸载程序集 -----------------

@ rem 不能指定程序集名称（卸载时会附加exe或者dll）
@ gacutil /u GacDLL

pause
