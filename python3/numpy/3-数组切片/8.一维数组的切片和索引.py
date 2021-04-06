import numpy as np

# 创建一维数组
a = np.arange(10)
print('a=', a)

"""
1.通过索引访问元素
"""
# 正索引访问 [0, n-1]
print('第1个元素:', a[0])
print('最后1个元素:', a[9])

# 负索引访问 [-n, -1]
print('最后1个元素:', a[-1])
print('第1个元素:', a[-10])

"""
2.切片 [start:stop:step]
"""
# 正向索引操作
print(a[:])  # 从开头到结尾
print(a[3:])  # 从索引3开始到结尾
print(a[3:5])  # 从索引3开始到索引4 [start, stop)
print(a[1:7:2])  # 从索引1开始到索引6, 步长为2

# 负向索引操作
print(a[::-1])  # 反向
print(a[-5:-2])  # [a[-5], a[-4], a[-3]]
