str1 = 'abcd'
list1 = ['hello', 'world']
t1 = ('world',)
dict1 = {'name': 'Tome'}
s1 = {1, 2, 3, 4}

# 1.len()
print(len(str1))
print(len(list1))
print(len(t1))
print(len(dict1))  # 1
print(len(s1))

# 3.max()/min()
print(max(str1))
print(max(list1))
print(max(t1))
print(max(dict1))
print(max(s1))

# 4.range(start, end, step) -- 生成从[start, end)步长为step的数字，供for循环使用
print(range(1, 10, 1))
print(range(1, 10))  # 默认步长为 1
print(range(1, 10, 2))
print(range(10))  # 默认开始为 0

for i in range(3):
    print(i)

# 5.enumerate(可遍历对象,start=0) -- 将可遍历对象(列表/元组/字符串)组合为索引序列(同时列出数据和数据下标)。一般配合for循环使用
for index, char in enumerate(str1, 100):
    print(f'下标是{index}, 对应的字符是{char}')

# 2.del/del()
del str1
del list1[0]  # 可删除元素
del t1
del dict1['name']  # 可删除元素
del s1
