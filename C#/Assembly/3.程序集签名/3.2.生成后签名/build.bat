@ chcp 936 >nul
@ title 先生成程序集，再给程序集签名
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------ 创建密钥对 -----------------

@ rem 生成密钥对
@ sn -k MyCompany.snk

@rem 导出公钥
@ sn -p MyCompany.snk MyCompany.publicKey

@rem 打印公钥
@ sn -tp MyCompany.publicKey



@ rem ----------------- 对程序集签名 -----------------

@ rem 编译程序集
@ csc /t:library /out:speaker.dll Speaker.cs >nul

@ rem 反汇编程序集
@ ildasm speaker.dll /out:speaker.dll.il

@ rem 汇编合成新的签名程序集
@ ilasm /key:MyCompany.snk /dll /out:speaker.dll /res:speaker.dll.res speaker.dll.il

@ rem 验证签名是否有效
@ sn -vf speaker.dll



@ rem --------------- 引用签名的程序集 -----------------

@ rem 编译主程序
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul

@ main.exe


@ rem 反汇编 IL（查看签名）
@ ildasm /out:speaker.dll.txt main.exe >nul
@ speaker.dll.txt

@ rem 删除临时文件
@ del /q main.exe speaker.dll.* MyCompany.*
