import numpy as np

a = np.array([0, 30, 45, 60, 90])   # 角度
b = a * np.pi / 180                 # 弧度

print('计算角度的正弦值，输入以弧度为单位：')
sin = np.sin(b)
print(sin)
print('\n')


print('计算角度的反正弦，返回值以弧度为单位：')
inv = np.arcsin(sin)
print(inv)
print('\n')


print('通过转化为角度制来检查结果：')
print(np.degrees(inv))
print('\n')
