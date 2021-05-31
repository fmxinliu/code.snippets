import numpy as np

# 创建2个数组
a = np.arange(1, 7).reshape(2, 3)
b = np.arange(7, 13).reshape(2, 3)
print('a=\n', a)
print('b=\n', b)


"""
1.hstack(列表或元组) 水平拼接
"""
# c = np.hstack([a, b])  # 可以传递列表
c = np.hstack((a, b))    # 可以传递元组
print('c=\n', c)


"""
2.vstack(列表或元组) 垂直拼接
"""
d = np.vstack((a, b))
print('d=\n', d)


"""
3.concatenate((a1, a2,...), axis=0)
参数说明：
    a1, a2,...：相同类型的数组
    axis：沿着那个维度拼接
"""
e1 = np.arange(1, 13).reshape(1, 2, 6)   # (1, 2, 6)
e2 = np.arange(13, 25).reshape(1, 2, 6)  # (1, 2, 6)
print('e1=\n', e1, 'shape=', e1.shape)
print('e2=\n', e2, 'shape=', e2.shape)

# 三维：axis=0
f1 = np.concatenate((e1, e2), axis=0)     # (2, 2, 6)
print('f1=\n', f1, 'shape=', f1.shape)

# 三维：axis=1
f2 = np.concatenate((e1, e2), axis=1)     # (1, 4, 6)
print('f2=\n', f2, 'shape=', f2.shape)

# 三维：axis=2
f3 = np.concatenate((e1, e2), axis=2)     # (1, 2, 12)
print('f3=\n', f3, 'shape=', f3.shape)
