@rem 获取安装的所有包
@ pip freeze>python_modules.txt

@rem 卸载python_modules.txt这个文件中所罗列出的所有包，-y的意思是默认全部同意，这样就不用一直输入y了
@ pip uninstall -r python_modules.txt -y

@rem 删除临时文件
@ del python_modules.txt /q /s
