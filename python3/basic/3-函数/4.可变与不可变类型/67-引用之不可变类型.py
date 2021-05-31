a = 1
b = 1
if id(a) == id(b):
    print('同一引用')
else:
    print('不同引用')

a = 1
b = a  # 值靠引用传递
if id(a) == id(b):
    print('同一引用')
else:
    print('不同引用')

b = 2
print(a)
print(b)  # 值是不可变类型
