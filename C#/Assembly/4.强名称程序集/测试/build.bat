@ chcp 936 >nul
@ title ǿ���Ƴ��򼯷��۸���ʾ
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------ ����DLL --------------------------
@ start /wait build-1.bat

@ rem ------------------ ����DLL --------------------------
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul


@ main.exe


@ rem ------------------- ����1 ---------------------------
@ start /wait build-2.bat

@ rem �����ļ����۸ģ�ǩ����֤ʧ��
@ main.exe

@ rem ------------------- ����2 ---------------------------
@ start /wait build-3.bat

@ rem ��Ȼģ����һ����ͬ���Ƶĳ��򼯣�
@ rem ������û��ǩ�����ó��򼯲���õ���������Ͽ�
@ main.exe

@ rem -------------- ������ʱ�ļ� -------------------------
@ del /q main.exe speaker.dll.* *.snk
