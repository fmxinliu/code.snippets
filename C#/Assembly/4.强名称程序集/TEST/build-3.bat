@ chcp 936 >nul
@ title ģ����򼯱��滻��DLL�ٳ֣�
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------- ģ����򼯱��滻 ---------------------------
@ echo.
@ echo.
@ echo "��ʼ�滻DLL>>>>>"
@ echo\
@ echo\

@ rem ���뱻�滻��ĳ���
@ csc /t:library /out:speaker.dll SpeakerHiJack.cs

@ exit
