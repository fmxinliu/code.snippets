name_list = []
print(f'{name_list}, len={len(name_list)}')

#
# 1.添加元素
#

# append()
name_list.append('Tom')
name_list.append('Lily')
name_list.append('Rose')
print(f'{name_list}, len={len(name_list)}')

# extend()
name_list.extend('小明')  # 如果追加的是序列，会将序列拆开添加
print(f'{name_list}, len={len(name_list)}')  # ['Tom', 'Lily', 'Rose', '小', '明'], len=5

name_list.extend(['小A', '小B'])  # 追加列表
print(f'{name_list}, len={len(name_list)}')  # ['Tom', 'Lily', 'Rose', '小', '明', '小A', '小B'], len=7

name_list.extend(['小A', '小B'])
print(f'{name_list}, len={len(name_list)}')

name_list.extend(['小C'])  # 等价于 name_list.append('小C')
print(f'{name_list}, len={len(name_list)}')

# insert()
name_list.insert(1, '000')
print(f'{name_list}, len={len(name_list)}')

#
# 2.删除元素
#

# del
del name_list[0]  # 删除元素
print(f'{name_list}, len={len(name_list)}')

del (name_list[0])  # 可以添加括号
print(f'{name_list}, len={len(name_list)}')

# del name_list  # 删除列表（删除后，列表未定义，使用会报错）
# print(f'{name_list}, len={len(name_list)}')

# pop(下标) 删除元素，返回被删除的元素
del_name = name_list.pop()  # 删除最后一个
print(f'del_name={del_name}')
print(f'{name_list}, len={len(name_list)}')

del_name = name_list.pop(1)  # 删除指定下标的元素
print(f'del_name={del_name}')
print(f'{name_list}, len={len(name_list)}')

# remove(元素)
name_list.remove('小A')  # 删除查找到的第一个元素
print(f'{name_list}, len={len(name_list)}')

# clear()
name_list.clear()
print(f'{name_list}, len={len(name_list)}')

#
# 3.修改元素
#

# 下标修改
num_list = ['AAA', 3, 2, 5, 4, 6]  # 列表允许混合元素
num_list[0] = 1
print(num_list)

# reverse() 逆序
num_list.reverse()
print(num_list)

# sort()
num_list.sort()  # 默认升序排序
print(num_list)

num_list.sort(reverse=True)  # 降序排序
print(num_list)

# copy()
name_list1 = ['Tom', 'Lily', 'Rose']
name_list2 = name_list1.copy()  # 拷贝列表
name_list1[0] = 1  # 修改原元素
print(name_list1)
print(name_list2)  # 确认拷贝的列表元素没有被修改

#
# 4.查找元素
#

# print(name_list1.index('Tom'))  # 不存在报错
print(name_list2.index('Tom'))  # 返回元素下标

if 'Tom' in name_list1:
    print(name_list1.index('Tom'))  # 不存在报错
else:
    print('Tom 不存在于 name_list1')
