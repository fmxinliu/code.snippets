@ cd ..

@ call ���ɿ�ִ�г���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����3������ʱָ�����ҡ�����������Ŀ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ echo ������ָ��������Ŀ¼.config��
@ copy /y ���������1�Ͳ���3ʧ��\Config\ָ��������Ŀ¼.config .\main.exe.config >nul
@ call ��֤�����Ƿ�ɹ�.bat needconfig

@ rem ɾ�����Գ���
@ del /q main.exe >nul

pause
