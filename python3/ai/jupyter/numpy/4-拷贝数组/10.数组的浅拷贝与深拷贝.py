import numpy as np


def print_copy_info(a, sub):
    if sub[0, 0] == a[0, 0]:
        print('修改切片数组中元素的值，会影响到原来数组中的元素。')
        print('=>通过切片获取到的新数组，只是原来数组中一部分的【浅拷贝】\n')
    else:
        print('修改copy(切片数组)中元素的值，不会影响到原来数组中的元素。')
        print('=>通过copy(切片数组)获取到的新数组，执行的是【深拷贝】\n')


# 创建二维数组
a = np.arange(1, 13).reshape(4, 3)


# 浅拷贝
sub_a = a[:2:, :2]
sub_a[0, 0] = 99
print_copy_info(a, sub_a)


# 深拷贝
sub_b = np.copy(a[:2:, :2])
sub_b[0, 0] = 100
print_copy_info(a, sub_b)
