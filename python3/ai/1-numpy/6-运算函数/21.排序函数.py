"""
sort(a, axis, kind, order)
参数说明：
    a: 要排序的数组
    axis: 沿着它排序数组的轴，如果没有数组会被展开，沿着最后的轴排序， axis=0 按列排序，axis=1 按行排序
    kind: 默认为'quicksort'（快速排序），可选：'mergesort'（归并排序），'heapsort'（堆排序）
    order: 如果数组包含字段，则是要排序的字段
"""

import numpy as np

# 按行、列排序
a = np.array([[3, 7], [9, 1]])
print('原始数组：')
print(a)
print('\n')

print('按行排序：')
print(np.sort(a))
print('\n')

print('按列排序：')
print(np.sort(a, axis=0))
print('\n')

# 按字段排序
dt = np.dtype([('name', 'S10'), ('age', int)])
b = np.array([("raju", 21), ("anil", 25), ("ravi", 17), ("amar", 27)], dtype=dt)
print('原始数组：')
print(b)
print('\n')

print('按 name 排序：')
print(np.sort(b, order='name'))
