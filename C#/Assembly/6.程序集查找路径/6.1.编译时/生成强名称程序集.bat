@ chcp 936 >nul
@ title 对程序集签名
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem 生成密钥对
@ sn -k MyCompany.snk >nul

@ rem 编译程序集（用私钥签名，公钥嵌入到清单）
@ csc /keyfile:MyCompany.snk /t:library /out:MyDLL.dll MyDLL.cs >nul

@ rem 将程序集移动到指定目录
@ xcopy /y MyDLL.dll DLLs\ >nul

@ rem 删除临时文件
@ del /q MyDLL.dll MyCompany.* >nul

@ exit /b 0
