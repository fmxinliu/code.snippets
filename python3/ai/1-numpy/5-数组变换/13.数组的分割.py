import numpy as np

"""
split(ary, indices_or_sections, axis=0)
参数说明：
    ary：被分割的数组
    indices_or_sections：如果是一个整数，就用该数平均切分；如果是一个列表或元组，为沿轴切分的位置索引
    axis：沿着那个维度切分
"""
# help(np.split)


# 分割一维数组
x = np.arange(1, 9)
print('x=', x)

a = np.split(x, 4)  # 平均分割成4份
print('a=', a)

b = np.split(x, [3, 5])  # 按位置分割成3份: x[0]-x[2]、x[3]-x[4]、x[5]-x[-1]
print('b=', b)


# 分割二维数组
y = np.arange(1, 25).reshape(4, 6)
print('y=\n', y)

c = np.split(y, 2, axis=0)       # 垂直均分
print('c=\n', c)

d = np.split(y, [2, 3], axis=0)  # 垂直位置
print('d=\n', d)

e = np.split(y, 2, axis=1)       # 水平均分
print('e=\n', e)

f = np.split(y, [2, 3], axis=1)  # 水平位置
print('f=\n', f)


"""
hsplit(ary, indices_or_sections): 水平分割
vsplit(ary, indices_or_sections): 垂直分割
"""
