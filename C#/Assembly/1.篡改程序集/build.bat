@ chcp 936 >nul
@ title �۸ĳ�����ʾ
@ call "%VS100COMNTOOLS%vsvars32.bat"

@ rem �������
@ csc /t:library /out:speaker.dll Speaker.cs

@ rem ����������
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs

@ main.exe

@ echo.
@ echo.
@ echo "��ʼ����>>>>>"
@ echo\
@ echo\

@ rem ���뱻�۸ĺ�ĳ���
@ csc /t:library /out:speaker.dll SpeakerHiJack.cs
@ main.exe

@ rem ��ʱɾ��
@ ping -n 5 127.0.0.1 >nul
@ del /q main.exe speaker.dll
