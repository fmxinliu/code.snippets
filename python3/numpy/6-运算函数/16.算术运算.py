"""
ufunc(universal function)：这些函数能够作用于ndarray对象的每一个元素上，而不是针对ndarray对象操作
"""

import numpy as np

a = np.arange(9).reshape(3, 3)  # 3行3列
print('第一个数组：')
print(a)
print('\n')
print('第二个数组：')
b = np.array([10, 100, 1000])      # 1行3列，先扩展为3行3列
print(b)
print('\n')


print('两个数组相加：np.add(a, b)')
print(a + b)
print('\n')


print('两个数组相减：np.subtract(a, b)')
print(a - b)
print('\n')


print('两个数组相乘：np.multiply(a, b)')
print(a * b)
print('\n')


print('数组取幂次方：np.power(a, b)')
print(a ** b)
print('\n')


print('两个数组相除：np.divide(a, b), np.true_divide(a, b)')
print(a / b)
print('\n')


print('两个数组整除：np.floor_divide(a, b)')
print(a // b)
print('\n')


print('数组取余：np.remainder(a, b), np.mod(a, b)')
print(a % b)
print('\n')
