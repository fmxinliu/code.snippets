import numpy as np

"""
语法：zeros
"""
# 创建一维数组
a = np.zeros(5)
print('a=\n', a)

b = np.zeros((5,))
print('b=\n', b)

c = np.zeros(5, dtype=int)
print('c=\n', c)

# 创建二维数组
d = np.zeros((3, 4), dtype=np.byte)
print('d=\n', d)


"""
语法：ones
"""
# 创建三维数组
e = np.ones((2, 3, 4), dtype=np.byte)
print('e=\n', e)


"""
语法：empty, 创建指定维度、数据类型的数组，但不对元素进行初始化
"""
f = np.empty((2, 3, 4), dtype=np.byte)
print('f=\n', f)


"""
语法：linspace(start, stop, num, dyte), 创建一维数组(等差序列)
"""
g = np.linspace(1, 10, 10, dtype=np.byte)
print('g=\n', g)


"""
语法：logspace(start, stop, num, base, dyte), 创建一维数组(等比序列)
"""
h = np.logspace(0, 9, 10, base=2, dtype=np.int32)
print('h=\n', h)


"""
语法：eye(N, M=None, k=0, dtype)，创建 N * M 对角阵
"""
eye1 = np.eye(3)
print('eye1=\n', eye1)

eye2 = np.eye(3, 4, dtype=int)
print('eye2=\n', eye2)

eye3 = np.eye(3, 4, k=1, dtype=int)
print('eye3=\n', eye3)

eye4 = np.eye(3, 4, k=-1, dtype=int)
print('eye4=\n', eye4)
