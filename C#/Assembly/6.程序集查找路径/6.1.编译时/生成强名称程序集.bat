@ chcp 936 >nul
@ title �Գ���ǩ��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ������Կ��
@ sn -k MyCompany.snk >nul

@ rem ������򼯣���˽Կǩ������ԿǶ�뵽�嵥��
@ csc /keyfile:MyCompany.snk /t:library /out:MyDLL.dll MyDLL.cs >nul

@ rem �������ƶ���ָ��Ŀ¼
@ xcopy /y MyDLL.dll DLLs\ >nul

@ rem ɾ����ʱ�ļ�
@ del /q MyDLL.dll MyCompany.* >nul

@ exit /b 0
