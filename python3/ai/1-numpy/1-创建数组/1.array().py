import numpy as np

# 创建一维数组
a = np.array([1, 2, 3, 4])
print('a=\n', a)

# 创建二维数组
b = np.array([[1, 2, 3], [4, 5, 6], [7, 8, 9]])
print('b=\n', b)

# 创建三维数组
c = np.array([[[1, 2, 3], [4, 5, 6], [7, 8, 9]]])
print('c=\n', c)


# 指定数组中元素的类型
d = np.array([1, 2, 3], dtype=float)
print('d=\n', d)


# 指定数组维度
e = np.array([1, 2, 3], ndmin=5)
print('e=\n', e)
