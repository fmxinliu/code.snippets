@ chcp 936 >nul
@ title 创建强名称程序集 speaker.dll
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem 创建私钥
@ sn -k MyCompany.snk

@ rem 编译程序集（使用私钥进行签名）
@ csc /keyfile:MyCompany.snk /t:library /out:speaker.dll Speaker.cs

@ rem 删除临时文件
@ del /q MyCompany.snk >nul

@ exit
