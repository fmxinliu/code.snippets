#
# 字符串是不可变的
#

# 1.单引号
a = "hello world"
print(f'a = {a}', end=', ')
print(type(a))


# 2.双引号
b = "hello world"
print(f'b = {b}', end=', ')
print(type(b))


# 3.三引号
c = '''hello world'''
print(f'c = {c}', end=', ')
print(type(c))

d = """hello world"""
print(f'd = {d}', end=', ')
print(type(d))


# ====== 区别 =======
e = 'hello ' \
    'world'
print(f'e = {e}', end=', ')
print(type(e))

f = '''hello 
world'''
print(f'f = {f}', end=', ')
print(type(f))

g = '''hello 
       world'''
print(f'g = {g}', end=', ')
print(type(g))


# 打印：I'm Tom
h = "I'm Tom"
i = 'I\'m Tom'
j = '''I'm Tom'''
print(f'h = {h}')
print(f'i = {i}')
print(f'j = {j}')


# 字符串是否存在
print('hello' in 'hello world')  # True
print('hello' not in 'hello world')  # False

