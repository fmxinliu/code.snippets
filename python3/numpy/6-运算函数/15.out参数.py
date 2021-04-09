"""
out参数的使用
"""

import numpy as np

# 测试-1
x = np.arange(9).reshape(3, 3)  # 3行3列
y = np.empty((3, 3))            # 3行3列(未初始化)
z = np.multiply(x, 10, out=y)   # out可省略
print('x=\n', x)
print('y=\n', y)
print('z=\n', z)

print('\ny和z引用同一个数组：', id(y) == id(z), '\n\n')


# 测试-2
a = np.arange(1, 6)
b = np.zeros(10)
c = np.power(a, 2, out=b[2:7])  # 放入指定的位置
print(a)
print(b)
print(c)
