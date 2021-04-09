import numpy as np

a = np.arange(9).reshape(3, 3)  # 3行3列
print('第一个数组：')
print(a)
print('\n')
print('第二个数组：')
b = np.array([10, 100, 1000])      # 1行3列，先扩展为3行3列
print(b)
print('\n')


print('判断两个数组相等：np.equal(a, b)')
print(a == 1)
print('\n')


print('判断两个数组不相等：np.not_equal(a, b)')
print(a != 1)
print('\n')


print('判断数组1<数组2：np.less(a, b)')
print(a < 5)
print('\n')


print('判断数组1<=数组2：np.less_equal(a, b)')
print(a <= 5)
print('\n')


print('判断数组1>数组2：np.greater(a, b)')
print(a > 5)
print('\n')


print('判断数组1>数组2：np.greater_equal(a, b)')
print(a >= 5)
print('\n')
