"""
1.一个包，就是物理上的一个文件夹
2.文件夹中，包含一个__init__.py，可以控制（from 模块名 import *方式）包的导入行为
3.文件夹中，包含若干模块
"""

# 方法一：import 包名.模块名; 包名.模块名.功能
import mypackage.my_module1
mypackage.my_module1.test()


# 方法二： from 包名 import 模块名1, 模块名2...; 模块名.功能
from mypackage import my_module2
my_module2.test()


# 方法三：from 模块名 import *; 模块名.功能(由__init__.py文件中定义的all列表，控制导入那些模块，如果没有定义all列表，不能导入)
from mypackage import *
# my_module1.test()
my_module2.test()
