@ chcp 936 >nul
@ title ����ʱ��������ǩ��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------ ������Կ�� -----------------

@ rem ������Կ��
@ sn -k MyCompany.snk

@rem ������Կ
@ sn -p MyCompany.snk MyCompany.publicKey

@rem ��ӡ��Կ
@ sn -tp MyCompany.publicKey



@ rem ----------------- �Գ���ǩ�� -----------------

@ rem ������򼯣���˽Կǩ������ԿǶ�뵽�嵥��
@ csc /keyfile:MyCompany.snk /t:library /out:speaker.dll Speaker.cs >nul

@ rem ��֤ǩ���Ƿ���Ч
@ sn -vf speaker.dll



@ rem --------------- ����ǩ���ĳ��� -----------------

@ rem ����������
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul

@ main.exe


@ rem ����� IL���鿴ǩ����
@ ildasm /out:speaker.dll.txt main.exe >nul
@ speaker.dll.txt

@ rem ɾ����ʱ�ļ�
@ del /q main.exe speaker.dll.* MyCompany.*
