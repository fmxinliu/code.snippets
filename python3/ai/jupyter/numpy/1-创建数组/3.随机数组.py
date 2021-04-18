import numpy as np

"""
语法：random(size), size来指定维度大小, 生成随机小数[0.0, 1.0)
"""

# 创建一维数组
a = np.random.random(4)
print('a=\n', a)

# 创建二维数组
b = np.random.random((3, 4))
print('b=\n', b)

# 创建三维数组
c = np.random.random(size=(2, 3, 4))
print('c=\n', c)


"""
语法：randint(low, high, size, dtype), size来指定维度大小, 生成随机整数[low, high)
"""

# 创建一维数组
d = np.random.randint(4, size=5)
print('d=\n', d)

# 创建二维数组
e = np.random.randint(1, 11, (3, 4))
print('e=\n', e)

# 创建三维数组
f = np.random.randint(1, 11, (2, 3, 4))
print('f=\n', f)


"""
语法：randn(d0, d1, ..., dn), 创建标准正态分布，期望=0,方差=1
"""

# 创建三维数组
g = np.random.randn(2, 3, 4)
print('g=\n', g)


"""
语法：normal(loc=0.0, scale=1.0, size=None), 创建正态分布，期望=loc,方差=scale
"""

# 创建三维数组
h = np.random.normal(2.1, 1.1, (2, 3, 4))
print('h=\n', h)
