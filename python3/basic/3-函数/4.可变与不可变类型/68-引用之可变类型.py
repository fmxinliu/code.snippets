aa = [10, 20]
bb = [10, 20]
if id(aa) == id(bb):
    print('同一引用')
else:
    print('不同引用')

bb = aa  # 引用传递
if id(aa) == id(bb):
    print('同一引用')
else:
    print('不同引用')

aa.append(30)
print(aa)
print(bb)  # 列表是可变类型

if id(aa) == id(bb):
    print('同一引用')
else:
    print('不同引用')

