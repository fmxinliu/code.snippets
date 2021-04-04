num1 = 17
str1 = '10'

# 1.int(x [,base])
print(type(int(str1)))

# 2.float()
print(type(float(num1)))
print(float(num1))
print(float(str1))

# 3.str()
print(type(str(num1)))

# 4.tuple()
list1 = [10, 20, 30]
print(tuple(list1))

# 5.list()
tuple1 = (10, 20, 30)
print(list(tuple1))

# 6.eval() -- 计算在字符串中的有效Python表达式，并返回一个对象
str2 = '1'
str3 = '1.1'
str4 = 'abc'  # error
str5 = 'True'
str6 = 'false'  # error
str7 = '[1000, 2000, 3000]'
str8 = '(1000, 2000, 3000)'
print(type(eval(str2)))  # int
print(type(eval(str3)))  # float
# print(type(eval(str4)))  # ======== error
print(type(eval(str5)))  # bool
# print(type(eval(str6)))   # ======== error
print(type(eval(str7)))  # list
print(type(eval(str8)))  # tuple

# 7.hex()
print(hex(num1))

# 8.oct()
print(oct(num1))

# 9.ord() - 将一个字符转换为它的整数值
print(ord('A'))

# 10.chr() - 将一个整数转换为一个字符
print(chr(66))
