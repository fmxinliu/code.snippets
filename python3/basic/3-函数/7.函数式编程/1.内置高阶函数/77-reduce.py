list1 = [1, 2, 3, 4, 5]

# 1.导入模块
import functools


# 2.定义功能函数
def func(a, b):
    return a + b


# 3.调用reduce：把结果继续和序列的下一个元素做累积计算
result = functools.reduce(func, list1)
print(result)

# ===== 等价于 ======
f = func
result = f(f(f(f(1, 2), 3), 4), 5)
print(result)

"""
    测试
"""
from functools import reduce

CHAR_TO_INT = {
    '0': 0,
    '1': 1,
    '2': 2,
    '3': 3,
    '4': 4,
    '5': 5,
    '6': 6,
    '7': 7,
    '8': 8,
    '9': 9
}


def str2int(str0):
    """字符串转化为整数"""

    def char2num(s):
        """字符转数字"""
        return CHAR_TO_INT[s]

    def map2int(x, y):
        """序列转整数"""
        return x * 10 + y

    def getsign(s):
        """解析符号"""
        return -1 if s.startswith('-') else 1

    def normalize(s):
        """标准化字符串"""
        return s if s[0].isdigit() else s[1:]

    sign = getsign(str0)
    str1 = normalize(str0)
    map1 = map(char2num, str1)
    num = sign * reduce(map2int, map1)
    return num


print(str2int('0'),
      str2int('1'),
      str2int('12300'),
      str2int('0012345'),
      str2int('-111222333'))

CHAR_TO_FLOAT = {
    '0': 0,
    '1': 1,
    '2': 2,
    '3': 3,
    '4': 4,
    '5': 5,
    '6': 6,
    '7': 7,
    '8': 8,
    '9': 9,
    '.': -1
}


def str2float(str0):
    """字符串转化为浮点数"""

    def char2num(s):
        """字符转数字"""
        return CHAR_TO_FLOAT[s]

    def getsign(s):
        """解析符号"""
        return -1 if s.startswith('-') else 1

    def normalize(s):
        """标准化字符串"""
        return s if s[0].isdigit() else s[1:]

    point = 0

    def to_float(f, n):
        nonlocal point  # 声明函数外部定义的局部变量
        if n == -1:
            point = 1
            return f
        if point == 0:
            return f * 10 + n
        else:
            point = point * 10
            return f + n / point

    sign = getsign(str0)
    str1 = normalize(str0)
    map1 = map(char2num, str1)
    num = sign * reduce(to_float, map1)
    return num

    # # 1.小数点位置
    # index = str0.find('.')
    # pi = index if index >= 0 else 0
    # pos = len(str0) - pi - 1
    #
    # # 2.去掉小数点，转换为整数
    # str1 = str0.replace('.', '')
    # num = str2int(str1)
    #
    # # 3.转换为小数
    # num /= 10 ** pos
    # return num


print(str2float('0'),
      str2float('1'),
      str2float('123.456'),
      str2float('123.45600'),
      str2float('0.1234'),
      str2float('.1234'),
      str2float('-120.0034'))


def normalize(name):
    """名字规范化：首字母大写，其余小写"""
    return name[0].upper() + name[1:].lower()


L1 = ['adam', 'LISA', 'barT']
L2 = list(map(normalize, L1))
print(L2)
