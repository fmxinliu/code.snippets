@ chcp 936 >nul
@ title ����ǿ���Ƴ��� speaker.dll
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ����˽Կ
@ sn -k MyCompany.snk

@ rem ������򼯣�ʹ��˽Կ����ǩ����
@ csc /keyfile:MyCompany.snk /t:library /out:speaker.dll Speaker.cs

@ rem ɾ����ʱ�ļ�
@ del /q MyCompany.snk >nul

@ exit
