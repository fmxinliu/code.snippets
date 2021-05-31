# 高阶函数：
# 1.可以接受函数作为参数
# 2.还可以把函数作为结果值返回


def lazy_sum(*args):
    def sum():
        ax = 0
        for n in args:
            ax = ax + n
        return ax  # 返回：求和结果

    return sum  # 返回：求和函数


f = lazy_sum(1, 3, 5, 7, 9)
print(f)  # 注意1：返回的并不是求和结果，而是求和函数
print(f())  # 注意2：计算求和的结果

f1 = lazy_sum(1, 3, 5, 7, 9)
f2 = lazy_sum(1, 3, 5, 7, 9)
print(f1 == f2)  # 注意3：f1()和f2()的调用结果互不影响
