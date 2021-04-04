"""
模块搜索顺序：
1.当前目录
2.PYTHONPATH
3.默认路径
"""

import sys
print(sys.path)  # 模块搜索路径


# 1.自定义变量、函数、模块不要和已有模块重名，否则已有模块无法使用
# math = 123
# import math
# math.sqrt(math)

# 2.使用from XXX import YYY，如果 YYY 与已有的变量、函数、模块重复，调用到的是最后定义或导入的功能
def sleep():
    print('自定义sleep')


from time import sleep


# def sleep():
#     print('自定义sleep')

sleep(1)

