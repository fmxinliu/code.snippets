import numpy as np


def print_ndarray_info(array: np.ndarray):
    print('数组的秩:', array.ndim)
    print('数组的维度:', array.shape)
    print('数组元素的总个数:', array.size)
    print('每个元素的大小(bytes):', array.itemsize)
    print('元素类型:', array.dtype)
    print('元素的实部:', array.real)
    print('元素的虚部:', array.imag)
    print()


# 一维数组
a = np.arange(5)
print_ndarray_info(a)

# 二维数组
b = np.random.randint(1, 10, (3, 4), np.int8)
print_ndarray_info(b)

# 三维数组
c = np.random.randn(2, 3, 4)
print_ndarray_info(c)
