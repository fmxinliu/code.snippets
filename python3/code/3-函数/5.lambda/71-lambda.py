# 语法 f = lambda 参数列表: 表达式

# 1.无参数
fn1 = lambda: print('hello world')
fn1()

# 2.一个参数
fn2 = lambda x, y: x + y
print(fn2(1, 2))

# 3.默认参数
fn3 = lambda x, y, z=3: x + y + z
print(fn3(1, 2))

# 4.可变参数
fn4 = lambda *args: args
fn5 = lambda **kwargs: kwargs
print(fn4('hello world', 123))
print(fn5(name='lucky'))
