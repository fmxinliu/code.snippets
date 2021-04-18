import numpy as np

"""
语法：np.arange(start, end, step, dtype)
"""

a = np.arange(1, 11)
print('a=\n', a)

b = np.arange(11)
print('b=\n', b)

c = np.arange(1, 11, 2)
print('c=\n', c)


# 指定数组中元素的类型
d = np.arange(5, dtype=float)
print('d=\n', d)
