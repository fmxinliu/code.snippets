@ if "%1" neq "runas" (
@ call D:\Repo\Project\code.snippets\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ chcp 936 >nul
@ title ȫ�ֳ��򼯻���-���򼯰�װ��ж��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ----------------- �Գ���ǩ�� -----------------

@ rem ������Կ��
@ sn -k MyCompany.snk >nul

@ rem ������򼯣���˽Կǩ������ԿǶ�뵽�嵥��
@ csc /keyfile:MyCompany.snk /t:library /out:GacDLL.dll GacDLL.cs >nul

@ rem ��֤ǩ���Ƿ���Ч
@ sn -vf GacDLL.dll >nul



@ rem --------------- ��װ���򼯵� GAC -----------------

@ gacutil /i GacDLL.dll



@ rem --------------- ���� GAC ��װ�ĳ��� -----------------

@ rem del /q GacDLL.dll >nul

@ rem ��������������DLL��ע�⣺����ʱ��������ȥGAC�в��Ҳ����DLL!!!��
@ csc /t:exe /out:main.exe /r:GacDLL.dll GacDLLTest.cs >nul

@ rem ɾ����ǰĿ¼DLL����������
@ del /q GacDLL.dll >nul
@ main.exe

@ rem ɾ����ʱ�ļ�
@ del /q main.exe MyCompany.* >nul



@ rem --------------- �� GAC ж�س��� -----------------

@ rem ����ָ���������ƣ�ж��ʱ�ḽ��exe����dll��
@ gacutil /u GacDLL

pause
