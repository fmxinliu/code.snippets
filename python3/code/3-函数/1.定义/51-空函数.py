# 1. 空函数
def nop():
    pass  # 占位符


nop()

# 空函数，返回None
print(nop())


# 2.无返回值的函数
def nop1():
    return


def nop2():
    print('hello world')


# 无返回值的函数，返回None
print(nop1())
print(nop2())
