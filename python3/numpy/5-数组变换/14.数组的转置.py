import numpy as np

"""
转置(a[i][j] == a[j][i]), 只能用在二维以上数组
1.T
2.transpose()
3.np.transpose(a, axes=None), axes: 维度的索引组成的序列(元组或列表)
"""
# 二维数组转置
x = np.arange(1, 7).reshape(2, 3)
a = x.T
b = x.transpose()
c = np.transpose(x)
print('x=\n', x, 'shape=', x.shape)
print('a=\n', a, 'shape=', a.shape)
print('b=\n', b, 'shape=', b.shape)
print('c=\n', c, 'shape=', c.shape)


# 多维数组转置
y = np.arange(1, 25).reshape(2, 3, 4)
d = np.transpose(y)
e = np.transpose(y, (1, 2, 0))
# 数组的维度索引：                       0  1  2
print('y=\n', y, 'shape=', y.shape)  # (2, 3, 4)
print('d=\n', d, 'shape=', d.shape)  # (4, 3, 2)
print('e=\n', e, 'shape=', e.shape)  # (3, 4, 2)
