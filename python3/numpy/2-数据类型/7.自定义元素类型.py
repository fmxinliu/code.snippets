import numpy as np

# 自定义类型
a = np.zeros((2, 2), dtype=[('x', 'i4'), ('y', 'i4'), ('z', 'i4')])
print('a=\n', a)
