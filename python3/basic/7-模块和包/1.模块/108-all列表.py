import my_module2
my_module2.testB()


# all列表限制：只导入testA
from my_module2 import *
testA()
# testB()
