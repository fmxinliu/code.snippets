from matplotlib import pyplot as plt
import numpy as np

a = np.array([22, 87, 5, 43, 56, 73, 55, 54, 11, 20, 51, 5, 79, 31, 27])

"""
numpy.histogram() - 计算统计直方图
"""
hist, bins = np.histogram(a, bins=[0, 20, 40, 60, 80, 100])
print(hist)  # 频数
print(bins)  # 分割边界

"""
plt.hist() - 绘制统计直方图
"""
plt.hist(a, bins=[0, 20, 40, 60, 80, 100])
plt.title("histogram")
plt.show()
