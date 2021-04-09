"""
sum     - 求和
prod    - 所有元素相乘
mean    - 算术平均值
average - 加权平均值(=各数值乘以相应的权数，求和，再除以总的权数和)
var     - 方差
std     - 标准差(=方差的算术平方根)
median  - 中数
power   - 幂运算
sqrt    - 开方
min     - 最小值
max     - 最大值
argmin  - 最小值的下标
argmax  - 最大值的下标
exp(x)  - 以 e 为底的指数
log(x)  - 对数
inf     - 无穷大
nan     - 不是一个数字
"""

import numpy as np

'''中位数'''
# 1.偶数：中位数=中间2个数的平均值
a = np.arange(1, 5).reshape(2, 2)
print('数组：')
print(a)
print('中位数：', np.median(a), '\n')

# 2.奇数
b = np.arange(1, 10).reshape(3, 3)
print('数组：')
print(b)
print('中位数：', np.median(b), '\n')


''''算术平均值 和  加权平均值'''
x = np.array([1, 2, 3, 4])
print('原始数组：', x, '\n')


print('调用 mean() 函数：', np.mean(x))
print('调用 average() 函数，不指定权重：', np.average(x))
print('=>average() 不指定权重时相当于 mean() 函数\n')


# 加权平均值 = (1*4+2*3+3*2+4*1)/(4+3+2+1)
y = np.array([4, 3, 2, 1])
print('调用 average() 函数，指定权重：')
print('权重=', y)
print('结果=', np.average(x, weights=y))
print('(结果, 权重的和)=', np.average(x, weights=y, returned=True))  # 如果 returned 参数设为 true，则返回权重的和
