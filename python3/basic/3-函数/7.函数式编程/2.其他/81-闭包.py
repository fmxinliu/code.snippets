# 闭包：返回的函数，在其定义内部引用了局部变量args


# 1. 易错
def count():
    fs = []
    for i in range(1, 4):
        def f():
            return i * i

        fs.append(f)  # f 函数引用了变量 i, 但 f 函数并非立即执行。
    return fs


f1, f2, f3 = count()
print(f1(), f2(), f3())  # 当执行的时候，i == 3


# 2.改进
def count():
    def f(j):
        def g():
            return j * j

        return g

    fs = []
    for i in range(1, 4):
        fs.append(f(i))  # f(i)立刻被执行，因此 i 的当前值被传入 g()
    return fs


f1, f2, f3 = count()
print(f1(), f2(), f3())


# 3.练习 - 计数器函数
def createCounter():
    n = 0

    def counter():
        nonlocal n  # 引用外部函数定义的局部变量
        n = n + 1
        return n

    return counter


counterA = createCounter()
print(counterA(), counterA(), counterA(), counterA(), counterA())  # 1 2 3 4 5

counterB = createCounter()
if [counterB(), counterB(), counterB(), counterB()] == [1, 2, 3, 4]:
    print('测试通过!')
else:
    print('测试失败!')
