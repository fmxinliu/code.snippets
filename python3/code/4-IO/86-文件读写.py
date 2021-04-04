import os

"""
只读/写：
  r: 只读，文件不存在报错，文件指针指向起始
  w: 只写，文件不存在新建，文件指针指向起始，文件长度被截断
  a: 只写（追加），文件不存在新建，文件指针指向结尾
  
可读可写：
  r+: 文件不存在报错，文件指针指向起始
  w+: 文件不存在新建，文件指针指向起始，文件长度被截断
  a+: 文件不存在新建，文件指针指向结尾
  
二进制读写：
  rb+
  wb+
  ab+
"""

# 1.打开
f = open('86.txt', 'w')

# 2.操作
f.write('111\n')
f.write('222\n')
f.write('333\n\n')
f.write('444\n')

# 3.关闭
f.close()

# 读取整个文件
f = open('86.txt', 'r')
# print(f.readlines())  # 适合读取小文件
while True:
    buffer = f.read(3)
    if len(buffer) == 0:
        break
    print(buffer, end='')
f.close()

"""
语法：文件对象.seek(偏移量，起始位置)
起始位置：
 0: 文件开头
 1: 当前位置
 2: 文件结尾
"""
f = open('86.txt', 'a+')
f.seek(0)
print(f.readlines())
f.close()
