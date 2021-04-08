import numpy as np

# 创建一维数组
a = np.arange(1, 25)
print('a=', a)


"""
1.reshape()
"""
# 一维 -> 多维
b = a.reshape(2, 12)            # 直接传入
print('b=\n', b)

c = a.reshape((3, 8))           # 按元组传入
print('c=\n', c)

d = np.reshape(a, (2, 3, 4))    # 只能按元组传入
print('d=', d)

# 多维 -> 一维
e = d.reshape(24)  # 元素个数必须正确
print('e=', e)

f = d.reshape(-1)  # 不必计算元素个数
print('f=', f)


"""
2.ravel()、flatten(): 多维 -> 一维
"""
g = d.ravel()
print('g=', g)

h = d.flatten()
print('h=', h)


"""
3.修改ndarray的属性shape
"""
d.shape = (-1,)      # 多维 -> 一维
print('d=', d)

d.shape = (2, 3, 4)  # 一维 -> 多维
print('d=\n', d)


"""
4.resize()
"""
d.resize(24)     # 多维 -> 一维
print('d=', d)

d.resize(1, 24)  # 一维 -> 多维
print('d=', d)
