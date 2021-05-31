""" 高阶函数 filter() """

""" 1.过滤序列，得到奇数 """
def is_odd(n):
    return n % 2 == 1  # 保留返回值为True的元素


print(list(filter(is_odd, [1, 2, 3, 4, 5, 6, 7, 8, 9])))


""" 2.删掉空字符串 """
def not_empty(s):
    # if s: 判断 (s=='') or (s==None)
    # strip(X) 移除字符串头尾指定的字符X (默认为空格或换行符)
    return s and s.strip()


print(list(filter(not_empty, ['A', '', 'B', None, 'C', '  '])))


""" 3.打印回数 """
def is_palindrome(n):
    s1 = str(n)
    s2 = s1[::-1]
    return s1 == s2


print(list(filter(is_palindrome, range(1, 100))))


""" 4.求素数 """
def _odd_iter():  # 奇数生成器
    n = 1
    while True:
        n = n + 2
        yield n


def _not_divisible(n):  # 筛选函数
    return lambda x: lambda x: x % n > 0
    # print('---', n)
    # return lambda x: print('===', x, n) == None  # 查看执行过程


def primes():  # 生成器，不断返回下一个素数
    yield 2
    it = _odd_iter()  # 初始序列
    while True:
        n = next(it)  # 返回序列的第一个数
        yield n
        it = filter(_not_divisible(n), it)  # 构造新序列


# 打印100以内的素数:
for n in primes():
    if n < 100:
        print(n)
    else:
        break
