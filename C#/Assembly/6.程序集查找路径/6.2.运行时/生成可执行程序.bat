@ chcp 936 >nul
@ title 引用强命名程序集，生成可执行程序
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=.\解决：测试1和测试3失败\Config\
@ rem 生成密钥对
@ if not exist %CfgPath%MyCompany.snk (
@ sn -k %CfgPath%MyCompany.snk >nul
)

@rem 导出公钥
@rem sn -p %CfgPath%MyCompany.snk %CfgPath%MyCompany.publicKey

@rem 打印公钥
@rem sn -tp %CfgPath%MyCompany.publicKey

@ rem 编译程序集（用私钥签名，公钥嵌入到清单）
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:MyDLL.dll MyDLL.cs >nul

@ rem 编译主程序
@ csc /t:exe /out:main.exe /r:MyDLL.dll Program.cs >nul

@ rem 将程序集移动到指定目录
@ xcopy /y MyDLL.dll DLLs\ >nul

@ rem 删除临时文件
@ del /q MyDLL.dll >nul

@ exit /b 0
