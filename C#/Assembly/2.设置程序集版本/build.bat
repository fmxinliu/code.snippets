@ chcp 936 >nul
@ title ���ó��򼯰汾
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem �������
@ csc /t:library /out:speaker.dll Speaker.cs >nul

@ rem ����������
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul

@ main.exe

@ rem ��ʱ
@ ping -n 3 127.0.0.1 >nul

@ rem ����� IL
@ ildasm /out:speaker.dll.txt main.exe >nul
@ type speaker.dll.txt

@ ping -n 5 127.0.0.1 >nul
@ del /q main.exe speaker.dll.*
