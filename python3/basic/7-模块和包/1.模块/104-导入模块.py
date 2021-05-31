"""
1.一个py文件，就是一个模块
2.模块名，就是py文件的名字
3.py文件可以任意命名，但模块名要遵循标识符规则。因此，作为模块的py文件名，也要遵循标识符规则
"""

# 方法一：import 模块名; 模块名.功能
import math
print(math.sqrt(9))


# 方法二： from 模块名 import 功能1, 功能2...; 功能调用(不需要书写模块名.功能)
from math import fabs
print(fabs(-1))


# 方法三：from 模块名 import *; 功能调用(由模块文件中定义的all列表，控制导入那些功能，如果没有定义all列表，则导入全部功能)
from math import *
print(floor(9.1))
