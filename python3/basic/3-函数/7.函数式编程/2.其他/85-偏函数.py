print(int('1234'))  # 10 进制
print(int('1234', 16))
print(int('1234', base=16))  # 16 进制


def int2(x, base=2):
    return int(x, base)


print(int2('010'))
print(int2('110'))

# 创建偏函数
import functools

# functools.partial()：把一个函数的某些参数给固定住（设置默认值），返回一个新的函数，调用这个新函数会更简单
int16 = functools.partial(int, base=16)
print(int16('00ff'))
print(int16('ffff'))
