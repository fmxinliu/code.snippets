@ chcp 936 >nul
@ title ģ����򼯱��۸ģ�DLLע�룩
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------- ģ����򼯱��۸� ---------------------------
@ rem ����۸ĳ���
@ csc /t:exe /out:speakerAttack.exe SpeakerAttack.cs

@ echo.
@ echo.
@ echo "��ʼ�۸�DLL>>>>>"
@ echo\
@ echo\

@ rem ���д۸ĳ���
@ copy /y speaker.dll AssemblyLib.dll >nul
@ speakerAttack.exe
@ copy /y AssemblyLib.dll speaker.dll >nul

@ rem ɾ����ʱ�ļ�
@ del /q speakerAttack.exe AssemblyLib.dll >nul

@ exit
