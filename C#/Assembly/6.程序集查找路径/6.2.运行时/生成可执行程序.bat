@ chcp 936 >nul
@ title ����ǿ�������򼯣����ɿ�ִ�г���
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ set CfgPath=.\���������1�Ͳ���3ʧ��\Config\
@ rem ������Կ��
@ if not exist %CfgPath%MyCompany.snk (
@ sn -k %CfgPath%MyCompany.snk >nul
)

@rem ������Կ
@rem sn -p %CfgPath%MyCompany.snk %CfgPath%MyCompany.publicKey

@rem ��ӡ��Կ
@rem sn -tp %CfgPath%MyCompany.publicKey

@ rem ������򼯣���˽Կǩ������ԿǶ�뵽�嵥��
@ csc /keyfile:%CfgPath%MyCompany.snk /t:library /out:MyDLL.dll MyDLL.cs >nul

@ rem ����������
@ csc /t:exe /out:main.exe /r:MyDLL.dll Program.cs >nul

@ rem �������ƶ���ָ��Ŀ¼
@ xcopy /y MyDLL.dll DLLs\ >nul

@ rem ɾ����ʱ�ļ�
@ del /q MyDLL.dll >nul

@ exit /b 0
