@ chcp 936 >nul
@ title 强名称程序集防篡改演示
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------ 生成DLL --------------------------
@ start /wait build-1.bat

@ rem ------------------ 引用DLL --------------------------
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul


@ main.exe


@ rem ------------------- 测试1 ---------------------------
@ start /wait build-2.bat

@ rem 程序集文件被篡改，签名验证失败
@ main.exe

@ rem ------------------- 测试2 ---------------------------
@ start /wait build-3.bat

@ rem 虽然模拟了一个相同名称的程序集，
@ rem 但由于没有签名，该程序集不会得到主程序的认可
@ main.exe

@ rem -------------- 清理临时文件 -------------------------
@ del /q main.exe speaker.dll.* *.snk
