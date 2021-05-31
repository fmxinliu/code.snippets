# 1. 变量可以指向函数
fn = abs
print(fn(-123))

# 2.函数名也是变量
print(abs)  # abs指向内建函数
abs = 333
print(abs)  # abs指向其他对象

import builtins; abs = builtins.abs;  # 恢复abs指向内建函数
abs(10)


# 定义高阶函数
def add(a, b, f):
    """高阶函数：把函数作为参数f传入"""
    return f(a) + f(b)


print(add(-1, 2, abs))  # 绝对值求和
print(add(1.1, 2.5, round))  # 四舍五入求和
