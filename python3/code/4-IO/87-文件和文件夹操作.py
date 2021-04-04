import os
import os.path

dirpath = os.getcwd()
filepath = os.getcwd() + '\\' + '87-文件和文件夹操作.py'

print('当前路径：' + dirpath)
print('上级目录：' + os.path.abspath('..'))

print('父文件夹名：' + os.path.dirname(dirpath))
print('子文件夹名：' + os.path.basename(dirpath))

print('目录名：' + os.path.dirname(filepath))
print('文件名：' + os.path.basename(filepath))

print(f"'{dirpath}'是文件夹？{os.path.isdir(dirpath)}")
print(f"'{filepath}'是文件？{os.path.isfile(filepath)}")

# 创建目录
if not os.path.exists('87-1'):
    os.mkdir('87-1')  # 单级目录

if not os.path.exists('87-2\88\89'):
    os.makedirs('87-2\88\89')  # 多级目录

# 遍历目录
print(os.listdir('.'))

# 删除空目录
os.rmdir('87-1')
os.rmdir('87-2\88\89')
os.rmdir('87-2\88')
os.rmdir('87-2')
