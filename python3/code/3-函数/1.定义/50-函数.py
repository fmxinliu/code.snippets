def my_abs(x):
    if x >= 0:
        return x
    else:
        return -x


print(my_abs(-9))


# 函数可以重定义？？？
def my_abs(x):
    # 参数检查
    if not isinstance(x, (int, float)):
        raise TypeError('bad operand type')

    if x >= 0:
        return x
    else:
        return -x


print(my_abs('A'))
