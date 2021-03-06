# 数字之间的逻辑运算
a = 0
b = 1
c = 2
d = 0
e = 3

# and运算符，只要有一个值为0，则结果为0，否则结果为最后一个非0数字
print(a and b)  # 0
print(b and a)  # 0
print(a and c)  # 0
print(c and a)  # 0
print(b and c)  # 2
print(c and b)  # 1

print(a and b and c)  # 0
print(b and c and e)  # 3


# or运算符，只有所有值为0结果才为0，否则结果为第一个非0数字
print(a or b)  # 1
print(c or a)  # 2
print(b or c)  # 1
print(a or d)  # 0

print(a or b or c)  # 1
print(b or c or e)  # 1
