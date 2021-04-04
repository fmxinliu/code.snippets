import math


# 函数返回多个值
def move(x, y, step, angle):
    nx = x + step * math.cos(angle)
    ny = y + step * math.sin(angle)
    return nx, ny


# 1.实际上返回仍是单个值，只不过组合成一个元组
r = move(100, 100, 60, math.pi / 6)
print(f'{r}, {type(r)}')

# 2.支持的调用方式
(x, y) = move(100, 100, 60, math.pi / 6)
print(f'{x}, {y}')

x, y = move(100, 100, 60, math.pi / 6)  # 可省略括号
print(f'{x}, {y}')
